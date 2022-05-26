using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    //Player behaviour elements
    [Header("PlayerLocomotion")]
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float crouchSpeed = 7f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpRaycastDistance = 4f;
    public float hAxis;
    public float vAxis;


    //ui elements 
    [Header("Stamina Ui Controls")]
    [SerializeField] private Text staminaText;
    [SerializeField] Image StaminaSliderImage;
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private float staminaDepletFactor = 13;
    [SerializeField] private float staminaRefillFactor = 10;
    [SerializeField] private Color maxStaminaColor;
    [SerializeField] private Color zeroStaminaColor;

    private BoxCollider playerCollider;
    private float currentStamina;

    private Rigidbody rb;


    //AnimatorControllers and booleans
    private Animator anim;
    private bool IsCrouching = false;
    private bool IsSprinting = false;
    private bool IsWalking = false;
    private bool IsJumping = false;


    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();        
        playerCollider = GetComponent<BoxCollider>();

        currentStamina = maxStamina;

        StaminaUiUpdate();
    }


    private void Update()
    {
        Jump();
    }


    private void FixedUpdate()
    {

        Move();

        Crouching();
        Sprint();
    }



    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal"); //getting the inputs
        float vAxis = Input.GetAxisRaw("Vertical");  // from user

        if (IsSprinting == true) //Determining the speed to be used based on walk, sprint, crouch...
        {

            speed = sprintSpeed;
        }
        else if (IsCrouching == true)
        {
            speed = crouchSpeed;

        }
        else
        {
            speed = walkSpeed;
        }

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.fixedDeltaTime; // calculate for new position 
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement); // moves the player to new position
        rb.MovePosition(newPosition);
    }


    private bool IsGrounded() //function to check whether the player is on ground
    {
        Debug.DrawRay(transform.position, Vector3.down * jumpRaycastDistance, Color.blue); 
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance); //Fires a ray from the current position in downward direction for some distance
    }

    private void Jump()
    {
        if (IsGrounded()) // check if the player is on ground
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse); // add jumpForce as impulse in Y axis and no effect on X and Z axis
            }
        }
    }

    private void Crouching()
    {
        if (Input.GetKey(KeyCode.C))
        {
            IsCrouching = true;
           
        }
        else
        {
            IsCrouching = false;
           
        }
    }

    private void Sprint()
    {
        //write code for stamina handling 
        if ((Input.GetKey(KeyCode.W)) && ((Input.GetKey(KeyCode.LeftShift))))
        {
            if (IsGrounded())
            {
                if (currentStamina > 0)
                {
                    IsSprinting = true;
                    currentStamina -= (staminaDepletFactor * Time.deltaTime);
                }
                else
                {
                    IsSprinting = false;
                }
            }

        }
        else
        {
            IsSprinting = false;

            if (currentStamina < maxStamina)//stamina regeneration
            {
                currentStamina += (staminaRefillFactor * Time.deltaTime);
            }
        }
        currentStamina = Mathf.Clamp(currentStamina, 0, 100);
        StaminaUiUpdate();
        //UpdateAnimation();


    }

    private void StaminaUiUpdate()
    {
        //write code for stamina ui
        float staminaPercentage = CalculateStaminaPercentage();

        //StaminaSlider.value = currentStamina;
        StaminaSliderImage.fillAmount = staminaPercentage / 100;
        StaminaSliderImage.color = Color.Lerp(zeroStaminaColor, maxStaminaColor, staminaPercentage / 100);

        staminaText.text = "" + currentStamina.ToString("0");
        staminaText.color = Color.Lerp(zeroStaminaColor, maxStaminaColor, staminaPercentage / 100);

    }

    private float CalculateStaminaPercentage()
    {
        return ((float)currentStamina / (float)maxStamina) * 100;//casting is used to treat values as floats
    }


}

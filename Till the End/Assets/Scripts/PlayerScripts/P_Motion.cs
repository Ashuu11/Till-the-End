using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Motion : MonoBehaviour
{
    //Player behaviour elements
    [Header("PlayerLocomotion")]
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float crouchSpeed = 7f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpRaycastDistance = 2.3f;
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
    
    private CapsuleCollider playerCollider;
    private float currentStamina;

    private Rigidbody rb;
    private float NormalFOV;
    private float SprintFOVmodifier = 1.5f;


    //AnimatorControllers and booleans
    private Animator anim;
    private bool IsCrouching = false;
    private bool IsSprinting = false;
    private bool IsWalking = false;
    private bool IsJumping = false;

    private void Start()
    {
       
        rb = GetComponent<Rigidbody>();        
        playerCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        //Stamina 
        currentStamina = maxStamina;
        //StaminaSlider.maxValue = maxStamina;
        //stamina slider vlaue is between 0 and 1
        StaminaUiUpdate();
    }


    private void Update()
    {
        Jump();
        //Debug.Log(IsJumping);
    }


    private void FixedUpdate()
    {
        Move();
        Walk();
        Crouching();
        Sprint();
    }



    public  void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal"); //getting the inputs
        float vAxis = Input.GetAxisRaw("Vertical");  // from user

        if (IsSprinting == true) //Determining the speed to be used based on walk, sprint, crouch...
        {

            speed = sprintSpeed;
            IsWalking = false;
        }
        else if (IsCrouching == true)
        {
            speed = crouchSpeed;
            IsWalking = false;

        }
       else if(IsWalking == true)
        {
            speed = walkSpeed;
        }

      

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.fixedDeltaTime; // calculate for new position 
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement); // moves the player to new position
        rb.MovePosition(newPosition);

        UpdateAnimation();
    }

    private void Walk()
    {
        if(hAxis != 0 && vAxis != 0 && IsSprinting == false)
        {
            IsWalking = true;
        }
        else
        {
            IsWalking = false;
        }

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
                IsJumping = true;
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse); // add jumpForce as impulse in Y axis and no effect on X and Z axis
            }
        }
        else
        {
            IsJumping = false;
        }
        
        UpdateAnimation();
    }

    private void Crouching()
    {
        if (Input.GetKey(KeyCode.C))
        {
            IsCrouching = true;
            playerCollider.height = 1;
        }
        else
        {
            IsCrouching = false;
            playerCollider.height = 2;
        }

        UpdateAnimation();
    }

    private void Sprint()
    {
        //write code for stamina handling 
        if ((Input.GetKey(KeyCode.W)) && ((Input.GetKey(KeyCode.LeftShift))  ))
        {
            if(IsGrounded())
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
        UpdateAnimation();


    }

    private void StaminaUiUpdate()
    {
        //write code for stamina ui
        float staminaPercentage = CalculateStaminaPercentage();
        
        //StaminaSlider.value = currentStamina;
        StaminaSliderImage.fillAmount = staminaPercentage / 100;
        StaminaSliderImage.color = Color.Lerp(zeroStaminaColor, maxStaminaColor, staminaPercentage / 100);

        staminaText.text =  ""+currentStamina.ToString("0");
        staminaText.color = Color.Lerp(zeroStaminaColor, maxStaminaColor, staminaPercentage / 100);
       
    }

    private float CalculateStaminaPercentage()
    {
        return ((float)currentStamina / (float)maxStamina) * 100;//casting is used to treat values as floats
    }

    void UpdateAnimation()
    {
        //idle to run  - idleTOrun -- final
        if(IsWalking == false && IsSprinting == true)
        {
            anim.SetBool("idleTOrun", true);
        }
        else
        {
            anim.SetBool("idleTOrun", false);
        }

        //run to run jump  - runTOrunJump --final
        if(IsSprinting == true && IsJumping == true)
        {
            anim.SetBool("runTOrunJump", true);
        }
        else
        {
            anim.SetBool("runTOrunJump", false);
        }

        //run to walk  - runTOwalk -- final 
        if(IsSprinting == false && IsWalking == true)
        {
            anim.SetBool("runTOwalk", true);
        }
        else
        {
            anim.SetBool("runTOwalk", false);
        }

        //run to idle - runTOidle -- final 
        if(IsSprinting == false && IsWalking == false)
        {
            anim.SetBool("runTOidle", true);
        }
        else
        {
            anim.SetBool("runTOidle", false);
        }

        //idle to walk - idleTOwalk -- final
        if (IsWalking ==true && IsSprinting == false)
        {
            anim.SetBool("idleTOwalk", true);
        }
        else
        {
            anim.SetBool("idleTOwalk", false);
        }

        //walk to run jump  - walkTORunJump -- final
        if (IsWalking == true && IsJumping ==true)
        {
            anim.SetBool("runTOrunJump", true);
        }
        else
        {
            anim.SetBool("runTOrunJump", false);
        }

        //walk to run - walkTOrun -- final 
        if(IsWalking == false && IsSprinting == true)
        {
            anim.SetBool("walkTOrun", true);
        }
        else
        {
            anim.SetBool("walkTOrun", false);
        }

        //walk to idle  - walkTOidle -- final
        if(IsWalking == false && IsSprinting == false)
        {
            anim.SetBool("walkTOidle", true);
        }
        else
        {
            anim.SetBool("walkTOidle", false);
        }

        //idle to stand jump - idleTostandJump -- final
        if(IsWalking == false && IsSprinting == false && IsJumping == true && IsGrounded())
        {
            anim.SetBool("idleTostandJump", true);
        }
        else
        {
            anim.SetBool("idleTostandJump", false);
        }

        //stand to crouch idle - idleTOcrouchIdle
        

        //Crouch idle - crouchIdle

        //crouch idle to crouch walk  - crouchIdleTOcrouchWalk

        //crouch to stand - crouchTOstand


    }

}

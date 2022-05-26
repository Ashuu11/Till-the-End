using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControllerFPS : MonoBehaviour
{
    [SerializeField] private float lookSenstivity = 2f;
    [SerializeField] private float smoothing = 2f;
    [SerializeField] private int maxLookUpAngle = 85;

    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPos;


    private void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked; //cursor locked and hidden 
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
        
    }

    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")); //takes mouse inputs

        inputValues = Vector2.Scale(inputValues, new Vector2(lookSenstivity * smoothing, lookSenstivity * smoothing)); //scale the mouse values 
        
        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing); //smooth velocity of moving screen to prevent
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing); // sudden frame change

        currentLookingPos += smoothedVelocity; // changing the frame after smoothing 

        currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -maxLookUpAngle, maxLookUpAngle); // restrict vertical view angle
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos.y, Vector3.right); // rotates camera only vertically
        player.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, player.transform.up); // rotates player as well as camera horizontally(camera is child)
        
    } 

    

    
}

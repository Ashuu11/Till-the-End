using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    /* [SerializeField] float CameraMoveSpeed = 120.0f;
     [SerializeField] GameObject CameraFollowObject; //target to follow  
     Vector3 FollowPos;
     [SerializeField] float ClampAngle = 85.0f;
     [SerializeField] float inputSenstivity = 150.0f;

     [SerializeField] GameObject CameraObj;
     [SerializeField] GameObject PlayerObj;

     [SerializeField] float CamDistanceXToPlayer;
     [SerializeField] float CamDistanceYToPlayer;
     [SerializeField] float CamDistanceZToPlayer;

     [SerializeField] float SmoothX;
     [SerializeField] float SmoothY;

     private float mouseX;
     private float mouseY;
     private float FinalInputX;
     private float FinalInputZ;
     private float rotY = 0f;
     private float rotX = 0f;

     Vector3 offset;



     void Start()
     {
         Vector3 rot = transform.localRotation.eulerAngles;
         rotY = rot.y;
         rotX = rot.x;

         Cursor.lockState = CursorLockMode.Locked; //locks the cursor 
         Cursor.visible = false;


     }

     // Update is called once per frame
     void Update()
     {
         //setup the rotation of the sticks here 
         //go to project settings and input manager and add two more stick variables as done in the project settings 

         float inputX = Input.GetAxis("RightStickHorizontal");
         float inputZ = Input.GetAxis("RightStickVertical");

         mouseX = Input.GetAxis("Mouse X");
         mouseY = Input.GetAxis("Mouse Y");

         FinalInputX = inputX + mouseX;
         FinalInputZ = inputZ + mouseY;

         rotY += FinalInputX * inputSenstivity * Time.deltaTime;
         rotX += FinalInputZ * inputSenstivity * Time.deltaTime;

         rotX = Mathf.Clamp(rotX, -ClampAngle, ClampAngle);

         Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
         transform.rotation = localRotation;

     }

     private void LateUpdate()
     {
         CameraUpdater();
     }

     void CameraUpdater()
     {
         Transform target = CameraFollowObject.transform;

         //move toward target object that we just created above


         float step = CameraMoveSpeed * Time.deltaTime;
         Debug.Log(step);
         transform.position = Vector3.MoveTowards(transform.position, target.position , step);


     }


    /* public GameObject target;
     public float rotateSpeed = 5;
     Vector3 offset;

     void Start()
     {
         offset = target.transform.position - transform.position;
     }

     void LateUpdate()
     {
         float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
         target.transform.Rotate(0, horizontal, 0);

         float desiredAngle = target.transform.eulerAngles.y;
         Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
         transform.position = target.transform.position - (rotation * offset);

         transform.LookAt(target.transform);
     }*/

    [SerializeField] Transform PlayerPos;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - PlayerPos.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerPos.position + offset, 0.1f);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolveAround : MonoBehaviour
{
    /*public GameObject orb; //target or center around which u want to revolve the other object 
    public float radius;
    public float radiusSpeed;
    public float rotationSpeed;
    [SerializeField] float clampAngle = 80.0f;

    private Transform centre;
    private Vector3 desiredPos;
    private Vector3 desiredPos1;

    void Start()
    {
        centre = orb.transform; // defining the center around which we want to rotate 
        transform.position = (transform.position - centre.position).normalized * radius + centre.position;
    }

    void Update()
    {
        //horizontal rotation
        float rotationX = Input.GetAxis("Mouse X") * -rotationSpeed;
        transform.RotateAround(centre.position, Vector3.up, rotationX);

        desiredPos = (transform.position - centre.position).normalized * radius + centre.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPos, radiusSpeed * Time.deltaTime);



        //Vertical Rotation
        float rotationY = Input.GetAxis("Mouse Y") * -rotationSpeed;

        transform.RotateAround(centre.position, Vector3.right, Mathf.Clamp(rotationY, -clampAngle, clampAngle));
      

        

        desiredPos1 = (transform.position - centre.position).normalized * radius + centre.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPos1, radiusSpeed * Time.deltaTime);
    }*/
    /*
     steps 
    take the player as center 
    revolve the camera around the player in horizontal axis 
        
    revolve the camera arount the player in vertical axis with clamped angles
     */


    /**
     * this script should be attached to the camera and that camera should be child of an empty game object for the pivot to camera
     * however the pivot is individual object and not hte child of the camera(camera is child of pivot , pivot is child of no one , player is child of no one)
     * the camera will now follow the player and the follow script should be written and attached to the pivot object
     * the pivot element has to be centered at the player and should has offset 000
     */

    protected Transform _XForm_Camera; //for camera
    protected Transform _XForm_Parent; //For the pivot object

    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 6f;

    [SerializeField] Transform PlayerTransform; //takes transform data of the player
    [SerializeField] float MouseSensitivity = 4f;
    [SerializeField] float ScrollSensitvity = 2f; //scroll speed 
    [SerializeField] float OrbitDampening = 10f;
    [SerializeField] float ScrollDampening = 6f;
    [SerializeField] float minCamAngle = 5f;
    [SerializeField] float maxCamAngle = 85f;
    [SerializeField] float minCamZoom = 2f;
    [SerializeField] float maxCamZoom = 6f;
   

    // Use this for initialization
    void Start()
    {
        this._XForm_Camera = this.transform; //getting the transform of camera 
        this._XForm_Parent = this.transform.parent; //getting the transform of parent of camera i.e., pivot object

        //Locking the cursor and hiding it 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
    }


    void LateUpdate()
    {
        //Rotation of the Camera based on Mouse Coordinates
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity; //getting the mouse inputs as we move the mouse on screen 
            _LocalRotation.y += Input.GetAxis("Mouse Y") * MouseSensitivity;

            //Clamp the y Rotation to horizon and not flipping over at the top
           _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, minCamAngle, maxCamAngle);

            /* or we can also clamp the rotation as below 
            if (_LocalRotation.y < minCamAngle)
                _LocalRotation.y = minCamAngle;
            else if (_LocalRotation.y > maxCamAngle)
                _LocalRotation.y = maxCamAngle;*/
        }
        //Zooming Input from our Mouse Scroll Wheel
        //Zooms in or out to bring the camera close to the pivot object
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

            ScrollAmount *= (this._CameraDistance * 0.3f);

            this._CameraDistance += ScrollAmount * -1f;

            this._CameraDistance = Mathf.Clamp(this._CameraDistance, minCamZoom, maxCamZoom);
        }

        

        //Actual Camera Rig Transformations
        
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        
        //using local rotation x and y to set the pitch in yaw
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening); //rotates the parent(pivot) to have a camera revolving effect


        if(Input.GetKey(KeyCode.W))
        {
            //only when the player tries to move forward then we have to face the player in the direction of camera
            //next two lines rotates the player face to face the direction of camera only 

            Quaternion playerQT = Quaternion.Euler(0, _LocalRotation.x, 0);
            this.PlayerTransform.rotation = Quaternion.Lerp(this.PlayerTransform.rotation, playerQT, Time.deltaTime * OrbitDampening);

        }

        

        //for scrolling 
        if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
        {
            this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }

}

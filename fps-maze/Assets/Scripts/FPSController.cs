using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    //movement Variables
    public float walkSpeed = 7f;
    public float sprintSpeed = 12f;
    public float jumpSpeed = 7f;
    public float gravity = -9.81f;


    //camera Variables
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookLimitX = 90f;

    //bow variables (removed)
    // public Transform bowTransform;
    // //controls angle to front of character
    // public float bowAngle = -110f;

    //Public keycode for unlocking the mouse
    public KeyCode unlockMouseKey = KeyCode.Delete;

    //keycode for sprint modifer key
    public KeyCode sprintKey = KeyCode.LeftShift;

    //Private Variables
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX;
    float movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //find the character controller on the gameObject, our movement script here will use functionality from the character controller component to move
        characterController = GetComponent<CharacterController>();
        movementSpeed = walkSpeed;
        //locks the cursor to the center of the game window and hides it so it looks more like an fps
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        MouseLockControl();

        SprintControl();

        MovePlayer();
        

        RotateCamera();

    }

    void MouseLockControl() {
        //let players get their mouse back if they want
        if (Input.GetKeyDown(unlockMouseKey)) {
            // toggle mouse lock with unlockMouseKey
            if (Cursor.lockState == CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Cursor.visible = !Cursor.visible;
        }
    }

    void RotateCamera() {
        //calculate where our camera should rotate based on mouse input
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        //restrict how high or low the camera can rotate
        rotationX = Mathf.Clamp(rotationX, -lookLimitX, lookLimitX);
        //rotate the camera to match vertical mouse input, when using Quaternion.Euler the rotation is applied around the given axis not to it, this is why x and y appear to be flipped
        //hand pneumonic device
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        //removed bow usage
        //set bow transform to follow camera, x rotation of mouse is mapped to -z on the bow, 
        // bowTransform.localRotation = Quaternion.Euler(0, bowAngle, -rotationX);


        /*rotate our character to match horizontal mouse input, we use multiply here because of the nature of quaternions. You can't use addition to add one to the other. 
        to combine 2 quaternions like we want here (our original quaternion rotation + the rotation we want to turn to based on mouse input) we multiply the original by the second*/
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }


    void SprintControl() {
        if (Input.GetKey(sprintKey)){
            movementSpeed = sprintSpeed;
        } else {
            movementSpeed = walkSpeed;
        }
    }
    void MovePlayer() {
         //Local Vector Variables used to store 
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Local float Variables to calculate how fast we should move both forward and side to side based on player input
        float curSpeedX = movementSpeed * Input.GetAxis("Vertical");
        //no sideways sprinting
        float curSpeedZ = walkSpeed * Input.GetAxis("Horizontal");

        //local float variable to store the current veritcal direction of our player
        float jumpDirectionY = moveDirection.y;

        //calculate movement vector based on our speed variables for moving forward and side to side 
        moveDirection = (forward * curSpeedX) + (right * curSpeedZ);

        //adds vertical movement to our player if the player is on the ground and pressed the jump button
        if (Input.GetButton("Jump") && characterController.isGrounded) {
            moveDirection.y = jumpSpeed;
        } else {
            //stops adding vertical movement while the player is not jumping
            moveDirection.y = jumpDirectionY;
        }
        //if the player is not on the ground subtracts our gravity force from vertical movement. This will allows for the player jump to slowly reach a peak and then return to the ground over time
        // if (!characterController.isGrounded) {
            moveDirection.y += gravity * Time.deltaTime;
        // } else 
        if (characterController.isGrounded && moveDirection.y < 0) {
            //resets vertical movement to 0 after landing
            moveDirection.y = 0;
        }

        //apply our final move direction to the player in game using the built in characterController move funciton
        characterController.Move(moveDirection * Time.deltaTime);
    }

    
}

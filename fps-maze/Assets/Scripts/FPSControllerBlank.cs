using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerBlank : MonoBehaviour
{
    //movement Variables


    //camera Variables


    //Public keycode for unlocking the mouse


    //Private Variables


    // Start is called before the first frame update
    void Start()
    {
        //find the character controller on the gameObject, our movement script here will use functionality from the character controller component to move


        //locks the cursor to the center of the game window and hides it so it looks more like an fps

    }

    // Update is called once per frame
    void Update()
    {
        //let players get their mouse back if they want


        //Local Vector Variables used to store 


        //Local float Variables to calculate how fast we should move both forward and side to side based on player input


        //local float variable to store the current veritcal direction of our player


        //calculate movement vector based on our speed variables for moving forward and side to side 


        //adds vertical movement to our player if the player is on the ground and pressed the jump button

        //stops adding vertical movement while the player is not jumping


        //if the player is not on the ground subtracts our gravity force from vertical movement. This will allows for the player jump to slowly reach a peak and then return to the ground over time

        //resets vertical movement to 0 after landing


        //apply our final move direction to the player in game using the built in characterController move funciton


        //calculate where our camera should rotate based on mouse input

        //restrict how high or low the camera can rotate

        //rotate the camera to match vertical mouse input, when using Quaternion.Euler the rotation is applied around the given axis not to it, this is why x and y appear to be flipped
        //hand pneumonic device

        /*rotate our character to match horizontal mouse input, we use multiply here because of the nature of quaternions. You can't use addition to add one to the other. 
        to combine 2 quaternions like we want here (our original quaternion rotation + the rotation we want to turn to based on mouse input) we multiply the original by the second*/


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//handles collision events for collisions with the player character controller
public class PlayerCollisionController : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit other) {
        // Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Flag"){
            // Debug.Log(other.gameObject.tag);
            GameEvents.current.FlagPickup();
        }

        if (other.gameObject.tag == "Start"){
            // Debug.Log(other.gameObject.tag);
            GameEvents.current.TriggerStart();
        }
    }
}

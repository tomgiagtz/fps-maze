using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    private void OnControllerColliderEnter(ControllerColliderHit other) {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
            Debug.Log("{ickup");
    }
}

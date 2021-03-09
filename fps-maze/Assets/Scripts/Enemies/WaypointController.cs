using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public int enemyLayerId;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == enemyLayerId ) {
            other.gameObject.GetComponent<NavMeshController>().OnEnterWayPoint();
        }
    }
}

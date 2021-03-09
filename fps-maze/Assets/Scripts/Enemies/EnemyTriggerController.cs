using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerController : MonoBehaviour
{

    NavMeshController meshContr;

    private void Awake() {
        meshContr = GetComponentInParent<NavMeshController>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            Vector3 target = other.transform.position;
            meshContr.OnStartDetect(target);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            meshContr.OnEndDetect();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchController : MonoBehaviour
{

    public float shootRange = 5;
    public LayerMask hitMask;
    public Camera cam;

    public Color defaultCol, hitCol;
    public Image reticle;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        CastRay();

    }

    void CastRay() {
        RaycastHit hit;

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));


        Debug.DrawRay(rayOrigin, cam.transform.forward * shootRange, hitCol, 0.1f);
        if (Input.GetButtonDown("Fire1")) {
            // Debug.Log(hit);
            // Debug.Log(hasHit);

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, shootRange, hitMask, QueryTriggerInteraction.Ignore)) {
            Debug.Log(hit.transform.gameObject.tag);
                Debug.Log("HIT");
                hit.transform.gameObject.GetComponent<EnemyController>().OnHit();
                // Debug.Log(enemy);
            }
        }
    }
}

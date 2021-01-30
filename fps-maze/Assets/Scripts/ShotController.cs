using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    //Variables
    public GameObject projectile;
    public Transform muzzlePoint;
    public GameObject projectileParent;
    public float projectileLifespan = 5f;
    public float projectileSpeed = 150f;
    public float minSpeed = 150f;
    public float maxSpeed = 1500f;

    // Update is called once per frame
    // void Update()
    // {
    //     //check if player hit our shoot button
    //     if (Input.GetMouseButtonDown(0)) {
            
    //     }
    // }

    public void FireShot(float _projectileSpeed) {
        //instantiate our prefab projectile
            GameObject currProjectile = Instantiate(projectile , muzzlePoint.position, muzzlePoint.rotation);
            //set the parent of the projectile to a null object so it is not impaced by our character movement
            currProjectile.transform.SetParent(projectileParent.transform);
            //add force to the projectile
            currProjectile.GetComponent<Rigidbody>().AddForce(muzzlePoint.right * _projectileSpeed);
            //destroy the projectile after time has passed
            Destroy(currProjectile, projectileLifespan);
    }

    public void FireShot() {
        FireShot(projectileSpeed);
    }
}

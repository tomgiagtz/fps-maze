using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnContact : MonoBehaviour
{
    //declare our list of strings we'll use if we want our projectile to destroy an object it hits
    public List<string> destroyableObjects = new List<string>();

    private void OnCollisionEnter(Collision collision)
    {
        //run through our list of tags we want to destroy
        for (int i = 0; i < destroyableObjects.Count; i++)
        {
            //check if the hit object has one of our tags
            if(collision.gameObject.tag == destroyableObjects[i])
            {
                //destroy the hit object and the projectile

            }
        }
    }
}

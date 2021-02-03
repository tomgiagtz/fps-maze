using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{

    private void Start()
    {
        // originY = transform.position.y;
        //subscribe to onCoinPickup event
        GameEvents.current.onFlagPickup += HandleFlagPickup;
    }
    private void OnDisable()
    {
        //unsubscribe from coin pick up event when object is disabled
        GameEvents.current.onFlagPickup -= HandleFlagPickup;
    }
    private void HandleFlagPickup()
    {
        Destroy(gameObject);
    }
}

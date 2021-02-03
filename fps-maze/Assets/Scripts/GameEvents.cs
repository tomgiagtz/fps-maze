using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// big thanks to game dev guide on youtube, triggers are great
// followed along here: https://www.youtube.com/watch?v=gx0Lt4tCDE0
public class GameEvents : MonoBehaviour
{
    //singleton GameEvents object placed in scene
    public static GameEvents current;
    void Awake()
    {
        //set current to this instance
        current = this;
    }

    //create on coin pickup event
    public event Action onFlagPickup;
    //function to be run when coin pickup event is triggered
    public void FlagPickup()
    {   
        //if there are listeners to this event
        if (onFlagPickup != null)
        {
            onFlagPickup();
        }
    }

    //very similarly, create events for enemy range enter and exit
    public event Action<int> onEnemyRangeEnter;
    public void EnemyRangeEnter(int id) {
        if (onEnemyRangeEnter != null) {
            onEnemyRangeEnter(id);
        }
    }

    // hope you get it by now :)
    public event Action<int> onEnemyRangeExit;
    public void EnemyRangeExit(int id) {
        if (onEnemyRangeExit != null) {
            onEnemyRangeExit(id);
        }
    }
}

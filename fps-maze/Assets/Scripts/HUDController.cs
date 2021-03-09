using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class HUDController : MonoBehaviour
{
    public Image flagIcon;

    public TextMeshProUGUI tipsText;
    private bool flagCollected = false;
    public ParticleSystem beaconParticles;
    public Transform beaconPosition;

    private string[] tips = new string[]{"Look for the flag", "Find your way back to the start, watch out for the turrets!", "You Win!"};
    private string controls  = "Shift to run, RMB to draw and LMB to shoot, Move with WASD and Space to Jump";

    private int tipsIndex = 0;


    private bool showingControls = true;
    public float controlsCooldownTime = 5f;
    private float currCooldown = 0f;
    private ParticleSystem beacon;
    private void OnDisable() {
        GameEvents.current.onFlagPickup -= HandleFlagPickup;
        GameEvents.current.onTriggerStart -= HandleTriggerStart;
    }
    void Start()
    {
        GameEvents.current.onTriggerStart += HandleTriggerStart;
        GameEvents.current.onFlagPickup += HandleFlagPickup;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (tipsIndex == 2 || currCooldown > controlsCooldownTime) {
            showingControls = false;
        } else {
            showingControls = true;
        }


        if (showingControls) {
            tipsText.SetText(controls);
            currCooldown += Time.deltaTime;
        } else {
            tipsText.SetText(tips[tipsIndex]);
        }

        flagIcon.gameObject.SetActive(flagCollected);

        
    }

    void HandleFlagPickup() {
        Debug.Log("Hehe");
        flagCollected = true;
        tipsIndex = 1;
        beacon = Instantiate(beaconParticles, beaconPosition);
    }

    void HandleTriggerStart() {
        if (flagCollected) {
            tipsIndex = 2;
            beacon.Stop();
            beacon.Clear();
        }
        showingControls = true;
        currCooldown = 0f;
    }
}

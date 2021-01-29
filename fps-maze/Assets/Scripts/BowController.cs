using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{


    //speeds for each stage of animation
    public float drawSpeed = 3f;
    public float releaseSpeed = 10f;
    public float fireSpeed = 40f;
    public float recoilSpeed = 80f;
    

    // blend ratios for drawing and firing
    [Range(0,100)] public float drawBlend = 0f;
    [Range(0,100)] public float recoilBlend = 0f;

    //IDs for blendShapes on the bow model
    private int drawID = 1;
    private int recoilID = 0;

    //States to keep track of how we should animate
    enum BowStates {Idling, Drawing, Releasing, Firing, Recoiling};
    BowStates currState = BowStates.Idling;
    //keep trakc of previous state for debug purposes
    BowStates prevState = BowStates.Idling;


    //keycodes for drawing and firing
    public KeyCode fireButton = KeyCode.Mouse0;
    public KeyCode drawButton = KeyCode.Mouse1;
    //mesh component for bow model, needed to control blend shapes
    SkinnedMeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        prevState = currState;

        //delegate to functions for input and handling each type of animation
        HandleInput();
        DrawBow();
        FireBow();
        RecoilBow();

        //master set on mesh for blendshapes
        mesh.SetBlendShapeWeight(drawID, drawBlend);
        mesh.SetBlendShapeWeight(recoilID, recoilBlend);


        //debug logging
        // if (currState != prevState) {
        //     Debug.Log(currState);
        // }

    }
    // Handle drawing animation
    void DrawBow() {
        //blend towards full draw if currently drawing
        if (currState == BowStates.Drawing) {
            drawBlend = Mathf.Lerp(drawBlend, 100f, Time.deltaTime * drawSpeed);
            //stop lerp from fluctuating at max
            if (drawBlend > 99f) {
                drawBlend = 100f;
            }
            // mesh.SetBlendShapeWeight(drawID, drawBlend);
        } else  { // lerp back to idle slowly, no fire, just release
            drawBlend = Mathf.Lerp(drawBlend, 0f, Time.deltaTime * releaseSpeed);
            //stop lerp from fluctuating at min
            if (drawBlend < 1f) {
                drawBlend = 0f;
                // currState = BowStates.Recoiling;
            }
            // mesh.SetBlendShapeWeight(drawID, drawBlend);
        }
    }

    // Handle firing animation and call fire function
    void FireBow() {
        if (currState != BowStates.Firing) return;

        // Debug.Log("Shwip");
        //if bow has been drawn before firing
        if (drawBlend > 1f) {
            //play fire animation, at firing speed
            drawBlend = Mathf.Lerp(drawBlend, 0f, Time.deltaTime * fireSpeed);
        } else {
            // when bow has gone back to no blend, start recoil animation and fire projectile
            drawBlend = 0;
            Debug.Log("FIRE!");
            currState = BowStates.Recoiling;
        }
        
    }

    //handle recoil to max then back to zero
    bool isRecoilIncreasing = true;
    void RecoilBow() {
        if (currState == BowStates.Recoiling) {

            if (isRecoilIncreasing) { //if animating toward max recoil shape
                recoilBlend = Mathf.Lerp(recoilBlend, 100f, Time.deltaTime * recoilSpeed);
                //if recoil has reach max, set isRecoilIncreasing false
                if (recoilBlend > 99f) {
                    recoilBlend = 100f;
                    isRecoilIncreasing = false;
                }
            } else { // else animate towards no blend shape
                recoilBlend = Mathf.Lerp(recoilBlend, 0f, Time.deltaTime * recoilSpeed);
                if (recoilBlend < 1f) {
                    //stop lerp from fluctuating at min
                    recoilBlend = 0f;
                    //once back to no blend, go back to idling, reset recoil bool for next Recoil
                    currState = BowStates.Idling;
                    isRecoilIncreasing = true;
                }
            }
        }
    }

    //master input control
    void HandleInput() {

        //animation are over if blend shapes are ~= 0
        bool animationsOver = drawBlend < 1f && recoilBlend < 1f;
        //animating towards idle if Recoil or Release is playing
        bool animatingTowardsIdle = currState == BowStates.Releasing || currState == BowStates.Recoiling;


        // if both  of the above are true, then we should be in idle
        if (animatingTowardsIdle && animationsOver) {
            currState = BowStates.Idling;
        }
        
        //if holding the draw button, go into drawing state
        if (Input.GetKeyDown(drawButton)) {
            currState = BowStates.Drawing;
            // Debug.Log(currState);
        }
        //if let go of drawing button, play releasing, ignore if bow has not been drawn previously
        if (Input.GetKeyUp(drawButton) && drawBlend > 0) {
            currState = BowStates.Releasing;
            // Debug.Log(currState);
        }
        // play firing animation
        if (Input.GetKeyDown(fireButton)) {
            currState = BowStates.Firing;
            // Debug.Log(currState);
        }
    }
}

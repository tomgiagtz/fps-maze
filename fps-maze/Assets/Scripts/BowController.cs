using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{

    public float drawSpeed = 3f;
    public float releaseSpeed = 10f;
    public float fireSpeed = 40f;
    public float recoilSpeed = 80f;
    [Range(0,100)] public float drawBlend = 0f;
    [Range(0,100)] public float recoilBlend;
    private int drawID = 1;
    private int recoilID = 0;
    enum BowStates {Idling, Drawing, Releasing, Firing, Recoiling};
    BowStates currState = BowStates.Idling;
    BowStates prevState = BowStates.Idling;

    public KeyCode fireButton = KeyCode.Mouse0;
    public KeyCode drawButton = KeyCode.Mouse1;
    private bool drawingBow = false;
    private bool isFiring = false;
    public bool bowFullyDrawn = false;
    // private bool releasedfinished = true;
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
        bowFullyDrawn = drawBlend >= 99.9f;


        HandleInput();
        DrawBow();
        FireBow();
        RecoilBow();

        mesh.SetBlendShapeWeight(drawID, drawBlend);
        mesh.SetBlendShapeWeight(recoilID, recoilBlend);

        if (currState != prevState) {
            Debug.Log(currState);
        }

    }
    // float currBlend;
    void DrawBow() {
        //step out if currently animating for firing
        if (currState == BowStates.Drawing) {
            drawBlend = Mathf.Lerp(drawBlend, 100f, Time.deltaTime * drawSpeed);
            if (drawBlend > 99f) {
                drawBlend = 100f;
            }
            // mesh.SetBlendShapeWeight(drawID, drawBlend);
        } else  {
            drawBlend = Mathf.Lerp(drawBlend, 0f, Time.deltaTime * releaseSpeed);
            if (drawBlend < 1f) {
                drawBlend = 0f;
                // currState = BowStates.Recoiling;
            }
            // mesh.SetBlendShapeWeight(drawID, drawBlend);
        }
    }

    void FireBow() {
        if (currState != BowStates.Firing) return;

        // Debug.Log("Shwip");
        if (drawBlend > 1f) {
            drawBlend = Mathf.Lerp(drawBlend, 0f, Time.deltaTime * fireSpeed);
        } else {
            drawBlend = 0;
            Debug.Log("FIRE!");
            currState = BowStates.Recoiling;
        }
        
    }
    bool isRecoilIncreasing = true;
    void RecoilBow() {
        if (currState == BowStates.Recoiling) {
            // Debug.Log("Recoil");
            
            // recoilBlend = Mathf.Lerp(recoilBlend, 100f, Time.deltaTime * recoilSpeed);


            if (isRecoilIncreasing) {
                recoilBlend = Mathf.Lerp(recoilBlend, 100f, Time.deltaTime * recoilSpeed);

                if (recoilBlend > 99f) {
                    recoilBlend = 100f;
                    isRecoilIncreasing = false;
                }
            } else {
                recoilBlend = Mathf.Lerp(recoilBlend, 0f, Time.deltaTime * recoilSpeed);
                if (recoilBlend < 1f) {
                    recoilBlend = 0f;
                    currState = BowStates.Idling;
                    isRecoilIncreasing = true;
                }
            }
        }
    }

    void HandleInput() {
        bool animationsOver = drawBlend < 1f && recoilBlend < 1f;
        bool animatingTowardsIdle = currState == BowStates.Releasing || currState == BowStates.Recoiling;

        if (animatingTowardsIdle && animationsOver) {
            Debug.Log("Anim Over");
            Debug.Log(animationsOver);
            Debug.Log(recoilBlend);
            currState = BowStates.Idling;
        }
        
        if (Input.GetKeyDown(drawButton)) {
            currState = BowStates.Drawing;
            // Debug.Log(currState);
        }

        if (Input.GetKeyUp(drawButton) && drawBlend > 0) {
            currState = BowStates.Releasing;
            // Debug.Log(currState);
        }

        if (Input.GetKeyDown(fireButton)) {
            currState = BowStates.Firing;
            // Debug.Log(currState);
        }
    }
}

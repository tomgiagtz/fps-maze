using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Variable for our look at target
    public Transform target;
    public GameObject muzzle;

    public ParticleSystem explosion;
    public bool engaged = false;
    private ShotController shotController;
    public float coolDownTime = 3f;
    private float currCoolDown = 0f;

    private ParticleSystem particles;
    public Material lampMat;

    [ColorUsage(true, true)]
    public Color32 litColor, unlitColor;
    private bool wasHit = false;
    
    // private bool flagPickedUp = false;
    private void Start() {
        GameEvents.current.onFlagPickup += HandleFlagPickup;
        shotController = GetComponent<ShotController>();
        particles = GetComponentInChildren<ParticleSystem>();
        litColor = lampMat.GetColor("_EmissionColor");
    }

    private void OnDisable() {
        GameEvents.current.onFlagPickup -= HandleFlagPickup;
        // GameEvents.current.onTriggerStart -= HandleTriggerStart;
    }
    // Update is called once per frame
    void Update()
    {
        if (engaged) {
            particles.Play();
        } else {
            particles.Stop();
        }
        currCoolDown += Time.deltaTime;


        muzzle.transform.LookAt(target);
        if (engaged && currCoolDown > coolDownTime) {
            shotController.FireShot();
            currCoolDown = 0;
        }
        //tell gameobject to look at the target
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Arrow" && engaged){
            Instantiate(explosion, transform);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void Disengage() {
        engaged = false;

    }

    public void Engage() {
        if (!wasHit) {
            engaged = true;
            lampMat.SetColor("_EmissionColor", litColor);
        }
        
    }

    public void OnHit() {
        Disengage();
        lampMat.SetColor("_EmissionColor", unlitColor);

        wasHit = true;
        StartCoroutine(Death());
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(2);

        Destroy(this.gameObject);
    }



    void HandleFlagPickup() {
        // engaged = true;
    }

    
}

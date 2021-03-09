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
    MeshRenderer meshRenderer;
    Material localMat;

    [ColorUsage(true, true)]
    public Color litColor, unlitColor;
    private bool wasHit = false;
    
    
    // private bool flagPickedUp = false;
    private void Start() {
        GameEvents.current.onFlagPickup += HandleFlagPickup;
        shotController = GetComponent<ShotController>();
        particles = GetComponentInChildren<ParticleSystem>();
        localMat = new Material(lampMat);
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.materials[1] = localMat;

        localMat.SetColor("_EmissionColor", litColor);
        particles.Stop();
    }

    private void OnDisable() {
        GameEvents.current.onFlagPickup -= HandleFlagPickup;
        // GameEvents.current.onTriggerStart -= HandleTriggerStart;
    }
    // Update is called once per frame
    void Update()
    {
        // if (engaged) {
        //     particles.Play();
        // } else {
        //     particles.Stop();
        // }
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
        particles.Stop();

    }

    public void Engage() {
        if (!wasHit) {
            particles.Play();
            engaged = true;
            localMat.SetColor("_EmissionColor", litColor);
        }
        
    }

    public void OnHit() {
        Disengage();
        localMat.SetColor("_EmissionColor", unlitColor);

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static ILogger logger = Debug.unityLogger;

    public Light light;
    public bool enableLightSystem = true;
    public float lightTick = 0.15f;
    public TMP_Text textMeshPro;
    public GameObject shark;
    public Camera camera;

    private float lastLightEvent = 0f;
    private bool isRescued = false;
    private bool isSharkAppeared = false;
    private float minSharkPositionY = -20f;

    public bool IsSharkAppeared(){ return this.isSharkAppeared;}

    // Start is called before the first frame update
    void Start()
    {
        this.lastLightEvent = Time.time;

        if(this.textMeshPro != null) this.textMeshPro.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(Mathf.Abs(Time.time - this.lastLightEvent) > this.lightTick){
            this.DecreaseLight();
        }

        if(this.isSharkAppeared == false && this.light.intensity < 0.2f){
            this.isSharkAppeared = true;
            this.spawnShark();
        }else if(this.isSharkAppeared){

        }
    }

    public void OnAddLight(InputAction.CallbackContext value){
        this.IncreaseLight();
    }

    public void IncreaseLight(){
        if(this.enableLightSystem){
            this.light.intensity = Mathf.Min(1f, this.light.intensity+0.1f);
            this.light.range = Mathf.Min(200f, this.light.range+ 10f);
            this.lastLightEvent = Time.time;
        }
    }

    public void DecreaseLight(){
        if(this.enableLightSystem){
            this.light.intensity = Mathf.Max(0f, this.light.intensity-0.01f);
            this.light.range = Mathf.Max(15f, this.light.range-0.01f);
            this.lastLightEvent = Time.time;
        }
    }

    void OnCollisionEnter(Collision collision){
        GameObject hitObject = collision.gameObject;

        if(hitObject.name == "Diver"){
            Destroy(hitObject);
            this.isRescued = true;
            this.textMeshPro.enabled = true;
            logger.Log("Collision", "Diver has been rescued, return to base");
        }
    }

    private void spawnShark(){
        this.shark.transform.position = this.camera.transform.position + this.camera.transform.forward * 125;

        if(this.shark.transform.position.y < this.minSharkPositionY){
            this.shark.transform.position = new Vector3(this.shark.transform.position.x, this.minSharkPositionY, this.shark.transform.position.z);
        }

        this.shark.transform.LookAt(this.camera.transform);
        this.camera.transform.LookAt(this.shark.transform);
        this.shark.GetComponent<Rigidbody>().AddForce(this.shark.transform.forward * 2000f);
    }
    
    void OnTriggerEnter(Collider collider) {
        GameObject hitObject = collider.gameObject;
        
        if(hitObject.tag == "Fish")
        {
            StartCoroutine(gameObject.GetComponent<FlashLight>().handleFlashLight());
            this.IncreaseLight();
            gameObject.GetComponent<SpawnFoes>().removeSchoolFishNumber();
            Destroy(hitObject.transform.parent.gameObject);
        } else if (hitObject.tag == "SeaWeed")
        {
            StartCoroutine(gameObject.GetComponent<FlashLight>().handleFlashLight());
            this.IncreaseLight();
            Destroy(hitObject);
        } else if(hitObject.name == "WhiteShark") {
            SceneManager.LoadScene(4);
        } else if(this.isRescued == true && hitObject.name == "SeabaseCollider"){
            this.textMeshPro.enabled = false;
            SceneManager.LoadScene(3);
            logger.Log("Collision", "Welcome back to the Sea base");
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private static ILogger logger = Debug.unityLogger;

    public Light light;
    public bool enableLightSystem = true;
    public float lightTick = 0.15f;

    private float lastLightEvent = 0f;
    private bool isRescued = false;

    // Start is called before the first frame update
    void Start()
    {
        this.lastLightEvent = Time.time;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(Mathf.Abs(Time.time - this.lastLightEvent) > this.lightTick){
            this.DecreaseLight();
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
            logger.Log("Collision", "Diver has been rescued, return to base");
        }else if(this.isRescued == true && hitObject.name == "SeabaseCollider"){
            logger.Log("Collision", "Welcome back to the Sea base");
        }
    }
}

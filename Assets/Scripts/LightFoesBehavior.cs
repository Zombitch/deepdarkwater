using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFoesBehavior : MonoBehaviour
{
    public GameObject player;
    Light foesLight;
    Light playerLight;
    
    void Start()
    {
        foesLight = GetComponent<Light>();
        playerLight = player.GetComponent<Light>();
    }

    /**
    void Update()
    {
        foesLight.intensity = Mathf.PingPong(Time.time, 8);
    }
    **/
    
    void OnTriggerEnter (Collider other)
    {
        Debug.Log("colide");
        if (other.gameObject == player)
        {
            playerLight.intensity += foesLight.intensity;
            foesLight.intensity = 0;
        }
    }
    
    
}

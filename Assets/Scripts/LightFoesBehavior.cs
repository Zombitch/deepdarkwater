using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFoesBehavior : MonoBehaviour
{
    public GameObject player;
    Light foesLight;
    Light playerLight;
    private GameObject parent;

    void Start()
    {
        foesLight = GetComponent<Light>();
        playerLight = player.GetComponent<Light>();
        GameObject parent = GameObject.Find("schoolFish");
    }

    /**
void Update()
{
    foesLight.intensity = Mathf.PingPong(Time.time, 8);
}
**/



}


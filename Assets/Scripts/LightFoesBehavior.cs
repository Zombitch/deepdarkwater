using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFoesBehavior : MonoBehaviour
{
    public GameObject player;
    Light foesLight;
    private GameObject parent;

    void Start()
    {
        foesLight = GetComponent<Light>();
        GameObject parent = GameObject.Find("schoolFish");
    }

    /**
void Update()
{
    foesLight.intensity = Mathf.PingPong(Time.time, 8);
}
**/



}


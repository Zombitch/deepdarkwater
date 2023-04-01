using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFoesBehavior : MonoBehaviour
{
    Light foesLight;

    void Start()
    {
        foesLight = GetComponent<Light>();
    }

    void Update()
    {
        foesLight.intensity = Mathf.PingPong(Time.time, 8);
    }
}

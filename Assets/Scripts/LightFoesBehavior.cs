using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DistantLandsOverride
{
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
    
        void OnTriggerEnter (Collider other)
        {
            Debug.Log(other);   
            Debug.Log("collide");
            if (other.gameObject == player)
            {
                playerLight.intensity += foesLight.intensity;
                foesLight.intensity = 0;
                SpawnFoes.removeShcoolFish(parent);
            }
        }
        
    
    
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damsel : MonoBehaviour
{
    public GameObject[] spawns; 

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(spawns.Length);
        GameObject spawn = spawns[Random.Range(0, spawns.Length)];
        transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z);
        Debug.Log("damsel spawned");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

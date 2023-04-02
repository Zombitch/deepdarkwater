using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnFoes : MonoBehaviour
{
    private float m_currentsSchoolFishNumber = 0f;
    
    public float maxFishs = 3f;

    public GameObject schoolFish;

    public float posY = 0f;
    public float spawnInterval = 5f;

    public void addSchoolFishNumber()
    {
        m_currentsSchoolFishNumber++;
    }
    
    public void removeSchoolFishNumber()
    {
        m_currentsSchoolFishNumber--;
    }

    void Start()
    {
        this.startSpawn();
        InvokeRepeating("SpawnSphereOnEdgeRandomly3D", spawnInterval, spawnInterval);
    }

    void Update()
    {
        
    }

    void startSpawn()
    {
        for (int i = 0; i < maxFishs; i++) 
        {
            this.SpawnSphereOnEdgeRandomly3D();
        }
    }

    void SpawnSphereOnEdgeRandomly3D()
    {
        float radius = 2f;
        Vector3 randomPos = Random.insideUnitSphere * radius;
        randomPos += transform.position;
        randomPos.y = 0f;
        
        Vector3 direction = randomPos - transform.position;
        direction.Normalize();
            
        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);
        
        randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.z;
        randomPos.y = posY;
            
        GameObject go = Instantiate(this.schoolFish, randomPos, Quaternion.identity);
        go.transform.position = randomPos;
        this.addSchoolFishNumber();
    }


}

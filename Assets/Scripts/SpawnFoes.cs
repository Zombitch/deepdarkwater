using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DistantLandsOverride
{
    public class SpawnFoes : MonoBehaviour
    {
        private static List<GameObject> m_currentsSchoolFish;

        public GameObject schoolFish;

        public float posY = 3f;

        public static void addSchoolFish(GameObject schoolFish)
        {
            m_currentsSchoolFish.Add(schoolFish);
        }
        
        public static void removeShcoolFish(GameObject schoolFish)
        {
            m_currentsSchoolFish.Remove(schoolFish);
        }

        void Start()
        {
            m_currentsSchoolFish = new List<GameObject>();
        }

        void Update()
        {
            if (m_currentsSchoolFish.Count == 0)
            {
                SpawnSphereOnEdgeRandomly3D(schoolFish);
            }
        }
    
        void SpawnSphereOnEdgeRandomly3D(GameObject schoolFish)
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
                
            GameObject go = Instantiate(schoolFish, randomPos, Quaternion.identity);
            go.transform.position = randomPos;
            addSchoolFish(go);
        }
        
    
    }

}

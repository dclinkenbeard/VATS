using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    public int spawnCount;
    //public LayerMask obstacleMask;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject agent = Instantiate(agentPrefab,
                transform.position + Random.insideUnitSphere * 3f,
                Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))),
                transform
                );

            //agent.GetComponent<BoidAgent>().obstacleMask = obstacleMask;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCollider : MonoBehaviour
{
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (model.transform.position.y >= -7)
        {
            //Debug.Log("model out");
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == model)
        {
            Debug.Log("gameobject collided");
        }

        if (collision.collider.gameObject == model)
        {
            Debug.Log("gameobject collided");

        }
        if (collision.collider.gameObject.tag == "model")
        {
            Debug.Log("gameobject collided");

        }
    }
}

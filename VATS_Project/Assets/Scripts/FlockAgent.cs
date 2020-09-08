﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    bool lerping;
    Vector3 lerpTo;
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();  
    }

    public void Move(Vector3 velocity) {
        if (transform.forward != velocity)
        {
            lerping = true;
            lerpTo = velocity;
            //transform.forward = velocity;
        }

        transform.position += velocity * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //smooth lerp to direction
        if (lerping) {
            if (transform.forward == lerpTo)
            {
                lerping = false;
            }
            else {
                transform.forward = Vector3.Lerp(transform.forward, lerpTo, 0.05f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FEV
{
    // Values for creating boid agents
    public float neighborRadius;
    public float avoidRadius;
    public float collisionLength;
    public float minSpeed;
    public float maxSpeed;
    public float maxTurnSpd;
    public string fishType;
    public int id;
    public string modelUrl;

    // Values for JSON encyclopedia
    public string fishName;
    public string scientificName;
    public string type;
    public string diet;
    public string habitat;
    public float minSize, maxSize;
    public float minTemp, maxTemp;
    public float minDepth, maxDepth;
    public string range;
    public string status;
    public int lowerLimit, upperLimit;

    void Start()
    {

    }

    //change fish spawn on parameter change
}

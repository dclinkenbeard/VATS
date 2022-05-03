using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FEV
{


    // Values for creating boid agents
    public int id;
    public double neighborRadius;
    public double avoidRadius;
    public double collisionLength;
    public double minSpeed;
    public double maxSpeed;
    public double maxTurnSpd;
    public string fishType;
    public string modelUrl;
    public bool hasCohesion;
    public bool hasAlignment;
    public bool hasAvoidance;
    public double cohesionWeight;
    public double alignmentWeight;
    public double avoidanceWeight;

    // Values for JSON encyclopedia
    public string name;
    public string sci;
    public string type;
    public string diet;
    public string habitat;
    public double minSize, maxSize;
    public double minTemp, maxTemp;
    public double minDepth, maxDepth;
    public double minAcidity, maxAcidity;
    public double minPollution, maxPollution;
    public string range;
    public string status;
    public int lowLimit, uppLimit;

    
    //change fish spawn on parameter change
}

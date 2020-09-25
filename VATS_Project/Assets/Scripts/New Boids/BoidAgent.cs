using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAgent : MonoBehaviour
{
    public float neighborRadius = 5f;
    public float avoidRadius = 1f;
    public float collisionLength = 5f;

    public float minSpeed = 5f;
    public float maxSpeed = 10f;

    public float maxTurnSpd = 10f;

    public string fishType;
    public LayerMask obstacleMask;
    public LayerMask fishLayerMask;

    // two lists must be corresponding
    public List<BoidBehavior> boidBehaviors;
    public List<float> behaviorWeights;

    //float avoidanceWeight = 2.5f;
    //float alignmentWeight = 2f;
    //float cohesionWeight = 1f;

    const float collisionWeight = 40f;

    List<Vector3> spherePoints;
    Vector3 velocity;


    bool spawning = true;
    bool despawning = false;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
        
        velocity = transform.forward * ((minSpeed + maxSpeed)/2);
        spherePoints = GenerateSpherePoints();
    }

    // Update is called once per frame
    void Update()
    {
        //for debugging
        //if (Input.GetKeyDown(KeyCode.B)) {
        // despawning = true;
        //}
        if (spawning) {
            transform.localScale = Vector3.Lerp(transform.localScale, scale, 0.1f);
            if ((transform.localScale - scale).sqrMagnitude < 0.1f) {
                transform.localScale = scale;
                spawning = false;
            }
        }

        if (despawning) {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0,0,0), 0.1f);
            if (transform.localScale.sqrMagnitude < 0.1f)
            {
                transform.localScale = new Vector3(0,0,0);
                Destroy(gameObject);
            }
        }

        Vector3 acceleration = transform.forward;

        //FindOpenDirection();
        List<Transform> nearbyObjects = GetNearbyObjects();
        List<Transform> avoidObjects = GetAvoidObjects();


        //Vector3 avoidance = SteerTowards(CalculateAvoidance(avoidObjects)) * avoidanceWeight;
        //Vector3 alignment = SteerTowards(CalculateAlignment(nearbyObjects)) * alignmentWeight;
        //Vector3 cohesion = SteerTowards(CalculateCohesion(nearbyObjects)) * cohesionWeight;
        //Vector3 center = SteerTowards(transform.parent.transform.position) * 6f;

        for (int i = 0; i < boidBehaviors.Count; i++) {
            Vector3 behaviorMove = SteerTowards(boidBehaviors[i].CalculateMove(gameObject, nearbyObjects, avoidObjects)) * behaviorWeights[i];
            acceleration += behaviorMove;

        }

        //acceleration += avoidance;
        //acceleration += alignment;
        //acceleration += cohesion;
        //acceleration += center;
        //transform.position += velocity * Time.deltaTime;

        if (Physics.SphereCast(transform.position, 1f, transform.forward, out _, collisionLength, obstacleMask))
        {
            Vector3 collisionAvoidDir = FindOpenDirection();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * collisionWeight;
            acceleration = collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        velocity = dir * speed;

        transform.position += velocity * Time.deltaTime;
        transform.forward = dir;

      
    }
    public Vector3 FindOpenDirection()
    {
        
        for (int i = 0; i < spherePoints.Count; i++)
        {
            Vector3 dir = transform.TransformDirection(spherePoints[i]);
            Ray ray = new Ray(transform.position, dir);
            if (!Physics.SphereCast(ray, 1f, collisionLength, obstacleMask))
            {
                return dir;
            }
        }


        return transform.forward;
    }
    void StayInArea()
    {
        float maxLength = 50f;
        if (transform.position.x > maxLength) {
            Vector3 pos = transform.position;
            pos.x = -maxLength;
            transform.position = pos;
        }
        if (transform.position.x < -maxLength)
        {
            Vector3 pos = transform.position;
            pos.x = maxLength;
            transform.position = pos;
        }

        if (transform.position.y > maxLength)
        {
            Vector3 pos = transform.position;
            pos.y = -maxLength;
            transform.position = pos;
        }
        if (transform.position.y < -maxLength)
        {
            Vector3 pos = transform.position;
            pos.y = maxLength;
            transform.position = pos;
        }

        if (transform.position.z > maxLength)
        {
            Vector3 pos = transform.position;
            pos.z = -maxLength;
            transform.position = pos;
        }
        if (transform.position.z < -maxLength)
        {
            Vector3 pos = transform.position;
            pos.z = maxLength;
            transform.position = pos;
        }
    }

    /*
    Vector3 CalculateAvoidance(List<Transform> objects) {
        Vector3 avoidance = transform.forward;
        foreach (Transform obj in objects) {
            avoidance += transform.position - obj.position;
        }

        if (objects.Count > 0) {
            avoidance /= objects.Count;
        }

        return avoidance;
    }
    */

    /*
    Vector3 CalculateAlignment(List<Transform> objects)
    {
        Vector3 alignment = transform.forward;
        foreach (Transform obj in objects)
        {
            alignment += obj.forward;
        }

        if (objects.Count > 0)
        {
            alignment /= objects.Count;
        }

        return alignment;
    }
    */

    /*
    Vector3 CalculateCohesion(List<Transform> objects)
    {
        Vector3 cohesion = transform.forward;
        foreach (Transform obj in objects)
        {
            cohesion += obj.position;
        }

        if (objects.Count > 0)
        {
            cohesion /= objects.Count;
            cohesion -= transform.position;
        }

        return cohesion;
    }
    */
    List<Transform> GetNearbyObjects()
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(transform.position, neighborRadius);
        
        foreach (Collider c in contextColliders)
        {
            if (c.gameObject == this) {
                continue;
            }
            if (fishLayerMask == (fishLayerMask | (1 << c.gameObject.layer))){
                context.Add(c.transform);
            }
        }
        

        return context;
    }

    List<Transform> GetAvoidObjects()
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(transform.position, avoidRadius);
        
        foreach (Collider c in contextColliders)
        {
            if (c.gameObject == this)
            {
                continue;
            }
            if (fishLayerMask == (fishLayerMask | (1 << c.gameObject.layer))) {
                context.Add(c.transform);
            }
        }

        return context;
    }
    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, maxTurnSpd);
    }

    public List<Vector3> GenerateSpherePoints()
    {

        List<Vector3> spherePointsList = new List<Vector3>();
        int numPoints = 300;
        float turnFraction = 0.618033f;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (numPoints - 1f);
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * turnFraction * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            spherePointsList.Add(new Vector3(x, y, z));
        }

        return spherePointsList;
    }
}


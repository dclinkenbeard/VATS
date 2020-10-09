using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Area")]

public class StayInAreaBehavior : FlockBehavior
{
    public Vector3 center;
    public float horizontalRadius = 5f;
    public float verticalRadius = 5f;
    //public float radius = 5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 centerOffset = center - agent.transform.position;
        float t = 0f;

        if (Mathf.Abs(center.y - agent.transform.position.y) / verticalRadius > 0.8f) {
            t = centerOffset.magnitude/verticalRadius;
            return centerOffset * t * t;
        }

        if (Mathf.Abs(center.x - agent.transform.position.x) / horizontalRadius > 0.8f)
        {
            t = centerOffset.magnitude / horizontalRadius;
            return centerOffset * t * t;
        }

        if (Mathf.Abs(center.z - agent.transform.position.z) / horizontalRadius > 0.8f)
        {
            t = centerOffset.magnitude / horizontalRadius;
            return centerOffset * t * t;
        }

        return Vector3.zero;
        /*
        if (t < 0.9f) {
            return Vector3.zero;
        }

        //return agent.transform.position - (agent.transform.forward * 5);
        //return center;
        return centerOffset * t * t;
        */
    }
}

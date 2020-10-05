using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Pulled Towards Center")]

public class PulledTowardsCenterBehavior : FlockBehavior
{
    public Vector3 center;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //float dist = Vector3.Distance(agent.transform.for, center);
        return center;
    }
}

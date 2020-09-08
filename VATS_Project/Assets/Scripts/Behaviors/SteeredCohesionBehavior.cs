using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FlockBehavior
{
    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 cohesionMove = Vector3.zero;

        foreach (Transform item in context)
        {
            cohesionMove += item.position;
        }

        cohesionMove /= context.Count;
        cohesionMove -= agent.transform.position;

        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);

        return cohesionMove;
    }
}

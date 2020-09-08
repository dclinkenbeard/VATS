using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0) {
            return Vector3.zero;
        }
        Vector3 cohesionMove = Vector3.zero;

        foreach (Transform item in context) {
            cohesionMove += item.position;
        }

        cohesionMove /= context.Count;
        cohesionMove -= agent.transform.position;

        return cohesionMove;
    }
}

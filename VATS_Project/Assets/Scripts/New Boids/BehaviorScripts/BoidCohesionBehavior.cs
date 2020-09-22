using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Cohesion")]
public class BoidCohesionBehavior : BoidBehavior
{
    public override Vector3 CalculateMove(GameObject agent, List<Transform> nearbyObjects, List<Transform> avoidObjects)
    {

        Vector3 cohesion = agent.transform.forward;
        foreach (Transform obj in nearbyObjects)
        {
            cohesion += obj.position;
        }

        if (nearbyObjects.Count > 0)
        {
            cohesion /= nearbyObjects.Count;
            cohesion -= agent.transform.position;
        }

        return cohesion;
    }
}

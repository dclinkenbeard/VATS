using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Avoidance")]
public class BoidAvoidanceBehavior : BoidBehavior
{
    public override Vector3 CalculateMove(GameObject agent, List<Transform> nearbyObjects, List<Transform> avoidObjects)
    {
        foreach (BoidFilter filter in filters) {
            avoidObjects = filter.Filter(agent, avoidObjects);
        }

        Vector3 avoidance = agent.transform.forward;
        foreach (Transform obj in avoidObjects)
        {
            avoidance += agent.transform.position - obj.position;
        }

        if (avoidObjects.Count > 0)
        {
            avoidance /= avoidObjects.Count;
        }

        return avoidance;
    }
}

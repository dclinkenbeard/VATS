using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Alignment")]
public class BoidAlignmentBehavior : BoidBehavior
{

    public override Vector3 CalculateMove(GameObject agent, List<Transform> nearbyObjects, List<Transform> avoidObjects)
    {
        foreach (BoidFilter filter in filters)
        {
            avoidObjects = filter.Filter(agent, avoidObjects);
        }
        Vector3 alignment = agent.transform.forward;
        foreach (Transform obj in nearbyObjects)
        {
            alignment += obj.forward;
        }

        if (nearbyObjects.Count > 0)
        {
            alignment /= nearbyObjects.Count;
        }

        return alignment;
    }
}

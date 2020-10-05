using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Raycast Avoidance")]
public class RaycastAvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0) {
            return Vector3.zero;
        }

        Vector3 avoidanceMove;
        if (Physics.Raycast(agent.transform.position, agent.transform.forward, 5f)) {
            avoidanceMove = FindOpenDirection(agent, flock);
        }else{
            return Vector3.zero;
        }

        return avoidanceMove;
    }

    public Vector3 FindOpenDirection(FlockAgent agent, Flock flock) {
        Vector3 bestDir = agent.transform.forward;
        float furthestUnobstructedDst = 0;
        RaycastHit hit;

        for (int i = 0; i < flock.spherePoints.Count; i++) {
            Vector3 dir = (agent.transform.position - flock.spherePoints[i]).normalized;

            //Vector3 dir = agent.transform.TransformDirection(flock.spherePoints[i]);
            if (Physics.SphereCast(agent.transform.position, 5f, dir, out hit))
            {
                if (hit.distance > furthestUnobstructedDst)
                {
                    bestDir = flock.spherePoints[i];
                    furthestUnobstructedDst = hit.distance;
                }
            }
            else {
                return flock.spherePoints[i];
            }
        }

        return bestDir;
    }
}
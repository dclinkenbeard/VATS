using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidBehavior : ScriptableObject
{
	public List<BoidFilter> filters;
	public abstract Vector3 CalculateMove(GameObject agent, List<Transform> nearbyObjects, List<Transform> avoidObjects);
}

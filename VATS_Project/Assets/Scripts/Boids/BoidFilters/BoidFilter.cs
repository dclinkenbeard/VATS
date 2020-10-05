using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidFilter : ScriptableObject
{
	public abstract List<Transform> Filter(GameObject agent, List<Transform> objects);
}

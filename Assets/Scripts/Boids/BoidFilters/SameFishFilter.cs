using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Filter/SameFish")]
public class SameFishFilter : BoidFilter
{
	public override List<Transform> Filter(GameObject agent, List<Transform> objects) {

		List<Transform> filteredList = new List<Transform>();
		string agentType = agent.GetComponent<BoidAgent>().fishType;

		foreach (Transform obj in objects) {
			if (string.Compare(obj.GetComponent<BoidAgent>().fishType, agentType) == 0)
			{
				filteredList.Add(obj);
			}
		}

		return filteredList;
	}
}

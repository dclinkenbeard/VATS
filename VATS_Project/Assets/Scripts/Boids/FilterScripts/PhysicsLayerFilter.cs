using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            //if (mask == (mask | (1 << item.gameObject.layer))) {
            if (item.gameObject.layer == 8) {
                filtered.Add(item);
            }
        }
        foreach (Transform item in filtered) {
            Debug.Log(item.gameObject);
            Debug.Log(item.gameObject.name + "/n");
        }

        return filtered;
    }
}

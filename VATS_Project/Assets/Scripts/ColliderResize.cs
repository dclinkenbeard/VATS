using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderResize : MonoBehaviour
{
    public GameObject colliderParent;
    public GameObject zColliderA, zColliderB, xColliderA, xColliderB, topCollider, bottomCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ResizeColliders();
    }

    void ResizeColliders() {
        float localYPos = Mathf.Abs(transform.localPosition.y);
        colliderParent.transform.localPosition = new Vector3(0, (localYPos/2),0);

        zColliderA.transform.localPosition = new Vector3(zColliderA.transform.localPosition.x, zColliderA.transform.localPosition.y, (localYPos / 2f));
        zColliderB.transform.localPosition = new Vector3(zColliderB.transform.localPosition.x, zColliderB.transform.localPosition.y, -(localYPos / 2f));

        xColliderA.transform.localPosition = new Vector3((localYPos / 2f), xColliderA.transform.localPosition.y, xColliderA.transform.localPosition.z);
        xColliderB.transform.localPosition = new Vector3(-(localYPos / 2f), xColliderB.transform.localPosition.y, xColliderB.transform.localPosition.z);

        topCollider.transform.localPosition = new Vector3(topCollider.transform.localPosition.x, (localYPos / 2f), topCollider.transform.localPosition.z);
        bottomCollider.transform.localPosition = new Vector3(bottomCollider.transform.localPosition.x, -(localYPos / 2f), bottomCollider.transform.localPosition.z);

        zColliderA.transform.localScale = new Vector3(localYPos, localYPos, 1);
        zColliderB.transform.localScale = new Vector3(localYPos, localYPos, 1);
        xColliderA.transform.localScale = new Vector3(localYPos, localYPos, 1);
        xColliderB.transform.localScale = new Vector3(localYPos, localYPos, 1);
        topCollider.transform.localScale = new Vector3(localYPos, localYPos, 1);
        bottomCollider.transform.localScale = new Vector3(localYPos, localYPos, 1);

    }
}

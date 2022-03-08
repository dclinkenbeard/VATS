using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderResize : MonoBehaviour
{
    //variable declaration
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
    //When the fish are spawned, updating the local position once per frame
    //ensuring that the fish stay in a 'box' or local area
    void ResizeColliders() {
        float localYPos = Mathf.Abs(transform.localPosition.y);
        colliderParent.transform.localPosition = new Vector3(0, (localYPos/2),0);
        //changing the local z position by taking the local y position of the parent and dividing that by 2 units,
        //maintaining the x and y component of the position relative to the parent
        zColliderA.transform.localPosition = new Vector3(zColliderA.transform.localPosition.x, zColliderA.transform.localPosition.y, (localYPos / 2f));
        zColliderB.transform.localPosition = new Vector3(zColliderB.transform.localPosition.x, zColliderB.transform.localPosition.y, -(localYPos / 2f));
        //same as the code above except transforming the x position of the child relative to the y position of the parent divided in half
        xColliderA.transform.localPosition = new Vector3((localYPos / 2f), xColliderA.transform.localPosition.y, xColliderA.transform.localPosition.z);
        xColliderB.transform.localPosition = new Vector3(-(localYPos / 2f), xColliderB.transform.localPosition.y, xColliderB.transform.localPosition.z);
        //same as the code above except transforming the y-axis relative 
        topCollider.transform.localPosition = new Vector3(topCollider.transform.localPosition.x, (localYPos / 2f), topCollider.transform.localPosition.z);
        bottomCollider.transform.localPosition = new Vector3(bottomCollider.transform.localPosition.x, -(localYPos / 2f), bottomCollider.transform.localPosition.z);
        //increase the shape of the object's x and y axis relative to the x and y values of the parent
        zColliderA.transform.localScale = new Vector3(localYPos+1, localYPos+1, 1);
        zColliderB.transform.localScale = new Vector3(localYPos+1, localYPos+1, 1);
        xColliderA.transform.localScale = new Vector3(localYPos+1, localYPos+1, 1);
        xColliderB.transform.localScale = new Vector3(localYPos+1, localYPos+1, 1);
        topCollider.transform.localScale = new Vector3(localYPos+1, localYPos+1, 1);
        bottomCollider.transform.localScale = new Vector3(localYPos+1, localYPos+1, 1);

    }
}

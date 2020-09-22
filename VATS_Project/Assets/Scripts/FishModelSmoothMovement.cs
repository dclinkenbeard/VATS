using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishModelSmoothMovement : MonoBehaviour
{
    Quaternion rotation;
    Vector3 position;

    public float rotLerpSpd = 0.1f;
    public float posLerpSpd = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.parent.rotation;
        position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = transform.parent.rotation;
        rotation = Quaternion.Lerp(rotation, transform.parent.rotation, rotLerpSpd);
        position = Vector3.Lerp(position, transform.parent.position, posLerpSpd);

        transform.rotation = rotation;
        transform.position = position;
    }
}

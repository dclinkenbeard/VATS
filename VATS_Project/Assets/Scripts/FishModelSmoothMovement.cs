using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishModelSmoothMovement : MonoBehaviour
{
    Quaternion rotation;
    Vector3 position;
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
        rotation = Quaternion.Lerp(rotation, transform.parent.rotation, 0.1f);
        position = Vector3.Lerp(position, transform.parent.position, 0.1f);

        transform.rotation = rotation;
        transform.position = position;
    }
}

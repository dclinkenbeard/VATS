/*  
*   Fish lerping movement 
*   set for each fish movement excluding sardines
*   Path: ExplorationScene/ExaminationRoom/FishList/Boid+"fish"
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishModelSmoothMovement : MonoBehaviour
{
    Vector3 basePos;
    Quaternion rotation;
    Vector3 position;

    public float rotLerpSpd = 0.1f;
    public float posLerpSpd = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        basePos = transform.localPosition;
        rotation = transform.parent.rotation;
        position = transform.parent.position;
    }

    /*  
    *   Update is called once per frame
    *   Vector3.lerp - lerp(start value, end value, value used to interpolate between a and b),
    *   linear interpolates two points
    *   Quaternion.lerp - (starting rotation, target rotation, value from 0 - 1 time*speed)
    *   rotates object 
    */
    void Update()
    {
        //transform.rotation = transform.parent.rotation;
        rotation = Quaternion.Lerp(rotation, transform.parent.rotation, rotLerpSpd);
        position = Vector3.Lerp(position, transform.parent.position + basePos, posLerpSpd);

        transform.rotation = rotation;
        transform.position = position;
    }
}

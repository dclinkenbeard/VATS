using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    int state = 0;
    Transform trackingTarget;
    public float trackingLerp = 0f;

    public float mouseSense = 1.8f;
    public float movementSpeed = 10f;
    public float boostedSpeed = 50f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000)) {
                trackingTarget = hit.transform;
                trackingLerp = 0.01f;
                state = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            transform.parent = null;
            state = 0;
        }

        switch (state) {
            case 0:
                FreeFly();
                break;
            case 1:
                Track();
                break;
        }

    }

    public void FreeFly()
    {
        //movement
            Vector3 deltaPosition = Vector3.zero;
            float currentSpeed = movementSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = boostedSpeed;

            if (Input.GetKey(KeyCode.W))
                deltaPosition += transform.forward;

            if (Input.GetKey(KeyCode.S))
                deltaPosition -= transform.forward;

            if (Input.GetKey(KeyCode.A))
                deltaPosition -= transform.right;

            if (Input.GetKey(KeyCode.D))
                deltaPosition += transform.right;
            /*
            if (Input.GetKey(_moveUp))
                deltaPosition += transform.up;

            if (Input.GetKey(_moveDown))
                deltaPosition -= transform.up;
            */

        transform.position += deltaPosition * currentSpeed * Time.deltaTime;

        //Rotation
        // Pitch
        transform.rotation *= Quaternion.AngleAxis(
            -Input.GetAxis("Mouse Y") * mouseSense,
            Vector3.right
        );

        // Paw
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + Input.GetAxis("Mouse X") * mouseSense,
            transform.eulerAngles.z
        );
    }

    public void Track()
    {
        if (transform.parent == trackingTarget) {
            //transform.localPosition = new Vector3(1f, 0, 0);
            //transform.localRotation = Quaternion.Euler(0, -90f, 0);
            return;
        }

        Vector3 targetPos = trackingTarget.position + (trackingTarget.right * 1f);

        Vector3 fishRot = trackingTarget.rotation.eulerAngles;
        //Quaternion targetRot = Quaternion.Euler(fishRot.x, fishRot.y - 90f, fishRot.z);

        if (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, trackingLerp);
            trackingLerp *= 1.15f;
            transform.LookAt(trackingTarget.position);
        }
        else
        {
            transform.parent = trackingTarget;
            transform.localPosition = new Vector3(1f,0,0);
            transform.localRotation = Quaternion.Euler(0,-90f,0);
        }



        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.1f);

    }
}

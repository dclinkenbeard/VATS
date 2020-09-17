using System;
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
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            }else if (Cursor.lockState == CursorLockMode.None){
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.Locked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.transform.gameObject.tag == "Fish")
                {
                    trackingTarget = hit.transform;
                    trackingLerp = 0.005f;
                    state = 1;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.parent = null;
            state = 0;
        }

        switch (state)
        {
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
        if (Cursor.lockState != CursorLockMode.Locked) {
            return;
        }

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

        Transform trackingModel = trackingTarget.GetChild(0);
        if (transform.parent == trackingModel) {
            //transform.localPosition = new Vector3(1f, 0, 0);
            //transform.localRotation = Quaternion.Euler(0, -90f, 0);
            return;
        }

        Vector3 targetPos = trackingModel.position + (trackingModel.right * 2 * trackingTarget.localScale.x); //(trackingTarget.forward);
        Vector3 targetAngle = trackingModel.rotation.eulerAngles;
        targetAngle.x = 0;
        targetAngle.z = 0;
        targetAngle.y -= 90;

        //Vector3 fishRot = trackingTarget.rotation.eulerAngles;
        //Quaternion targetRot = Quaternion.Euler(fishRot.x, fishRot.y - 90f, fishRot.z);

        if (Vector3.Distance(transform.position, targetPos) > 1f)
        {
            //trackingLerp = 0.1f;
            transform.position = Vector3.Lerp(transform.position, targetPos, trackingLerp);
            trackingLerp *= 1.1f;
            transform.LookAt(trackingModel.position);
            Debug.Log("Tracking");
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngle), 0.1f);
        }
        else
        {
            transform.position = targetPos;
            transform.LookAt(trackingModel.position);
            Debug.Log("Tracked");
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngle), 0.01f);
            //transform.parent = trackingTarget;
            //transform.localPosition =  trackingTarget.position + new Vector3(1f,0,0);
            //transform.LookAt(trackingTarget.position);
            //transform.localRotation = Quaternion.Euler(0,-90f,0);
        }



        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.1f);

    }
}

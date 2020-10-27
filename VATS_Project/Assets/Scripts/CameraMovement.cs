using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CameraMovement : MonoBehaviour
{
    int state = 0;
    Transform trackingTarget;
    public float trackingLerp = 0f;

    public float mouseSense = 1.8f;
    public float movementSpeed = 10f;
    public float boostedSpeed = 50f;
    float camDistance = -10.0f;

    public Vector3 min_bound;
    public Vector3 max_bound;

    public LayerMask fishLayerMask;

    private Vector3 previousPosition;

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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1500) && state == 0) //1500
        {
            bool checkExamRoom = transform.GetComponent<CameraUI>().CheckInExamRoom();

            if (fishLayerMask == (fishLayerMask | (1 << hit.transform.gameObject.layer)) && state != 1 && !checkExamRoom)
            {
                transform.GetComponent<CameraUI>().basicInterface.SetActive(true);
                transform.GetComponent<CameraUI>().flockSpawn = hit.transform.parent.gameObject;
                transform.GetComponent<CameraUI>().UITextHandler(1);

                if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.Locked)
                {
                    trackingTarget = hit.transform;
                    trackingLerp = 0.005f;
                    state = 1;
                    transform.GetComponent<CameraUI>().UITextHandler(2);
                }
            }
            else
            {
                transform.GetComponent<CameraUI>().basicInterface.SetActive(false);
                transform.GetComponent<CameraUI>().flockSpawn = null;
            }
        }

        // Exit following fish
        if (Input.GetKeyDown(KeyCode.Q) && state == 1)
        {
            transform.parent = null;
            state = 0;
            transform.GetComponent<CameraUI>().UITextHandler(0);
        }

        // Enter Exmination Room
        if (Input.GetKeyDown(KeyCode.E) && state == 1)
        {
            transform.parent = null;
            state = 2;

            transform.GetComponent<CameraUI>().examEnter = true;

        }

        // Exit Examination Room
        if (Input.GetKeyDown(KeyCode.R) && transform.GetComponent<CameraUI>().inExamRoom)
        {
            transform.GetComponent<CameraUI>().examExit = true;
            state = 1;
        }

        if (transform.GetComponent<CameraUI>().inExamRoom) {
            return;
        }

        switch (state)
        {
            case 0:
                FreeFly();
                break;
            case 1:
                Track();
                break;
            case 2:
                Transform examPos = transform.gameObject.GetComponent<CameraUI>().GetFishExamRoom();
                Rotate(examPos);
                break;
        }
    }

    public void FreeFly()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

        Rotate(trackingModel);

        /*Vector3 targetPos = trackingModel.position + (trackingModel.right * Mathf.Clamp(2f * trackingTarget.localScale.x, 0f, 50f)); //(trackingTarget.forward);
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
            //Debug.Log("Tracking");
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngle), 0.1f);
        }
        else
        {
            transform.position = targetPos;
            transform.LookAt(trackingModel.position);
            //Debug.Log("Tracked");
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngle), 0.01f);
            //transform.parent = trackingTarget;
            //transform.localPosition =  trackingTarget.position + new Vector3(1f,0,0);
            //transform.LookAt(trackingTarget.position);
            //transform.localRotation = Quaternion.Euler(0,-90f,0);
        }*/



        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.1f);

    }

    public void Rotate(Transform target)
    {
        Camera cam = transform.gameObject.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.None;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            camDistance++;
            cam.transform.position = target.position;
            cam.transform.Translate(new Vector3(0, 0, camDistance));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camDistance--;
            cam.transform.position = target.position;
            cam.transform.Translate(new Vector3(0, 0, camDistance));
        }

        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            CameraUpdate(cam, camDistance, target);
        }

        if (state == 1)
        {
            Debug.Log("following fish camUpdate");
            cam.transform.position = target.position;
            cam.transform.Translate(new Vector3(0, 0, camDistance));

            //CameraUpdate(cam, camDistance, target);
        }
    }

    public void CameraUpdate(Camera cam, float camDistance, Transform target)
    {
        Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

        cam.transform.position = target.position;

        cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
        cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
        cam.transform.Translate(new Vector3(0, 0, camDistance));

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }
}

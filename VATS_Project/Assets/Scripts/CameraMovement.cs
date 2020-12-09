﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using TMPro;

public class CameraMovement : MonoBehaviour
{
    int state = 0;
    int targetState = -2;
    Transform trackingTarget;
    public float trackingLerp = 0f;

    public float mouseSense = 1.8f;
    public float movementSpeed = 10f;
    public float boostedSpeed = 50f;
    public float camDistance = -10.0f;
    public float minPitch, maxPitch;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Vector3 min_bound;
    public Vector3 max_bound;

    public LayerMask fishLayerMask;

    private Vector3 previousPosition;

    // Slider Interface
    public GameObject sliderInterface;
    public TextMeshProUGUI sliderText;

    string fishName;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sliderText.text = "Press M to open Ocean Sliders";
        sliderInterface.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1500) && state == 0) //1500
        {
            bool checkExamRoom = transform.GetComponent<CameraUI>().CheckInExamRoom();

            if (fishLayerMask == (fishLayerMask | (1 << hit.transform.gameObject.layer)) && state != 1 && !checkExamRoom)
            {
                transform.GetComponent<CameraUI>().basicInterface.SetActive(true);
                transform.GetComponent<CameraUI>().fishManager = hit.transform.parent.gameObject;
                transform.GetComponent<CameraUI>().UITextHandler(1);

                if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.Locked)
                {
                    trackingTarget = hit.transform;
                    trackingLerp = 0.005f;
                    state = 1;
                    transform.GetComponent<CameraUI>().UITextHandler(2);
                    fishName = SetFishName(trackingTarget.gameObject);
                }
            }
            else
            {
                transform.GetComponent<CameraUI>().basicInterface.SetActive(false);
                transform.GetComponent<CameraUI>().fishManager = null;
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
            state = -1;
            transform.GetComponent<CameraUI>().examEnter = true;
            transform.parent = null;
            targetState = 2;

        }

        // Exit Examination Room
        if (Input.GetKeyDown(KeyCode.R) && transform.GetComponent<CameraUI>().inExamRoom)
        {
            state = -1;
            transform.GetComponent<CameraUI>().examExit = true;
            targetState = 1;
        }

        // To prevent fast transition and allow reg transition
        if(targetState > -2 && !transform.GetComponent<CameraUI>().examExit && !transform.GetComponent<CameraUI>().examEnter){
            state = targetState;
            targetState = -2;
        }


        // Open and close Ocean Settings Menu
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (state == 0)
            {
                state = 3;
                Cursor.visible = true;
            }
            else if (state == 3)
            {
                state = 0;
                Cursor.visible = false;
            }
        }

        switch (state)
        {
            case 0:
                gameObject.GetComponent<FishInfoSet>().fishID = -1; 
                sliderText.text = "Press M to open Ocean Sliders";
                sliderInterface.SetActive(false);
                FreeFly();
                break;
            case 1:
                gameObject.GetComponent<FishInfoSet>().fishID = trackingTarget.GetComponent<BoidAgent>().id;
                sliderText.text = "";
                sliderInterface.SetActive(false);
                Track();
                break;
            case 2:
                gameObject.GetComponent<FishInfoSet>().fishID = -1; 
                Transform examPos = transform.gameObject.GetComponent<CameraUI>().GetFishExamRoom();
                Rotate(examPos);
                break;
            case 3:
                gameObject.GetComponent<FishInfoSet>().fishID = -1; 
                sliderText.text = "Press M to close Ocean Sliders";
                sliderInterface.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public void SetCamDistance(float distance) { camDistance = distance; }

    public string SetFishName(GameObject trackingTarget)
    {
        string tempName = trackingTarget.gameObject.name;
        string finalName = tempName.Remove(tempName.Length - 7, 7);
        return finalName;
    }

    public string GetFishName() { return fishName; }

    public void FreeFly()
    {
        gameObject.GetComponent<FishInfoSet>().fishID = -1; 
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

        transform.position += deltaPosition * currentSpeed * Time.deltaTime;

        //Rotation

        yaw += mouseSense  * Input.GetAxis("Mouse X");
        pitch -= mouseSense * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);                               

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }

    public void Track()
    {
        Transform trackingModel = trackingTarget.GetChild(0);
        if (transform.parent == trackingModel) {
            return;
        }

        Rotate(trackingModel);
    }

    public void Rotate(Transform target)
    {
        Camera cam = transform.gameObject.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.None;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            camDistance++;
            camDistance = ClampCamera(camDistance);  
            cam.transform.position = target.position;
            cam.transform.Translate(new Vector3(0, 0, camDistance));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camDistance--;
            camDistance = ClampCamera(camDistance);
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
            cam.transform.position = target.position;
            cam.transform.Translate(new Vector3(0, 0, camDistance));
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

    public float ClampCamera(float camDistance)
    {
        float clampedCamDis = 0;
        switch (state)
        {
            case 1:
                // When tracking marine life
                clampedCamDis = Mathf.Clamp(camDistance, -15.0f, -10.0f);
                break;
            case 2:
                // When examining marine life
                clampedCamDis = Mathf.Clamp(camDistance, -15.0f, -5.0f);
                break;
        }
        return clampedCamDis;
    }
}

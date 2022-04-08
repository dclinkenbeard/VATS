using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using TMPro;

public class CameraMovement : MonoBehaviour
{
    // state is determines the location of the camera
    // targetState determines whether a target is being followed, examined, or neither
    int state = 0;
    int targetState = -2;
    Transform trackingTarget;
    public float trackingLerp = 0f;

    // Camera control values
    public float mouseSense = 1.8f;
    public float movementSpeed = 10f;
    public float boostedSpeed = 50f;
    public float camDistance = -10.0f;
    public float minPitch, maxPitch;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Currently unused (purpose unknown)
    public Vector3 min_bound;
    public Vector3 max_bound;

    // Determines whether fish can be detected by raycasts
    public LayerMask fishLayerMask;

    // Determines the offset of the direction of the camera by however far the mouse is dragged
    // while left mouse button is held down
    private Vector3 previousPosition;

    // Slider Interface
    public GameObject sliderInterface;
    public TextMeshProUGUI sliderText;

    // Use to get and set the name of the fish being tracked for the UI
    string fishName;



    /// <summary>
    ///     Start the scene with the cursor locked (in the center of the screen) and
    ///     the slider menu closed
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sliderText.text = "Press M to open Ocean Sliders";
        sliderInterface.SetActive(false);
    }



    /// <summary>
    ///     Every frame, check if a fish is clicked, and whether other buttons controlling UI or camera are pressed.
    /// </summary>
    void Update()
    {
        // Use a raycast to determine what the camera is looking at and whether a fish has been clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If the camera is in free fly mode, check the following cases
        if (Physics.Raycast(ray, out hit, 1500) && state == 0) //1500
        {
            // First, check whether the camera is in the examination room
            bool checkExamRoom = transform.GetComponent<CameraUI>().CheckInExamRoom();

            // If the camera is looking at a fish, a fish is not already being tracked, and the camera is not already in the examination room,
            // then display UI telling the player they can track the fish
            if (fishLayerMask == (fishLayerMask | (1 << hit.transform.gameObject.layer)) && state != 1 && !checkExamRoom)
            {
                transform.GetComponent<CameraUI>().basicInterface.SetActive(true);
                transform.GetComponent<CameraUI>().fishManager = hit.transform.parent.gameObject;
                transform.GetComponent<CameraUI>().UITextHandler(1);

                // If left mouse button is clicked while looking at a fish and the cursor is locked,
                // then track the fish
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

        // Exit fish tracking
        if (Input.GetKeyDown(KeyCode.Q) && state == 1)
        {
            transform.parent = null;
            state = 0;
            transform.GetComponent<CameraUI>().UITextHandler(0);
        }

        // Enter Examination Room
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
        if (targetState > -2 && !transform.GetComponent<CameraUI>().examExit && !transform.GetComponent<CameraUI>().examEnter)
        {
            state = targetState;
            targetState = -2;
        }


        // Open and close Ocean Settings Menu
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (state == 0)
            {
                state = 3;
            }
            else if (state == 3)
            {
                state = 0;
            }
        }

        // Use state to regulate UI and camera movement
        switch (state)
        {
            // No fish is being tracked and slider menu is closed
            case 0:
                gameObject.GetComponent<FishInfoSet>().fishID = -1;
                sliderText.text = "Press M to open Ocean Sliders";
                sliderInterface.SetActive(false);
                FreeFly();
                break;

            // Fish is being tracked and slider menu is closed
            case 1:
                gameObject.GetComponent<FishInfoSet>().fishID = trackingTarget.GetComponent<BoidAgent>().id;
                sliderText.text = "";
                sliderInterface.SetActive(false);
                Track();
                break;

            // Fish is being examined
            case 2:
                gameObject.GetComponent<FishInfoSet>().fishID = -1;
                Transform examPos = transform.gameObject.GetComponent<CameraUI>().GetFishExamRoom();
                Rotate(examPos);
                break;

            // No fish is being tracked and slider menu is open
            case 3:
                gameObject.GetComponent<FishInfoSet>().fishID = -1;
                sliderText.text = "Press M to close Ocean Sliders";
                sliderInterface.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }



    /// <summary>
    ///     Set the camera's distance using a float variable.
    /// </summary>
    /// 
    /// <param name="distance">
    ///     The distance to change camDistance to.
    /// </param>
    /// 
    /// <remarks>
    ///     Typically this function will use the x vector of another transform as the distance.
    /// </remarks>
    public void SetCamDistance(float distance) 
    { 
        camDistance = distance; 
    }



    /// <summary>
    ///     Sets the name of the fish being tracked.
    /// </summary>
    /// 
    /// <param name="trackingTarget">
    ///     The target to set the fish name for.
    /// </param>
    /// 
    /// <returns>
    ///     The fish name being set.
    /// </returns>
    public string SetFishName(GameObject trackingTarget)
    {
        string tempName = trackingTarget.gameObject.name;
        string finalName = tempName.Remove(tempName.Length - 7, 7);
        return finalName;
    }



    /// <summary>
    ///     Gets the name of a fish, to which the gameObject this function is called on is connected.
    /// </summary>
    /// 
    /// <returns>
    ///     The name of the fish.
    /// </returns>
    public string GetFishName() 
    { 
        return fishName; 
    }



    /// <summary>
    ///     Controls the camera's movement when not tracking a target.
    /// </summary>
    public void FreeFly()
    {
        gameObject.GetComponent<FishInfoSet>().fishID = -1;
        Cursor.lockState = CursorLockMode.Locked;
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            return;
        }

        // Movement
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

        // Rotation
        yaw += mouseSense * Input.GetAxis("Mouse X");
        pitch -= mouseSense * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }



    /// <summary>
    ///     Sets the model for the camera to track, and tells the camera to rotate around it.
    /// </summary>
    public void Track()
    {
        Transform trackingModel = trackingTarget.GetChild(0);
        if (transform.parent == trackingModel)
        {
            return;
        }

        Rotate(trackingModel);
    }



    /// <summary>
    ///     Rotates the camera around a specified tracking target.
    /// </summary>
    /// 
    /// <remarks>
    ///     The distance between the camera and its target is controlled from here,
    ///     and the camera rotates when the mouse moves while the left mouse button
    ///     is held down.
    /// </remarks>
    /// 
    /// <param name="target">
    ///     The target around which the camera will be rotated.
    /// </param>
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

        if (Input.GetMouseButton(0))
        {
            CameraUpdate(cam, camDistance, target);
        }

        if (state == 1)
        {
            cam.transform.position = target.position;
            cam.transform.Translate(new Vector3(0, 0, camDistance));
        }
    }



    /// <summary>
    ///     Updates the camera's position when given a camera, tracking distance, and tracking target.
    /// </summary>
    /// 
    /// <param name="cam">
    ///     The camera whose position will be updated.
    /// </param>
    /// 
    /// <param name="camDistance">
    ///     The distance between the camera and the target that it's tracking.
    /// </param>
    /// 
    /// <param name="target">
    ///     The target being tracked by the camera.
    /// </param>
    public void CameraUpdate(Camera cam, float camDistance, Transform target)
    {
        Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

        cam.transform.position = target.position;

        cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
        cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
        cam.transform.Translate(new Vector3(0, 0, camDistance));

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }



    /// <summary>
    ///     When the camera distance has been increased or decreased, use clamp to lock the camera at the
    ///     new distance while maintaining its correct point of orbit.
    /// </summary>
    /// 
    /// <param name="camDistance">
    ///     The camera's new orbit radius.
    /// </param>
    /// 
    /// <returns>
    ///     The camera's new orbit radius, clamped.
    /// </returns>
    public float ClampCamera(float camDistance)
    {
        float clampedCamDis = 0;
        switch (state)
        {
            // When tracking marine life
            case 1:
                clampedCamDis = Mathf.Clamp(camDistance, -15.0f, -10.0f);
                break;

            // When examining marine life
            case 2:
                clampedCamDis = Mathf.Clamp(camDistance, -15.0f, -5.0f);
                break;
        }
        return clampedCamDis;
    }
}

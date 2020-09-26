using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMovement : MonoBehaviour
{
    int state = 0;
    Transform trackingTarget;
    public float trackingLerp = 0f;

    public float mouseSense = 1.8f;
    public float movementSpeed = 10f;
    public float boostedSpeed = 50f;

    public LayerMask fishLayerMask;

    // Basic Interface
    public GameObject basicInterface;
    public TextMeshProUGUI fishViewText;
    public TextMeshProUGUI fishExitText;
    public TextMeshProUGUI fishExamText;
    private string fishName;

    // Examination Room
    public Transform camExamPos;
    public Transform fishExamPos;
    [SerializeField] private Vector3 camOrigPos;
    [SerializeField] private Transform fishList;
    [SerializeField] private GameObject fishExamObj;
    [SerializeField] private bool inExamRoom;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        basicInterface.SetActive(false);
        fishViewText.text = "";
        fishExitText.text = "";
        fishExamText.text = "";
        inExamRoom = false;
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

        if (Physics.Raycast(ray, out hit, 500)) //1500
        {
            if (fishLayerMask == (fishLayerMask | (1 << hit.transform.gameObject.layer)) && state != 1 && !inExamRoom)
            {
                // Turns on the 'eye' and tells user what key to press to follow fish
                basicInterface.SetActive(true);
                GameObject flockSpawn = hit.transform.parent.gameObject;

                // Checks if fish has FEV TODO remove once all fish have it
                if (flockSpawn.GetComponent<FEV>())
                {
                    fishName = flockSpawn.GetComponent<FEV>().getFishName();
                }else {
                    fishName = "Target fish missing FEV!";
                }

                fishViewText.text = "Press Left Click to follow " + fishName;

                if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.Locked)
                {
                    trackingTarget = hit.transform;
                    trackingLerp = 0.005f;
                    state = 1;

                    basicInterface.SetActive(false);
                    fishViewText.text = "";
                    fishExitText.text = "Press Q to stop following " + fishName;
                    fishExamText.text = "Press E to examine " + fishName;
                    fishExamObj = flockSpawn.GetComponent<BoidSpawner>().agentPrefab;
                }
            }else {
                // if not in range or already following a fish, interface will turn off
                basicInterface.SetActive(false);
                fishViewText.text = "";
            }
        }

        // Exit following fish
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.parent = null;
            state = 0;
            fishExitText.text = "";
            fishExamText.text = "";
        }

        // Enter Exmination Room
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.parent = null;
            state = 0;

            inExamRoom = true;

            ExaminingFish(true);
            CameraTeleport(false);         

            fishExitText.text = "";
            fishExamText.text = "Press R to exit Examination Room";
        }

        // Exit Examination Room
        if (Input.GetKeyDown(KeyCode.R))
        {
            CameraTeleport(true);
            ExaminingFish(false);

            inExamRoom = false;
            fishExamText.text = "";
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

    // Examination Room Camera Handling
    public void CameraTeleport(bool returning)
    {
        if(returning)
        {
            this.transform.position = camOrigPos;
        }else {
            camOrigPos = this.transform.position;
            this.transform.position = camExamPos.position;
            this.transform.rotation = camExamPos.rotation;
        }
        return;
    }

    // Examination Room Fish Handling
    public void ExaminingFish(bool examining)
    {
        foreach (Transform childFish in fishList)
        {
            if(examining)
            {
                if (childFish.name.Equals(fishExamObj.name))
                {
                    // Find target fish and bring it into exam position
                    childFish.transform.position = fishExamPos.position;
                }
            }else {
                if (childFish.name.Equals(fishExamObj.name))
                {
                    // Reset fish position
                    childFish.position = fishList.position;
                }
            }
        }
            return;
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

        Vector3 targetPos = trackingModel.position + (trackingModel.right * Mathf.Clamp(2f * trackingTarget.localScale.x, 0f, 50f)); //(trackingTarget.forward);
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

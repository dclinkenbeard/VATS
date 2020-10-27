using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class CameraUI : MonoBehaviour
{
    // Basic Interface
    public GameObject canvas;
    public GameObject basicInterface;
    public GameObject flockSpawn;
    TextMeshProUGUI fishViewText;
    TextMeshProUGUI fishExitText;
    TextMeshProUGUI fishExamText;
    private string fishName;

    // Examination Room
    public GameObject examinationRoom;
    Transform camExamPos;
    Transform fishExamPos;
    private Vector3 camOrigPos;
    private Transform fishList;
    private GameObject fishExamObj;
    public bool inExamRoom;

    // Start is called before the first frame update
    void Start()
    {
        if (canvas != null)
        {
            // Get basic interface && fish view text
            basicInterface = canvas.transform.GetChild(0).gameObject;
            if(basicInterface.transform.childCount != 0)
            {
                fishViewText = basicInterface.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                fishViewText.text = "";
                basicInterface.SetActive(false);
            }
            // Get fish exit text
            fishExitText = canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            fishExitText.text = "";
            // Get fish exam text
            fishExamText = canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            fishExamText.text = "";
        }
        else
        {
            Debug.Log("ERROR: MISSING CANVAS!");
        }

        inExamRoom = false;

        // Check if the exam room is not empty
        if(examinationRoom != null)
        {
            camExamPos = examinationRoom.transform.GetChild(1);
            fishExamPos = examinationRoom.transform.GetChild(2);
            fishList = examinationRoom.transform.GetChild(3);
        }
        else
        {
            Debug.Log("ERROR: MISSING EXAMINTION ROOM!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flockSpawn != null)
        {
            if (flockSpawn.GetComponent<FEV>() != null)
            {
                fishName = flockSpawn.GetComponent<FEV>().getFishName();
                fishExamObj = flockSpawn.GetComponent<BoidSpawner>().agentPrefab;
            }
            else
            {
                fishName = "Target fish missing FEV!";
            }
        }
    }

    public bool CheckInExamRoom() { return inExamRoom; }
    public Transform GetFishExamRoom() { return fishExamPos; }

    public void UITextHandler(int textState)
    {
        switch(textState)
        {
            case 0:
                fishViewText.text = "";
                fishExitText.text = "";
                fishExamText.text = "";
                break;
            case 1:
                fishViewText.text = "Click to Follow";
                fishExitText.text = "";
                fishExamText.text = "";
                break;
            case 2:
                fishViewText.text = "";
                fishExitText.text = "Q to Stop Following";
                fishExamText.text = "E to Examine";
                break;
            case 3:
                fishViewText.text = "";
                fishExitText.text = "";
                fishExamText.text = "Press R to Return";
                break;
        }
    }

    // Examination Room Camera Handling
    public void CameraTeleport(bool returning)
    {   
        if (returning)
        {
            // Return to original position
            this.transform.position = camOrigPos;
        }
        else
        {
            // Go to exam position
            camOrigPos = this.transform.position;
            this.transform.position = camExamPos.position;
            this.transform.rotation = camExamPos.rotation;
        }
        return;
    }

    // Examination Room Fish Handling
    public void ExaminingFish(bool examining)
    {   
        if (examining)
        {
            foreach (Transform childFish in fishList)
            {
                if (childFish.name.Equals(fishExamObj.name))
                {
                    // Find target fish and bring it into exam position
                    childFish.transform.position = fishExamPos.position;
                }
            }
        }
        else
        {
            foreach (Transform childFish in fishList)
            {
                if (childFish.name.Equals(fishExamObj.name))
                {
                    // Reset fish position
                    childFish.position = fishList.position;
                }
            }
        }   
        return;
    }
}

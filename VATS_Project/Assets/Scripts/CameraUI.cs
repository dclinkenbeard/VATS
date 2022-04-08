using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/**
* <summary>
   CameraUI.cs is where you are in the exploration scene and you can choose to follow the fish and insect them in the examination room. This program controls the texts that pop up
when viewing the fish and also handles how the camera changes when you choose to examine a fish. When you go into the examniation room the screen goes from the examination room and blacks out
before opening in to the examination room. It centers the fish in the middle and lets you pane around the fish. You can then stop examining and go back into exploration scene
</summary>
*/
public class CameraUI : MonoBehaviour
{
    // Basic Interface
    public GameObject canvas;
    public GameObject basicInterface;
    public GameObject fishManager;
    TextMeshProUGUI fishViewText;
    TextMeshProUGUI fishExitText;
    TextMeshProUGUI fishExamText;
    Image transitionScreen;
    private string fishName;

    // Examination Room
    public GameObject examinationRoom;
    Transform camExamPos;
    Transform fishExamPos;
    private Vector3 camOrigPos;
    private Transform fishList;
    private GameObject fishExamObj;
    public bool inExamRoom;

    bool screenBlack = false;
    public bool examEnter = false;
    public bool examExit = false;

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

            transitionScreen = canvas.transform.GetChild(3).GetComponent<Image>();
            
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
        if (fishManager != null)
        {
            if (fishManager.GetComponent<FishManager>() != null)
            {
                fishName = transform.gameObject.GetComponent<CameraMovement>().GetFishName();
                foreach(GameObject fishChild in fishManager.GetComponent<FishManager>().FishPrefabs)
                {
                    if (fishChild.name.Equals(fishName))
                    {
                        fishExamObj = fishChild;
                    }
                }

            }
            else
            {
                fishName = "Target fish missing Fish Manager!";
            }
        }

        if (examEnter) {
            TransitionToExam();
        }

        if (examExit)
        {
            TransitionToExplore();
        }

        // get rid of screen alpha if screen is black
        if (screenBlack) {
            if (transitionScreen.color.a > 0)
            {
                Color c = transitionScreen.color;
                c.a -= 0.01f;
                transitionScreen.color = c;
            }
            else {
                screenBlack = false;
            }
        }
    }

    // turn screen black and switch to examination mode
    public void TransitionToExam() {
        screenBlack = false;
        if (transitionScreen.color.a < 1)
        {
            Color c = transitionScreen.color;
            c.a += 0.01f;
            transitionScreen.color = c;
        }
        else
        {
            screenBlack = true;
            inExamRoom = true;
            ExaminingFish(true);
            CameraTeleport(false);
            UITextHandler(3);
            examEnter = false;
        }
    }

    // turn screen black and switch to exploration mode
    public void TransitionToExplore()
    {
        screenBlack = false;
        if (transitionScreen.color.a < 1)
        {
            Color c = transitionScreen.color;
            c.a += 0.01f;
            transitionScreen.color = c;
        }
        else
        {
            screenBlack = true;
            CameraTeleport(true);
            ExaminingFish(false);
            inExamRoom = false;
            UITextHandler(2);
            examExit = false;
        }
    }
    //When in exploration mode, you can choose to follow a fish, examine the fish that leads into the examination room, and stop following the fish. These are the cases
    //that will switch depending on what option you trigger
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
            // Go to examination position
            camOrigPos = this.transform.position;
            transform.gameObject.GetComponent<CameraMovement>().SetCamDistance(camExamPos.localPosition.x);
            Debug.Log("Cam exam local pos for x" + camExamPos.localPosition.x);
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

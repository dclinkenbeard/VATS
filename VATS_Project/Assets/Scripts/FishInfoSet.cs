using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class FishInfoSet : MonoBehaviour
{
    public TextMeshProUGUI fishInfoText;
    public int fishID = -1;
    private void Update() {
        string infoText = "";
        if(fishID >= 0){
            infoText += "Name: " + gameHandler.instance.myFishList.fish[fishID].name.ToString();
            infoText += "\nScientific Name: " + gameHandler.instance.myFishList.fish[fishID].sci.ToString();
            infoText += "\nHabitat: " + gameHandler.instance.myFishList.fish[fishID].habitat.ToString();
            infoText += "\nDepth: " + gameHandler.instance.myFishList.fish[fishID].maxDepth.ToString();
            infoText += "\nSize: " + gameHandler.instance.myFishList.fish[fishID].maxSize.ToString();
        }
        fishInfoText.text = infoText;
    }

}

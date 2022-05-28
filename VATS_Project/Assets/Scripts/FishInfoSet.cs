using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;

public class FishInfoSet : MonoBehaviour
{
    string path;
    private string json;
    private JsonData itemData = new JsonData();

    public TextMeshProUGUI fishInfoText;
    public int fishID = -1;
    void Start()
    {

        path = Application.dataPath + "/JSON/Fish_Encyclopedia.JSON.txt";
        json = File.ReadAllText(path);
        itemData = JsonMapper.ToObject(json);
    }

    private void Update() {
        string infoText = "";
        if(fishID >= 0){
            infoText += "Name: " + itemData["fish"][index: fishID]["name"].ToString();
            infoText += "\nScientific Name: " + itemData["fish"][index: fishID]["sci"].ToString();
            infoText += "\nHabitat: " + itemData["fish"][index: fishID]["habitat"].ToString();
            infoText += "\nDepth: " + itemData["fish"][index: fishID]["maxDepth"].ToString();
            infoText += "\nSize: " + itemData["fish"][index: fishID]["maxSize"].ToString();
            //infoText += "\nDiet: " + itemData["fish"][index: fishID]["diet"].ToString();
            //infoText += "\nRange: " + itemData["fish"][index: fishID]["range"].ToString();
        }
        fishInfoText.text = infoText;
    }

}

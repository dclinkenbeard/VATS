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
        //creates a path to the Fish Ecyclopedia text file within /Assets/JSON, we read the
        //the file with all the details of each fish and assign it to our json variable. The code
        //then we create objects for each instance of fish in our JSON file
        path = Application.dataPath + "/JSON/Fish_Encyclopedia.JSON.txt";
        json = File.ReadAllText(path);
        itemData = JsonMapper.ToObject(json);
    }
    //Here we parse each object of Fish Encyclopedia so that we may output the charactericts
    //of a specific fish. This is used in the Exploration Scene, once you apply the conditions of
    //temperature and depth, you are able to follow a fish and this is where that fish's characteristics
    //are displayed
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

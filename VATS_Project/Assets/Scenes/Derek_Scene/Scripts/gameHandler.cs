using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;

[System.Serializable]
public class gameHandler : MonoBehaviour
{
    string path;
    private string json;
    private JsonData itemData = new JsonData();

    public SuperpositionFish sf;
    public GridToFev grid;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sciNameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI habitatText;
    public TextMeshProUGUI depthText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI dietText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI conservationText;

    public int fishID;

    void Start()
    {
        fishID = grid.FetchId();

        nameText = transform.Find("nameText").GetComponent<TextMeshProUGUI>();
        sciNameText = transform.Find("sciNameText").GetComponent<TextMeshProUGUI>();
        typeText = transform.Find("typeText").GetComponent<TextMeshProUGUI>();
        habitatText = transform.Find("habitatText").GetComponent<TextMeshProUGUI>();
        depthText = transform.Find("depthText").GetComponent<TextMeshProUGUI>();
        sizeText = transform.Find("sizeText").GetComponent<TextMeshProUGUI>();
        dietText = transform.Find("dietText").GetComponent<TextMeshProUGUI>();
        rangeText = transform.Find("rangeText").GetComponent<TextMeshProUGUI>();
        conservationText = transform.Find("statusText").GetComponent<TextMeshProUGUI>();

        // add a synopsis text

        path = Application.dataPath + "/Scenes/Derek_Scene/JSON/Fish_Encyclopedia.JSON.txt";
        json = File.ReadAllText(path);
        itemData = JsonMapper.ToObject(json);
    }

    JsonData GetItem(string name, string type)
    {
        for (int ii = 0; ii < itemData[type].Count; ii++)
        {
            if (itemData[type][ii]["name"].ToString() == name)
            {
                return itemData[type][ii];
            }
        }
        return null;
    }

    // observe fish button

    // change status UI to double circle based on fish's status

    void Update()
    {
        // fish stats displayed on UI
        nameText.text = itemData["fish"][index: fishID]["name"].ToString();
        sciNameText.text = itemData["fish"][index: fishID]["sci"].ToString();
        typeText.text = "\n" + itemData["fish"][index: fishID]["type"].ToString();
        habitatText.text = "\n" + itemData["fish"][index: fishID]["habitat"].ToString();
        depthText.text = "\n" + itemData["fish"][index: fishID]["maxDepth"].ToString();
        sizeText.text = "\n" + itemData["fish"][index: fishID]["maxSize"].ToString();
        dietText.text = "\n" + itemData["fish"][index: fishID]["diet"].ToString();
        rangeText.text = "\n" + itemData["fish"][index: fishID]["range"].ToString();
        conservationText.text = "\n" + itemData["fish"][index: fishID]["status"].ToString();
    }

}

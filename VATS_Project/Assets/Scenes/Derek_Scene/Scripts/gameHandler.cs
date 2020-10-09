using System.Collections;
using System.Collections.Generic;
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

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI habitatText;
    public TextMeshProUGUI depthText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI dietText;
    public TextMeshProUGUI rangeText;

    public int fishID;

    void Start()
    {
        nameText = transform.Find("nameText").GetComponent<TextMeshProUGUI>();
        typeText = transform.Find("typeText").GetComponent<TextMeshProUGUI>();
        habitatText = transform.Find("habitatText").GetComponent<TextMeshProUGUI>();
        depthText = transform.Find("depthText").GetComponent<TextMeshProUGUI>();
        sizeText = transform.Find("sizeText").GetComponent<TextMeshProUGUI>();
        dietText = transform.Find("dietText").GetComponent<TextMeshProUGUI>();
        rangeText = transform.Find("rangeText").GetComponent<TextMeshProUGUI>();

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

    // next fish button

    // prev fish button

    // observe fish button

    // change status UI to double circle based on fish's status

    void Update()
    {
        // fish stats displayed on UI
        nameText.text = itemData["fish"][index: fishID]["name"].ToString();
        typeText.text = "Animal type " + itemData["fish"][index: fishID]["type"].ToString();
        habitatText.text = "Habitat " + itemData["fish"][index: fishID]["habitat"].ToString();
        depthText.text = "Depth " + itemData["fish"][index: fishID]["maxDepth"].ToString();
        sizeText.text = "Size " + itemData["fish"][index: fishID]["maxSize"].ToString();
        dietText.text = "Diet " + itemData["fish"][index: fishID]["diet"].ToString();
        rangeText.text = "Range " + itemData["fish"][index: fishID]["range"].ToString();
    }

}

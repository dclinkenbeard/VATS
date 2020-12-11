using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;

public class ModelHandler : MonoBehaviour
{
    string path;
    private string json;
    private JsonData itemData = new JsonData();

    public GridToFev grid;

    public float xPos;
    public float yPos;
    public float zPos;

    public int fishID;

    void Start()
    {
        fishID = grid.FetchId();

        // add a synopsis text

        path = Application.dataPath + "/Scenes/Derek_Scene/JSON/Model_Positioning.JSON.txt";
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
        xPos = float.Parse(itemData["fish"][index: fishID]["xPos"].ToString());
        yPos = float.Parse(itemData["fish"][index: fishID]["yPos"].ToString());
        zPos = float.Parse(itemData["fish"][index: fishID]["zPos"].ToString());
    }

}

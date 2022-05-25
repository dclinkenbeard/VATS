using System.Collections;
using UnityEngine;
using System.IO;
using System;

public class ModelHandler : MonoBehaviour
{
    string path;
    private string json;
    public GridToFev grid;

    public float xPos;
    public float yPos;
    public float zPos;

    public int fishID;
    public static ModelHandler instance;
    [Serializable]
    public class Model
    {
        public int id;
        public string name;
        public float xPos;
        public float yPos;
        public float zPos;
    }
    [Serializable]
    public class ModelList
    {
        public Model[] model;
    }

    public ModelList myModelList = new ModelList();
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fishID = grid.FetchId();

        // add a synopsis text
        json = "";
        StartCoroutine(loadStreamingAsset());      
    }
    IEnumerator loadStreamingAsset()
    {
        path = Path.Combine(Application.streamingAssetsPath, "Model_PositioningJSON.txt");
        if (path.Contains("://") || path.Contains(":///"))
        {
            WWW www = new WWW(path);
            yield return www;
            json = www.text;
        }
        else
            json = File.ReadAllText(path);
        yield return new WaitUntil(() => json != "");
        myModelList = JsonUtility.FromJson<ModelList>(json);
    }

    void Update()
    {
        if(fishID < 24)
        {
            xPos = float.Parse(myModelList.model[fishID].xPos.ToString());
            yPos = float.Parse(myModelList.model[fishID].yPos.ToString());
            zPos = float.Parse(myModelList.model[fishID].zPos.ToString());
        }
    }
}

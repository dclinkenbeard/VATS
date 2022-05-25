using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using TMPro;

[Serializable]
public class gameHandler : MonoBehaviour
{
    private string path;
    private string json;
    public bool usedInFEV;
    //private JSONNode itemData;
    public static gameHandler instance;
    public SuperpositionFish sf;
    public GridToFev grid;

    public TMP_Text nameText;
    public TMP_Text sciNameText;
    public TMP_Text typeText;
    public TMP_Text habitatText;
    public TMP_Text depthText;
    public TMP_Text sizeText;
    public TMP_Text dietText;
    public TMP_Text rangeText;
    public TMP_Text conservationText;
    public int fishID;

    [Serializable]
    public class Fish
    {
        public int id;
        public string name;
        public string sci;
        public string type;
        public string diet;
        public string habitat;
        public float minSize;
        public float maxSize;
        public float minTemp;
        public float maxTemp;
        public float minDepth;
        public float maxDepth;
        public string range;
        public string status;
        public float lowLimit;
        public float uppLimit;
    }
    [Serializable]
    public class FishList
    {
        public Fish[] fish;
    }

    public FishList myFishList = new FishList();
    public void Awake()
    {
        instance = this;
        if (usedInFEV)
        {
            fishID = grid.FetchId();
            nameText = transform.Find("nameText").GetComponent<TMP_Text>();
            sciNameText = transform.Find("sciNameText").GetComponent<TMP_Text>();
            typeText = transform.Find("typeText").GetComponent<TMP_Text>();
            habitatText = transform.Find("habitatText").GetComponent<TMP_Text>();
            depthText = transform.Find("depthText").GetComponent<TMP_Text>();
            sizeText = transform.Find("sizeText").GetComponent<TMP_Text>();
            dietText = transform.Find("dietText").GetComponent<TMP_Text>();
            rangeText = transform.Find("rangeText").GetComponent<TMP_Text>();
            conservationText = transform.Find("statusText").GetComponent<TMP_Text>();
            // add a synopsis text       
        }
        json = "";
        StartCoroutine(loadStreamingAsset());
        StartCoroutine(safeFromJSON());
    }
    IEnumerator loadStreamingAsset()
    {
        path = Path.Combine(Application.streamingAssetsPath, "Fish_EncyclopediaJSON.txt");
        if (path.Contains("://") || path.Contains(":///"))
        {
            WWW www = new WWW(path);
            yield return www;
            json = www.text;
        }
        else
            json = File.ReadAllText(path); 
    }
    IEnumerator safeFromJSON()
    {
        yield return new WaitUntil(() => json != "");
        myFishList = JsonUtility.FromJson<FishList>(json);
        if (usedInFEV) { updateUI(); }
    }
    public void updateUI()
    {
        if(fishID < 24)
        {
            nameText.text = myFishList.fish[fishID].name.ToString();
            sciNameText.text = myFishList.fish[fishID].sci.ToString();
            typeText.text = "\n" + myFishList.fish[fishID].type.ToString();
            habitatText.text = "\n" + myFishList.fish[fishID].habitat.ToString();
            depthText.text = "\n" + myFishList.fish[fishID].maxDepth.ToString();
            sizeText.text = "\n" + myFishList.fish[fishID].maxSize.ToString();
            dietText.text = "\n" + myFishList.fish[fishID].diet.ToString();
            rangeText.text = "\n" + myFishList.fish[fishID].range.ToString();
            conservationText.text = "\n" + myFishList.fish[fishID].status.ToString();
        }
    }
}

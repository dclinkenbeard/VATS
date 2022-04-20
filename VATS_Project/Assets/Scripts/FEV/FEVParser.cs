﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using LitJson;

// Parser class used to read data from XML files
public class FEVParser : MonoBehaviour
{
    string assetPath = "Assets/Scripts/Boids/BoidObjects/";
    AssetBundle bundle;
    public string fevName = "Eel";

    void Start()
    {
        loadData();
    }


    void loadData()
    {
        TextAsset fevFile = Resources.Load<TextAsset>("FEVs/" + fevName);
        XmlSerializer fevSerializer = new XmlSerializer(typeof(FEV));
        using (StringReader fevReader = new StringReader(fevFile.text))
        {
            FEV fev = (fevSerializer.Deserialize(fevReader)) as FEV;
            SubmitFEV(fev);
        }
    }

    // Temp function for turning FEV data into fish. Will be part of a loader class in the future 
    public void SubmitFEV(FEV fev)
    {
        string path = Application.dataPath + "/JSON/Fish_Encyclopedia.JSON.txt";
        string json = File.ReadAllText(path);
        JsonData itemData = new JsonData();

        itemData = JsonMapper.ToObject(json);
        string newJson = JsonMapper.ToJson(fev);

        itemData["fish"].Add(JsonMapper.ToObject(newJson));


        // Writes data to JSON file in a readable format
        JsonWriter fevWriter = new JsonWriter();
        fevWriter.PrettyPrint = true;
        JsonMapper.ToJson(itemData, fevWriter);
        File.WriteAllText(path, fevWriter.ToString());


        GameObject newFish = new GameObject();
        MeshFilter filter = newFish.AddComponent<MeshFilter>() as MeshFilter;
        BoxCollider collider = newFish.AddComponent<BoxCollider>() as BoxCollider;
        BoidAgent newAgent = newFish.AddComponent<BoidAgent>() as BoidAgent;

        

        newAgent.avoidRadius = (float)fev.avoidRadius;
        newAgent.neighborRadius = (float)fev.neighborRadius;
        newAgent.collisionLength = (float)fev.collisionLength;

        newAgent.minSpeed = (float)fev.minSpeed;
        newAgent.maxSpeed = (float)fev.maxSpeed;
        newAgent.maxTurnSpd = (float)fev.maxTurnSpd;
        newAgent.fishType = fev.fishType;
        newAgent.id = fev.id;

        BoidBehavior avoidBehavior = (BoidBehavior)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Boids/BoidObjects/BoidAvoidance.asset", typeof(BoidBehavior));
        BoidBehavior alignBehavior = (BoidBehavior)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Boids/BoidObjects/BoidAlignment.asset", typeof(BoidBehavior));
        BoidBehavior cohesionBehavior = (BoidBehavior)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Boids/BoidObjects/BoidCohesion.asset", typeof(BoidBehavior));

        newAgent.boidBehaviors = new List<BoidBehavior>();
        newAgent.boidBehaviors.Add(avoidBehavior);
        newAgent.boidBehaviors.Add(alignBehavior);
        newAgent.boidBehaviors.Add(cohesionBehavior);

        newAgent.behaviorWeights = new List<float>();
        newAgent.behaviorWeights.Add(3);
        newAgent.behaviorWeights.Add(2.5f);
        newAgent.behaviorWeights.Add(2);

        newAgent.fishLayerMask = LayerMask.GetMask("Fish");
        newAgent.obstacleMask = LayerMask.GetMask("Obstacle");

        // GameObject.Instantiate(newFish, this.transform);

        // TODO: Get actual model from AssetBundle URL provided by FEV
        //GameObject tempModel = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject tempModel = GameObject.Instantiate(Resources.Load<GameObject>("EelTemp/EelModel"));
        tempModel.transform.parent = newFish.transform;

        PrefabUtility.SaveAsPrefabAssetAndConnect(newFish, "Assets/Prefabs/Boids/" + fevName + ".prefab", InteractionMode.UserAction);
    }

    void GetAssetBundleFromUrl(string url){
        StartCoroutine(GetAssetBundle(url));
    }

    IEnumerator GetAssetBundle(string url){
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();

        if(www.error != null){
            Debug.Log("www error: Could not get AssetBundle from url " + url + "\n" + www.error);
        }
        else{
            bundle = DownloadHandlerAssetBundle.GetContent(www);
        }
    }

    GameObject GetModelFromAssetBundle(AssetBundle bundle, string name){
        GameObject loadedModel = (GameObject) bundle.LoadAsset(name);
        return loadedModel;
    }
    
}

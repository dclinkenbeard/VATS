using System.Collections;
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
    public string fevName = "EelSample";

    void Start()
    {
        loadData();
    }


    // Creates an FEV object from the XML data
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

    // Uses data from the FEV object to create a new fish GameObject
    public void SubmitFEV(FEV fev)
    {
        string path = Application.dataPath + "/JSON/Fish_Encyclopedia.JSON.txt";
        string json = File.ReadAllText(path);
        JsonData itemData = new JsonData();

        itemData = JsonMapper.ToObject(json);
        string newJson = JsonMapper.ToJson(fev);

        
        // Create or update fish
        if (itemData["fish"][fev.id] != null) {
            itemData["fish"][fev.id] = JsonMapper.ToObject(newJson);
            fevName = GetFishByID(fev.id);
        } else {
            itemData["fish"].Add(JsonMapper.ToObject(newJson));
        }
        


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

        
        
       

        newAgent.boidBehaviors = new List<BoidBehavior>();

        newAgent.behaviorWeights = new List<float>();
        if (fev.hasAlignment)
        {
            BoidBehavior alignBehavior = (BoidBehavior)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Boids/BoidObjects/BoidAlignment.asset", typeof(BoidBehavior));
            newAgent.boidBehaviors.Add(alignBehavior);
            newAgent.behaviorWeights.Add((float)fev.alignmentWeight);
        }
        if (fev.hasAvoidance)
        {
            BoidBehavior avoidBehavior = (BoidBehavior)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Boids/BoidObjects/BoidAvoidance.asset", typeof(BoidBehavior));
            newAgent.boidBehaviors.Add(avoidBehavior);
            newAgent.behaviorWeights.Add((float)fev.avoidanceWeight);
        }
        if (fev.hasAlignment)
        {
             BoidBehavior cohesionBehavior = (BoidBehavior)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Boids/BoidObjects/BoidCohesion.asset", typeof(BoidBehavior));
            newAgent.boidBehaviors.Add(cohesionBehavior);
            newAgent.behaviorWeights.Add((float)fev.cohesionWeight);
        }

        newAgent.fishLayerMask = LayerMask.GetMask("Fish");
        newAgent.obstacleMask = LayerMask.GetMask("Obstacle");

        GameObject.Instantiate(newFish, this.transform);

        GetAssetBundleFromUrl(fev.modelUrl);
        GameObject model = GetModelFromAssetBundle(bundle, "model");
        model.transform.parent = newFish.transform;

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

    string GetFishByID(int id)
    {
        var fishList = Resources.LoadAll<GameObject>("Fish");

        foreach (GameObject fish in fishList)
        {
            if (fish.GetComponent<BoidAgent>().id == id)
            {
                return fish.name;
            }
        }
        return fevName;
    }
    
}

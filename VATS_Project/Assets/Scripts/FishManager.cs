using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;

[System.Serializable]
public class FishManager : MonoBehaviour
{
    //Declaring JSON/Database variables
    string path;
    private string json;
    private JsonData itemData = new JsonData();

    public TextMeshProUGUI currentFishText;
    public float depth;
    public float temp;
    public float acidity;
    public float pollution;

    //Declare a list to hold fish prefabs
    public List<GameObject> FishPrefabs = new List<GameObject>();

    //List to hold the spawn position
    List<Vector2Int> SpawnIndex = new List<Vector2Int>();

    //List to hold the current fishes that can be spawned
    List<GameObject> currentAgents = new List<GameObject>();
    

    /**
     * On Start, link the JSON database filepath 
     * and load it onto a variable 
     */
    void Start()
    {
        path = Application.dataPath + "/JSON/Fish_Encyclopedia.JSON.txt";
        json = File.ReadAllText(path);
        itemData = JsonMapper.ToObject(json);
    }

    private void Update() {
        string text = "\n";

        text += CalculateFish();

        currentFishText.text = text;

        //Debug.Log(text);
        /*
        if (Input.GetKeyDown(KeyCode.B)) {
            SpawnFish();
        }*/
    }
    // observe fish button

    // change status UI to double circle based on fish's status


    /**
     * Spawn the fish prefabs using vector 3 in specific range
     */
    public void SpawnFish(){

        //Despawn and remove all the existing fishes
        foreach(GameObject agent in currentAgents){ 
            agent.GetComponent<BoidAgent>().despawning = true;
        }

        //Clear the current fishes list
        currentAgents.Clear();

        //Loop through spawnindex vector2
        foreach(Vector2Int x in SpawnIndex){
            for (int i = 0; i < x[1]; i++)
            {
                GameObject agentPrefab = FishPrefabs[x[0]];
                GameObject agent = Instantiate(agentPrefab,
                    transform.position + Random.insideUnitSphere * 30f,
                    Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))),
                    transform
                    );

                currentAgents.Add(agent);
            }
        }

        //////////////// remove this later
        for (int i = 0; i < 400; i++)
        {
            GameObject agentPrefab = FishPrefabs[7];
            GameObject agent = Instantiate(agentPrefab,
                transform.position + Random.insideUnitSphere * 30f,
                Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))),
                transform
                );

            currentAgents.Add(agent);
        }
        /////////////////

    }

    /**
     * Calculate the fishes that can be spawned 
     * using the current slider value range
     * add them to the list and display them
     */
    private string CalculateFish()
    {
        string text = "Fish valid: \n";
        
        SpawnIndex.Clear();
        for(int i = 0; i < FishPrefabs.Count; i++){
            int id = FishPrefabs[i].GetComponent<BoidAgent>().id;

            float minTemp = float.Parse(itemData["fish"][index: id]["minTemp"].ToString());
            float maxTemp = float.Parse(itemData["fish"][index: id]["maxTemp"].ToString());

            float minDepth = float.Parse(itemData["fish"][index: id]["minDepth"].ToString());
            float maxDepth = float.Parse(itemData["fish"][index: id]["maxDepth"].ToString());

            float minAcidity = float.Parse(itemData["fish"][index: id]["minAcidity"].ToString());
            float maxAcidity = float.Parse(itemData["fish"][index: id]["maxAcidity"].ToString());

            float minPollution = float.Parse(itemData["fish"][index: id]["minPollution"].ToString());
            float maxPollution = float.Parse(itemData["fish"][index: id]["maxPollution"].ToString());

            if (temp > minTemp && temp < maxTemp 
                && depth > minDepth && depth < maxDepth
                && acidity > minAcidity && acidity < maxAcidity
                && acidity > minPollution && acidity < maxPollution)
            {
                text += itemData["fish"][index: id]["name"].ToString();
                text += "\n";

                int lowSpawn = int.Parse(itemData["fish"][index: id]["lowLimit"].ToString());
                int highSpawn = int.Parse(itemData["fish"][index: id]["uppLimit"].ToString());
                int spawnCount = Random.Range(lowSpawn, highSpawn);
                SpawnIndex.Add(new Vector2Int(i,spawnCount));
            }
        }

        return text;
    }

}

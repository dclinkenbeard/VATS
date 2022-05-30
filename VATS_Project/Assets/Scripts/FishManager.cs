using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public float acidity = 8.0f;
    public float acidityRate = 0.0f;
    public float pollution = 593.0f;
    public float pollutionRate = 0.0f;
    public float startTime = 0.0f;
    public float endTime = 0.0f;
    public float time;
    public bool intervalIsEnabled = false;



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
        FishPrefabs.AddRange(Resources.LoadAll<GameObject>("Fish"));
    }



    private void Update() 
    {
        string text = "\n";

        text += CalculateFish();

        currentFishText.text = text;

        //Debug.Log(text);
        /*
        if (Input.GetKeyDown(KeyCode.B)) {
            SpawnFish();
        }*/
    }



    /**
     * Spawn the fish prefabs using vector 3 in specific range
     */
    public void SpawnFish()
    {

        //Despawn and remove all the existing fishes
        foreach(GameObject agent in currentAgents){ 
            agent.GetComponent<BoidAgent>().despawning = true;
        }

        //Clear the current fish list
        currentAgents.Clear();

        //Loop through spawnindex vector2
        foreach(Vector2Int x in SpawnIndex)
        {
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

        // The following 12 lines were written by a previous team; unsure of the purpose, so leaving it as is for now
        // (Spawns 400 sardines to make the simulation appear more populated) Commenting it out as the hardcoded index no longer works with the new dynamic loading
        //////////////// remove this later
        // for (int i = 0; i < 400; i++)
        // {
        //     GameObject agentPrefab = FishPrefabs[7];
        //     GameObject agent = Instantiate(agentPrefab,
        //         transform.position + Random.insideUnitSphere * 30f,
        //         Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))),
        //         transform
        //         );

        //     currentAgents.Add(agent);
        // }
        /////////////////
    }



    // Updates the non-rate variables in 1 year increments.
    // (SliderScript.cs/OnApply currently executes this on a 5 second interval)
    public void accountForTimeOnInterval()
    {
        acidity = acidity + acidityRate;
        pollution = pollution + pollutionRate;
        time = time + 1.0f;
    }



    // Updates the non-rate variables using the time and rates set by the user, instantly (without interval)
    public void accountForTime() 
    {
        acidity = acidity + (acidityRate * time);
        pollution = pollution + (pollutionRate * time);
    }



    /**
     * Calculates the fish that can be spawned using the current slider values,
     * adds them to the SpawnIndex, and displays them in the "Valid Fish" box in-game.
     */
    public string CalculateFish()
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
                && pollution >= minPollution && pollution <= maxPollution)
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

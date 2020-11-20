using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using TMPro;

[System.Serializable]
public class FishManager : MonoBehaviour
{
    string path;
    private string json;
    private JsonData itemData = new JsonData();
    public TextMeshProUGUI currentFishText;
    public List<GameObject> FishPrefabs = new List<GameObject>();
    List<Vector2Int> SpawnIndex = new List<Vector2Int>();
    List<GameObject> currentAgents = new List<GameObject>();
    public float depth;
    public float temp;
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

    public void SpawnFish(){

        foreach(GameObject agent in currentAgents){ 
            agent.GetComponent<BoidAgent>().despawning = true;
        }

        currentAgents.Clear();

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
                //agent.GetComponent<BoidAgent>().obstacleMask = obstacleMask;
            }
        }

        //remove this later
        for (int i = 0; i < 100; i++)
        {
            GameObject agentPrefab = FishPrefabs[7];
            GameObject agent = Instantiate(agentPrefab,
                transform.position + Random.insideUnitSphere * 30f,
                Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))),
                transform
                );

            currentAgents.Add(agent);
            //agent.GetComponent<BoidAgent>().obstacleMask = obstacleMask;
        }

    }

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

            if(temp > minTemp && temp < maxTemp 
                && depth > minDepth && depth < maxDepth){
                text += itemData["fish"][index: id]["name"].ToString();
                text += "\n";

                int lowSpawn = int.Parse(itemData["fish"][index: id]["lowLimit"].ToString());
                int highSpawn = int.Parse(itemData["fish"][index: id]["uppLimit"].ToString());
                int spawnCount = Random.Range(lowSpawn, highSpawn);
                SpawnIndex.Add(new Vector2Int(i,spawnCount));
            }
        }

        return text;
        // fish stats displayed on UI
        /*
        nameText.text = itemData["fish"][index: fishID]["name"].ToString();
        sciNameText.text = itemData["fish"][index: fishID]["sci"].ToString();
        typeText.text = "\n" + itemData["fish"][index: fishID]["type"].ToString();
        habitatText.text = "\n" + itemData["fish"][index: fishID]["habitat"].ToString();
        depthText.text = "\n" + itemData["fish"][index: fishID]["maxDepth"].ToString();
        sizeText.text = "\n" + itemData["fish"][index: fishID]["maxSize"].ToString();
        dietText.text = "\n" + itemData["fish"][index: fishID]["diet"].ToString();
        rangeText.text = "\n" + itemData["fish"][index: fishID]["range"].ToString();
        */
    }

}

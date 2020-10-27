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
    public List<GameObject> FishPrefabs = new List<GameObject>();
    List<int> SpawnIndex = new List<int>();
    List<GameObject> currentAgents = new List<GameObject>();
    public int depth;
    public int temp;
    void Start()
    {

        path = Application.dataPath + "/Scenes/Derek_Scene/JSON/Fish_Encyclopedia.JSON.txt";
        json = File.ReadAllText(path);
        itemData = JsonMapper.ToObject(json);
    }

    private void Update() {
        string text = "\n";

        text += CalculateFish();
        Debug.Log(text);

        if (Input.GetKeyDown(KeyCode.B)) {
            SpawnFish();
        }
    }
    // observe fish button

    // change status UI to double circle based on fish's status

    void SpawnFish(){

        foreach(GameObject agent in currentAgents){ 
            agent.GetComponent<BoidAgent>().despawning = true;
        }

        currentAgents.Clear();

        foreach(int x in SpawnIndex){
            for (int i = 0; i < 10; i++)
            {
                GameObject agentPrefab = FishPrefabs[x];
                GameObject agent = Instantiate(agentPrefab,
                    transform.position + Random.insideUnitSphere * 30f,
                    Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))),
                    transform
                    );

                currentAgents.Add(agent);
                //agent.GetComponent<BoidAgent>().obstacleMask = obstacleMask;
            }
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

                SpawnIndex.Add(i);
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

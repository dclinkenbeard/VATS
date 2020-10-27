using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFish : MonoBehaviour
{
    public gameHandler gh;
    public GameObject fishModel;
    public Renderer fishMesh;
    public bool activateFish;
    private int fishTag;

    // Start is called before the first frame update

    public void Awake()
    {
        fishModel = GameObject.Find(fishTag.ToString());
        fishMesh = GetComponent<Renderer>();
    }
    void Start()
    {
        activateFish = true;

        fishModel.SetActive(true);
        fishModel = GameObject.Find(fishTag.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        fishTag = gh.fishID;
        fishModel = GameObject.Find(fishTag.ToString());
        DeactivateFish();
        InstaFish();
    }

    public void InstaFish()
    {
        activateFish = true;

        if (activateFish)
        {
            fishMesh = fishModel.GetComponent<Renderer>();

            if (!fishMesh.enabled)
            {
                fishMesh.enabled = true;
            }
        }
    }

    public void DeactivateFish()
    {
        activateFish = false;

        if (!activateFish)
        {
            fishMesh = fishModel.GetComponent<Renderer>();
            fishMesh.enabled = false;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperpositionFish : MonoBehaviour
{
    public gameHandler gh;
    public GameObject fishModel;
    public GameObject nextFishModel;
    private int fishTag;
    public int nextFish;

    void Start()
    {
        fishTag = gh.fishID;
        nextFish = gh.fishID + 1;

        fishModel = GameObject.Find(fishTag.ToString());
        nextFishModel = GameObject.Find(nextFish.ToString());

        fishModel.transform.position = new Vector3(-37, 0, 70);

    }

    void Update()
    {
        fishTag = gh.fishID;
        nextFish = fishTag + 1;
        fishModel = GameObject.Find(fishTag.ToString());
        nextFishModel = GameObject.Find(nextFish.ToString());
    }

    public void LBSuperposition()
    {
        fishTag = gh.fishID;
        nextFish = gh.fishID + 1;

        fishModel = GameObject.Find(fishTag.ToString());
        nextFishModel = GameObject.Find(nextFish.ToString());

        if (fishTag > -1)
        {
            Debug.Log("lb");

            nextFishModel.transform.position = new Vector3(0, 30, 0);
            fishModel.transform.position = new Vector3(-37, 0, 70);
        }

    }

    public void RBSuperposition()
    {
        fishTag = gh.fishID;
        nextFish = gh.fishID + 1;

        Debug.Log(fishModel.transform.position);

        if (nextFish < 5)
        {
            Debug.Log("rb");

            fishModel.transform.position = new Vector3(0, 30, 0);
            nextFishModel.transform.position = new Vector3(-37, 0, 70);
        }
/*        else if (nextFish < 4)
        {
            nextFishModel.transform.position = new Vector3(0, 30, 0);
            fishModel.transform.position = new Vector3(-37, 0, 70);
        }*/
    }

    public void Increase()
    {
        if (nextFish < 5)
        {
            gh.fishID++;
            nextFish++;
        }
    }
    public void Decrease()
    {
        if (fishTag > 0)
        {
            gh.fishID--;
            nextFish--;
        }
    }
}

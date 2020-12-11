using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperpositionFish : MonoBehaviour
{
    public ModelHandler mh;
    public GridToFev grid;
    public gameHandler gh;

    public GameObject firstModel;
    public GameObject fishModel;
    public GameObject nextFishModel;

    public Vector3 fishPos;
    public static int fishTag;
    public int nextFish;

    public float xPos;
    public float yPos;
    public float zPos;


    void Start()
    {
        fishTag = grid.FetchId();
        nextFish = grid.FetchId() + 1;

        fishPos = grid.FetchPos();

        fishModel = GameObject.Find(fishTag.ToString());
        nextFishModel = GameObject.Find(nextFish.ToString());



        // makes model 0 start off at desired position
        if (grid.FetchId() == 0)
        {
            firstModel.transform.localPosition = new Vector3(40, 0, -25);
        }
        else
        {
            firstModel.transform.localPosition = new Vector3(0, 90, 0);
        }

        fishModel.transform.localPosition = fishPos;
        Debug.Log(fishModel.transform.localPosition);
    }

    void Update()
    {
        fishTag = mh.fishID;
        nextFish = mh.fishID + 1;
        fishModel = GameObject.Find(fishTag.ToString());
        nextFishModel = GameObject.Find(nextFish.ToString());

        xPos = mh.xPos;
        yPos = mh.yPos;
        zPos = mh.zPos;

        Debug.Log(xPos);
    }

    public void LBSuperposition()
    {
        fishTag = mh.fishID;
        nextFish = mh.fishID + 1;

        fishModel = GameObject.Find(fishTag.ToString());
        nextFishModel = GameObject.Find(nextFish.ToString());

        if (fishTag > -1)
        {

            nextFishModel.transform.localPosition = new Vector3(0, 0, 150);
            fishModel.transform.localPosition = new Vector3(xPos, yPos, zPos);
            //Debug.Log(fishModel.transform.position);

        }

    }

    public void RBSuperposition()
    {
        fishTag = mh.fishID;
        nextFish = mh.fishID + 1;

        //Debug.Log(fishModel.transform.position);

        if (nextFish < 25)
        {
            Debug.Log("rb");

            fishModel.transform.localPosition = new Vector3(0, 0, 150);
            nextFishModel.transform.localPosition = new Vector3(xPos, yPos, zPos);
            //Debug.Log(fishModel.transform.position);

        }
        /*        else if (nextFish < 4)
                {
                    nextFishModel.transform.position = new Vector3(0, 30, 0);
                    fishModel.transform.position = new Vector3(-37, 0, 70);
                }*/
    }

    public void Increase()
    {
        if (nextFish < 25)
        {
            mh.fishID++;
            nextFish++;
            gh.fishID++;
        }
    }
    public void Decrease()
    {
        if (fishTag > 0)
        {
            mh.fishID--;
            nextFish--;
            gh.fishID--;
        }
    }
}


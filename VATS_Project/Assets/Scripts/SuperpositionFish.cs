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
        if (grid.FetchId() == 0)
        {
            firstModel.transform.localPosition = new Vector3(40, 0, -25);
        }
        else
        {
            firstModel.transform.localPosition = new Vector3(0, 200, 0);
        }

        fishModel.transform.localPosition = fishPos;
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
    }

    public void LBSuperposition()
    {
        fishTag = mh.fishID;
        nextFish = mh.fishID + 1;

        fishModel = GameObject.Find(fishTag.ToString());
        nextFishModel = GameObject.Find(nextFish.ToString());

        if (fishTag > -1)
        {
            nextFishModel = GameObject.Find(nextFish.ToString());
            nextFishModel.transform.localPosition = new Vector3(0, 0, 200);
            fishModel.transform.localPosition = new Vector3(xPos, yPos, zPos);
        }
    }

    public void RBSuperposition()
    {
        fishTag = mh.fishID;
        nextFish = mh.fishID + 1;

        if (nextFish < 25)
        {
            fishModel.transform.localPosition = new Vector3(0, 0, 200);
            nextFishModel.transform.localPosition = new Vector3(xPos, yPos, zPos);
        }
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


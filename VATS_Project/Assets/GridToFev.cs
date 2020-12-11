using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridToFev : MonoBehaviour
{
    public ModelHandler mh;

    public string id;
    public static int fishID;

    public float xPos;
    public float yPos;
    public float zPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FetchFishId()
    {
        //Debug.Log(this.gameObject.name);
        id = this.gameObject.name;

        fishID = int.Parse(id);
    }

    public void StoreId()
    {
        PlayerPrefs.SetInt("intId", fishID);
        Debug.Log(fishID);
    }

    public int FetchId()
    {
        fishID = PlayerPrefs.GetInt("intId");
        Debug.Log(fishID);
        return fishID;
    }

    public void FetchFishPos()
    {
        xPos = mh.xPos;
        yPos = mh.yPos;
        zPos = mh.zPos;
    }

    public void StorePos()
    {
        PlayerPrefs.SetFloat("posX", xPos);
        PlayerPrefs.SetFloat("posY", yPos);
        PlayerPrefs.SetFloat("posZ", zPos);
    }

    public Vector3 FetchPos()
    {
        xPos = PlayerPrefs.GetFloat("posX");
        yPos = PlayerPrefs.GetFloat("posY");
        zPos = PlayerPrefs.GetFloat("posZ");

        return new Vector3(xPos, yPos, zPos);
    }

    public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}

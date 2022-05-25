using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridToFev : MonoBehaviour
{
    public ModelHandler mh;

    public string id;
    public static int fishID;

    public float xPos;
    public float yPos;
    public float zPos;

    public void FetchFishId()
    {
        id = this.gameObject.name;
        fishID = int.Parse(id);
    }

    public void StoreId()
    {
        PlayerPrefs.SetInt("intId", fishID);
    }

    public int FetchId()
    {
        fishID = PlayerPrefs.GetInt("intId");
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
        SceneManager.LoadScene("FEV");
    }
}

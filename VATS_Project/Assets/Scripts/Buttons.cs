using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public gameHandler gh;
    //public DisplayFish df;
    public SuperpositionFish sf;
    public GameObject fishModel;
    
    //public GameObject prefab;
    public int fishTag;
    //public ShowModelController mc;
    //[SerializeField] private ShowModelButton buttonPrefab;


    // Start is called before the first frame update
    void Start()
    {
        InstaFish();
        //var models = FindObjectOfType<ShowModelController>().GetModels();

    }

    // Update is called once per frame
    void Update()
    {
        fishTag = gh.fishID;
        //prefab = GameObject.FindWithTag(fishTag.ToString());
        string path = "Prefabs/fish" + gh.fishID.ToString() + ".prefab";


        //prefab = (GameObject)Resources.Load(path, typeof(GameObject));
    }

    public void NextButton()
    {
        sf.Increase();

        sf.RBSuperposition();
        //mh.fishID++;
        //sf.activateFish = true;

        /*        if (sf.activateFish)
                {*/
        //sf.Superposition();
        //}

        //sf.Superposition();

        //sf.Deposition();
        //Destroy(prefab);
        //Debug.Log("Destroy and move on");
        /*        mc.EnableModel(fishModel.transform);
        */
        /*        df.DeactivateFish();
                df.InstaFish();*/




    }

    public void PrevButton()
    {
        sf.Decrease();

        sf.LBSuperposition();
        //mh.fishID--;
        //sf.activateFish = true;

        /*        if (sf.activateFish)
                {*/
        //sf.Superposition();
        //}

        //sf.Superposition();

        //sf.Deposition();
        // Destroy(prefab);

        /*        df.DeactivateFish();
                df.InstaFish();*/
    }

    public void InstaFish()
    {

    }
}

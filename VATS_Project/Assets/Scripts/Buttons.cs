using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     This class appears to be unused. Documentation (or deletion) of this class will be postponed until its
///     purpose is discovered.
/// </summary>
public class Buttons : MonoBehaviour
{
    public gameHandler gh;
    //public DisplayFish df;
    public SuperpositionFish sf;
    public GameObject fishModel;
    public int fishTag;
    
    //public GameObject prefab;
    //public ShowModelController mc;
    //[SerializeField] private ShowModelButton buttonPrefab;

    // todo
    void Start()
    {
        InstaFish();
        //var models = FindObjectOfType<ShowModelController>().GetModels();
    }

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

        // gh.fishID++;
        // sf.activateFish = true;

        // if (sf.activateFish)
        // {
        //     sf.Superposition();
        // }

        // sf.Superposition();

        // sf.Deposition();

        // Destroy(prefab);

        // Debug.Log("Destroy and move on");

        // mc.EnableModel(fishModel.transform);
        
        // df.DeactivateFish();
        // df.InstaFish();
    }

    public void PrevButton()
    {
        sf.Decrease();
        sf.LBSuperposition();

        // gh.fishID--;
        // sf.activateFish = true;

        // if (sf.activateFish)
        // {
        //     sf.Superposition();
        // }

        // sf.Superposition();

        // sf.Deposition();
        // Destroy(prefab);

        // df.DeactivateFish();
        // df.InstaFish();
    }

    // todo
    public void InstaFish()
    {

    }
}

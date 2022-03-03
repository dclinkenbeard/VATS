/*  
*   Gets fish render and sets random range of movement for sardine fish
*   Not active in conservation mode
*   Path: ExplorationScene/ExaminationRoom/FishList/BoidSardine/SimpleSardine/SardineSkin
*/  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMotion : MonoBehaviour 
{
    private Material fishMaterial;
	void Start ()
    {
        fishMaterial = GetComponent<SkinnedMeshRenderer>().material;
        fishMaterial.SetFloat("_MoveOffset", Random.Range(0.0f, 3.14f));
	}
}

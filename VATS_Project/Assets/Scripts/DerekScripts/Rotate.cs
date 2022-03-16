using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the fish in Derek_Scene (not currently active)
/// </summary>
public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}

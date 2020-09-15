using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSphereDirection : MonoBehaviour
{
    public int numPoints = 1000;
    public float turnFraction = 0.618033f;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numPoints/2; i++)
        {
            float t = i / (numPoints - 1f);
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * turnFraction * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            Instantiate(ball, new Vector3(x, y, z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

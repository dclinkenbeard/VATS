using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : MonoBehaviour
{
    public Material _mat;
    public Vector4 _offset;
    public float _waveSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _offset.x += _waveSpeed;
        _offset.y += _waveSpeed;
        _mat.SetVector("_NoiseOffset", _offset);
    }
}

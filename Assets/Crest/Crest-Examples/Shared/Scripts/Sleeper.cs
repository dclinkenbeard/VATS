﻿// Crest Ocean System

// This file is subject to the MIT License as seen in the root of this folder structure (LICENSE)

using UnityEngine;

/// <summary>
/// Adds a sleep/freeze into the update, can be used to inflate the frame time.
/// </summary>
public class Sleeper : MonoBehaviour
{
    public int _sleepMs = 0;
    public bool _jitter = false;
    public int _sleepStride = 1;

    void Update()
    {
        if (Crest.OceanRenderer.FrameCount % _sleepStride == 0)
        {
            var sleep = _jitter ? (int)(Random.value * _sleepMs) : _sleepMs;
            System.Threading.Thread.Sleep(sleep);
        }
    }
}

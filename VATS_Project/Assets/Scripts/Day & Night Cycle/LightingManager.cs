﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class dictates how light is shown in the simulation
/// </summary>
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    // References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    // Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] private float TimeSpeed;
    public bool RunDay;
    public bool UsingRealWorldTime;

    private void UpdateLighting()
    {
        if (Preset == null) return;

        if (UsingRealWorldTime)
        {
            Debug.Log("System Time is " + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + ":" + System.DateTime.Now.Second);
        }

        if (RunDay)
        {
            if (UsingRealWorldTime)
            {
                TimeOfDay = RealTimeCalculation();
            }
            else
            {
                if (Application.isPlaying)
                {
                    TimeOfDay += (Time.deltaTime / TimeSpeed);
                    TimeOfDay %= 24; // Clamp between 0-24
                }
            }
            
        }

        CalculateNewLighting(TimeOfDay / 24.0f);
    }

    /// <summary>
    /// Gets the current time of day for the user and returns it.
    /// </summary>
    private float RealTimeCalculation()
    {
        float CurrentTimeOfDay_;
        float hour = System.DateTime.Now.Hour;
        float minute = System.DateTime.Now.Minute;

        CurrentTimeOfDay_ = hour + (minute / 60.0f);
        return CurrentTimeOfDay_;
    }

    private void CalculateNewLighting(float timePercentage)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercentage);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercentage);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercentage);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercentage * 360.0f) - 90.0f, 270, 0));
        }
    }

/// <summary>
/// This method sets correct light to DirectionalLight variable
/// </summary>
    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}


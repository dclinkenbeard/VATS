﻿// Crest Ocean System

// This file is subject to the MIT License as seen in the root of this folder structure (LICENSE)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;
    

/// <summary>
/// Provides out-scattering based on the camera's underwater depth. It scales down environmental lighting
/// (directional light, reflections, ambient etc) with the underwater depth. This works with vanilla lighting, but 
/// uncommon or custom lighting will require a custom solution (use this for reference).
/// </summary>
public class UnderwaterEnvironmentalLighting : MonoBehaviour
{
    Light _primaryLight;
    float _lightIntensity;
    float _ambientIntensity;
    float _reflectionIntensity;
    float _fogDensity;

    float _averageDensity = 0f;

    public const float DEPTH_OUTSCATTER_CONSTANT = 0.25f;

    bool _isInitialised = false;

    void Start()
    {
        if (OceanRenderer.Instance == null)
        {
            enabled = false;
            return;
        }

        // Check to make sure the property exists. We might be using a test material.
        if (!OceanRenderer.Instance.OceanMaterial.HasProperty("_DepthFogDensity"))
        {
            enabled = false;
            return;
        }

        _primaryLight = OceanRenderer.Instance._primaryLight;

        // Store lighting settings
        if (_primaryLight)
        {
            _lightIntensity = _primaryLight.intensity;
        }
        _ambientIntensity = RenderSettings.ambientIntensity;
        _reflectionIntensity = RenderSettings.reflectionIntensity;
        _fogDensity = RenderSettings.fogDensity;

        Color density = OceanRenderer.Instance.OceanMaterial.GetColor("_DepthFogDensity");
        _averageDensity = (density.r + density.g + density.b) / 3f;

        _isInitialised = true;
    }

    void OnDisable()
    {
        if (!_isInitialised)
        {
            return;
        }

        // Restore lighting settings
        if (_primaryLight)
        {
            _primaryLight.intensity = _lightIntensity;
        }
        RenderSettings.ambientIntensity = _ambientIntensity;
        RenderSettings.reflectionIntensity = _reflectionIntensity;
        RenderSettings.fogDensity = _fogDensity;

        _isInitialised = false;
    }

    void LateUpdate()
    {
        if (OceanRenderer.Instance == null)
        {
            return;
        }

        float depthMultiplier = Mathf.Exp(_averageDensity *
            Mathf.Min(OceanRenderer.Instance.ViewerHeightAboveWater * DEPTH_OUTSCATTER_CONSTANT, 0f));

        // Darken environmental lighting when viewer underwater
        if (_primaryLight)
        {
            _primaryLight.intensity = Mathf.Lerp(0, _lightIntensity, depthMultiplier);
        }
        RenderSettings.ambientIntensity = Mathf.Lerp(0, _ambientIntensity, depthMultiplier);
        RenderSettings.reflectionIntensity = Mathf.Lerp(0, _reflectionIntensity, depthMultiplier);
        RenderSettings.fogDensity = Mathf.Lerp(0, _fogDensity, depthMultiplier);
    }
}



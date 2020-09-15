using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool ActualTime;

    private void Update()
    {
        if(Preset == null)
        {
            return;
        }

        if(ActualTime)
        {
            Debug.Log("System Time is " + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + ":" + System.DateTime.Now.Second);
        }

        if (RunDay)
        {
            if (Application.isPlaying)
            {
                if(ActualTime)
                {
                    TimeOfDay = RealTimeCalculation();
                }
                else
                {
                    TimeOfDay += (Time.deltaTime / TimeSpeed);
                    TimeOfDay %= 24; // Clamp between 0-24
                }
            }
            else
            {
                if (ActualTime)
                {
                    TimeOfDay = RealTimeCalculation();
                }
            }
        }

        UpdateLighting(TimeOfDay / 24.0f);
    }

    private float RealTimeCalculation()
    {
        float TimeOfDay_;
        float hour = System.DateTime.Now.Hour;
        float minute = System.DateTime.Now.Minute;

        TimeOfDay_ = hour + (minute / 60.0f);
        return TimeOfDay_;   
    }

    private void UpdateLighting(float timePercentage)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercentage);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercentage);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercentage);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercentage * 360.0f) - 90.0f, 270, 0));
        }
    }

    private void OnValidate()
    {
        if(DirectionalLight != null)
        {
            return;
        }

        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}


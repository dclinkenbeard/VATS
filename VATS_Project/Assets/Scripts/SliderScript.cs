/**
 * SliderScript.cs
 * Description: This script connects the UI Slider of Conservation Mode with
 *              FEV to see the effect of different values on the sea life
 * Last updated: 02/25/2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class SliderScript : MonoBehaviour
{
    //Declare sliders
    public UnityEngine.UI.Slider tempSlider;
    public UnityEngine.UI.Slider presSlider;
    public UnityEngine.UI.Slider acidSlider;
    public UnityEngine.UI.Slider timeSlider;

    //Declare float variables
    public float tempValue;
    public float presValue;
    public float acidValue;
    public float timeValue;

    //Declare input field text values
    public InputField tempText;
    public InputField presText;
    public InputField acidText;
    public InputField timeText;

    //Import and declare fishManager class
    public FishManager fishManager;

    private void Start()
    {
        if(tempSlider == null || presSlider == null || acidSlider == null || timeSlider == null)
        {
            Debug.Log("ERROR: MISSING SLIDER GAME OBJECT IN SLIDERSCRIPT");
        }
        else
        {
            tempText.text = tempValue.ToString();
            presText.text = presValue.ToString();
            acidText.text = acidValue.ToString();
            timeText.text = timeValue.ToString();
        }
    }

    // Temperature Slider & Input Field
    public void TemperatureSlider(float newValue)
    {
        tempValue = newValue;
        tempSlider.value = tempValue;
        tempText.text = tempValue.ToString();
        fishManager.temp = tempValue;
    }

    /**
     * On Temperature slider value change, this function is called
     * parsse float from string and set it as new value
     */
    public void TempInput(string text)
    {
        if(text == null)
        {
            text = "0";
        }
        float newValue = float.Parse(text);
        newValue = CheckNewValue(tempSlider, newValue);
        TemperatureSlider(newValue);
    }

    // Pressure Slider & Input Field
    public void PressureSlider(float newValue)
    {
        presValue = newValue;
        presSlider.value = presValue;
        presText.text = presValue.ToString();
        fishManager.depth = presValue;
    }

    /**
     * On Pressure slider value change, this function is called
     * parsse float from string and set it as new value
     */
    public void PresInput(string text)
    {
        if (text == null)
        {
            text = "0";
        }
        float newValue = float.Parse(text);
        newValue = CheckNewValue(presSlider, newValue);
        PressureSlider(newValue);
    }

    // Acidity Slider & Input Field
    public void AciditySlider(float newValue)
    {
        acidValue = newValue;
        acidSlider.value = acidValue;
        acidText.text = acidValue.ToString();
        fishManager.acidity = acidValue;
    }

    /**
     * On Acidity slider value change, this function is called
     * parsse float from string and set it as new value
     * --- NOT IMPLEMENTED YET ---
     */
    public void AcidInput(string text)
    {
        if (text == null)
        {
            text = "0";
        }
        float newValue = float.Parse(text);
        newValue = CheckNewValue(acidSlider, newValue);
        AciditySlider(newValue);
    }

    // Time Slider & Input Field
    public void TimeSlider(float newValue)
    {
        timeValue = newValue;
        timeSlider.value = timeValue;
        timeText.text = timeValue.ToString();
    }

    /**
     * On time slider value change, this function is called
     * parsse float from string and set it as new value
     * --- NOT IMPLEMENTED YET ---
     */
    public void TimeInput(string text)
    {
        if (text == null)
        {
            text = "0";
        }
        float newValue = float.Parse(text);
        newValue = CheckNewValue(timeSlider, newValue);
        TimeSlider(newValue);
    }

    // On Apply Button
    public void OnApply()
    {
        fishManager.SpawnFish();
        Debug.Log("SENDING VALUES TO SCRIPT");
    }

    /**
     * Check for current value of the slider and set it accordingly
     */
    private float CheckNewValue(UnityEngine.UI.Slider slider, float newValue)
    {
        float returnValue;
        if (newValue > slider.maxValue)
        {
            returnValue = slider.maxValue;
        }

        if (newValue < slider.minValue)
        {
            returnValue = slider.minValue;
        }
        else
        {
            returnValue = newValue;
        }
        return returnValue;
    }
}

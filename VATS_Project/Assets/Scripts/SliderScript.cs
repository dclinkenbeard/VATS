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
    public UnityEngine.UI.Slider aciditySlider;
    public UnityEngine.UI.Slider acidityRateSlider;
    public UnityEngine.UI.Slider timeSlider;
    public UnityEngine.UI.Slider pollutionSlider;
    public UnityEngine.UI.Slider pollutionRateSlider;

    //Declare float variables
    public float tempValue;
    public float presValue;
    public float acidityValue = 8.0f;
    public float acidityRateValue = 0.0f;
    public float timeValue;
    public float pollutionValue = 0.0f;
    public float pollutionRateValue = 0.0f;

    //Declare input field text values
    public InputField tempText;
    public InputField presText;
    public InputField acidityText;
    public InputField acidityRateText;
    public InputField timeText;
    public InputField pollutionText;
    public InputField pollutionRateText;

    //Import and declare fishManager class
    public FishManager fishManager;

    private void Start()
    {
        if(tempSlider == null 
            || presSlider == null 
            || aciditySlider == null 
            || acidityRateSlider == null 
            || timeSlider == null 
            || pollutionSlider == null
            || pollutionRateSlider == null)
        {
            Debug.Log("ERROR: MISSING SLIDER GAME OBJECT IN SLIDERSCRIPT");
        }
        else
        {
            tempText.text = tempValue.ToString();
            presText.text = presValue.ToString();
            acidityText.text = acidityValue.ToString();
            acidityRateText.text = acidityRateValue.ToString();
            timeText.text = timeValue.ToString();
            pollutionText.text = pollutionValue.ToString();
            pollutionRateText.text = pollutionValue.ToString();
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
     * parse float from string and set it as new value
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
        // rate of change in pH
        acidityValue = newValue;
        aciditySlider.value = acidityValue;
        acidityText.text = acidityValue.ToString();
        fishManager.acidity = acidityValue;
        //Debug.Log(fishManager.acidity);
    }

    /**
     * On Acidity slider value change, this function is called
     * parse float from string and set it as new value
     */
     // modifies the value in the input field next to the slider
    public void AcidityInput(string text)
    {
        if (text == null)
        {
            text = "0";
        }
        float newValue = float.Parse(text);
        newValue = CheckNewValue(aciditySlider, newValue);
        AciditySlider(newValue);
    }



    // Acidity Slider & Input Field
    public void AcidityRateSlider(float newValue)
    {
        // rate of change in pH
        acidityRateValue = newValue;
        acidityRateSlider.value = acidityRateValue;
        acidityRateText.text = acidityRateValue.ToString();
        fishManager.acidityRate = acidityRateValue;
        //Debug.Log(fishManager.acidityRate);
    }

    public void AcidityRateInput(string text)
    {
        if (text == null)
        {
            text = "0";
        }
        float newValue = float.Parse(text);
        newValue = CheckNewValue(acidityRateSlider, newValue);
        AcidityRateSlider(newValue);
    }



    // Pollution Slider & Input Field
    public void PollutionSlider(float newValue)
    {
        pollutionValue = newValue;
        pollutionSlider.value = pollutionValue;
        pollutionText.text = pollutionValue.ToString();
        fishManager.pollution = pollutionValue;
    }

    /**
     * On Pollution slider value change, this function is called
     * parse float from string and set it as new value
     * --- NOT IMPLEMENTED YET ---
     */
    public void PollutionInput(string text)
    {
       if (text == null)
        {
            text = "0";
        }
        float newValue = float.Parse(text);
        newValue = CheckNewValue(pollutionSlider, newValue);
        PollutionSlider(newValue);
    }

    // Pollution Slider & Input Field
    public void PollutionRateSlider(float newValue)
    {
        // rate of change in ...
        pollutionRateValue = newValue;
        pollutionRateSlider.value = pollutionRateValue;
        pollutionRateText.text = pollutionRateValue.ToString();
        fishManager.pollutionRate = pollutionRateValue;
        //Debug.Log(fishManager.pollutionRate);
    }

    public void PollutionRateInput(string text)
    {
        if (text == null)
        {
            text = "0";
        }
        float newValue = float.Parse(text);
        newValue = CheckNewValue(pollutionRateSlider, newValue);
        PollutionRateSlider(newValue);
    }


    // Time Slider & Input Field
    public void TimeSlider(float newValue)
    {
        timeValue = newValue;
        timeSlider.value = timeValue;
        timeText.text = timeValue.ToString();
        fishManager.time = timeValue;
        
        
    }


    /**
     * On time slider value change, this function is called
     * parse float from string and set it as new value
     * --- NOT IMPLEMENTED YET ---
     */
    // public void TimeInput(string text)
    // {
    //     if (text == null)
    //     {
    //         text = "0";
    //     }
    //     float newValue = float.Parse(text);
    //     newValue = CheckNewValue(timeSlider, newValue);
    //     TimeSlider(newValue);
    // }

    // On Apply Button
    public void OnApply()
    {

        fishManager.accountForTime();
        AciditySlider(fishManager.acidity);
        AcidityInput(fishManager.acidity.ToString());
        PollutionSlider(fishManager.pollution);
        PollutionInput(fishManager.pollution.ToString());

        fishManager.CalculateFish();
        fishManager.SpawnFish();

        // Debug.Log("SENDING VALUES TO SCRIPT");
        // Debug.Log(fishManager.acidity);
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

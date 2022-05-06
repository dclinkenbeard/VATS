/**
 * SliderScript.cs
 * Description: This script connects the UI Slider of Conservation Mode with
 *              FEV to see the effect of different values on the sea life
 * Last updated: 05/06/2022
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
    public UnityEngine.UI.Toggle intervalToggle;

    //Declare float variables
    public float tempValue;
    public float presValue;
    public float acidityValue = 8.0f;
    public float acidityRateValue = 0.0f;
    public float timeValue;
    public float pollutionValue = 593.0f;
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
        // first make sure all sliders have been properly linked to this script in Unity
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
            pollutionRateText.text = pollutionRateValue.ToString();
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
     * On Temperature slider value change, this function is called.
     * Parse float from string and set it as new value.
     * Modifies the value in the input field next to the slider.
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
     * On Pressure (depth) slider value change, this function is called.
     * Parse float from string and set it as new value.
     * Modifies the value in the input field next to the slider.
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
        // currently represented in-game as pH
        acidityValue = newValue;
        aciditySlider.value = acidityValue;
        acidityText.text = acidityValue.ToString();
        fishManager.acidity = acidityValue;
    }



    /**
     * On Acidity slider value change, this function is called.
     * Parse float from string and set it as new value.
     * Modifies the value in the input field next to the slider.
     */
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



    // Acidity Rate Slider & Input Field
    public void AcidityRateSlider(float newValue)
    {
        // rate of change in pH
        acidityRateValue = newValue;
        acidityRateSlider.value = acidityRateValue;
        acidityRateText.text = acidityRateValue.ToString();
        fishManager.acidityRate = acidityRateValue;
    }


    // Functions the same way as the inputs for static (non-rate) variables,
    // but is not updated when apply button is clicked.
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
     * On Pollution slider value change, this function is called.
     * Parse float from string and set it as new value.
     * Modifies the value in the input field next to the slider.
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



    // Pollution Rate Slider & Input Field
    public void PollutionRateSlider(float newValue)
    {
        // rate of change in pollution (plastics, as a starting point)
        pollutionRateValue = newValue;
        pollutionRateSlider.value = pollutionRateValue;
        pollutionRateText.text = pollutionRateValue.ToString();
        fishManager.pollutionRate = pollutionRateValue;
    }



    // Functions the same way as the inputs for static (non-rate) variables,
    // but is not updated when apply button is clicked.
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



    // Determines whether changes over time will be applied on one year intervals.
    // Controlled by a checkbox in the in-game slider menu.
    public void ToggleInterval()
    {
        fishManager.intervalIsEnabled = fishManager.intervalIsEnabled ? false : true;
    }



    // Applies values differently depending on whether the interval toggle box was checked.
    public void ApplyValues()
    {
        if (fishManager.intervalIsEnabled)
        {
            // Values will be applied in 1 year increments
            fishManager.accountForTimeOnInterval();

            // Once the original specified time value has been reached, stop incrementing
            if (fishManager.time >= fishManager.endTime)
            {
                CancelInvoke("ApplyValues");
            }
        }
        else
        {
            // Values will be applied instantly for the amount of time specified
            fishManager.accountForTime();
        }

        // All sliders that are affected by rate of change must be updated regardless of whether
        // interval has been enabled
        AciditySlider(fishManager.acidity);
        AcidityInput(fishManager.acidity.ToString());
        PollutionSlider(fishManager.pollution);
        PollutionInput(fishManager.pollution.ToString());
        TimeSlider(fishManager.time);
        TimeInput(fishManager.time.ToString());

        // Use the new values to determine which fish can exist, as directed by the fish dictionary,
        // then spawn the fish
        fishManager.CalculateFish();
        fishManager.SpawnFish();
    }



    // Executes ApplyValues when "Apply" button is clicked.
    public void OnApply()
    {
        // If interval is enabled, then ApplyValues must be invoked more than once
        if (fishManager.intervalIsEnabled)
        {
            // Starting, end, and current times must be reset every time "Apply" button has been clicked
            // if interval is enabled
            fishManager.startTime = 0;
            fishManager.endTime = fishManager.time;
            fishManager.time = fishManager.startTime;

            // New values are applied every 5 seconds, starting immediately 
            // (consider allowing the user to set the interval in the future)
            InvokeRepeating("ApplyValues", 0.0f, 5.0f);
        }
        else
        {
            ApplyValues();
        }
    }



    /**
     * Checks for current value of the slider and sets it accordingly.
     * Ensures the value set by the user cannot exceed the maximum and minimum boundaries for each slider.
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

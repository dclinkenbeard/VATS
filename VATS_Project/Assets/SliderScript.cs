using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public UnityEngine.UI.Slider tempSlider;
    public UnityEngine.UI.Slider presSlider;
    public UnityEngine.UI.Slider acidSlider;
    public UnityEngine.UI.Slider timeSlider;

    public float tempValue;
    public float presValue;
    public float acidValue;
    public float timeValue;

    public InputField tempText;
    public InputField presText;
    public InputField acidText;
    public InputField timeText;

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
    }

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
    }

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
    }

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
        Debug.Log("SENDING VALUES TO SCRIPT");
    }

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

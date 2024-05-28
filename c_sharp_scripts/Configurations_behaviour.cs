using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Configurations_behaviour : MonoBehaviour
{
    public TMP_Dropdown dropdown1;
    public TMP_Dropdown dropdown2;
    private int value;
    private string type;

    void Start()
    {
        // Subscribe to the dropdown's OnValueChanged event
        dropdown1.onValueChanged.AddListener(delegate { SizeChangeValue(dropdown1); });
        dropdown2.onValueChanged.AddListener(delegate { SizeChangeValue(dropdown2); });
    }

    public void SizeChangeValue(TMP_Dropdown dropdown)
    {
        // Get the selected option's text and value
        string selectedValue = dropdown.options[dropdown.value].text;

        // Try to parse the selected value to an integer
        if (int.TryParse(selectedValue, out int intValue))
        {
            value = intValue;
            PlayerPrefs.SetInt("array_size", value);
        }
        else
        {
            type = selectedValue;
            PlayerPrefs.SetString("array_type", type);
        }
    }

}

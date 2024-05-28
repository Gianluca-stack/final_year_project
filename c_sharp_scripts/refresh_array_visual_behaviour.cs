using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using UnityEngine.UI;
using System;

public class refresh_array_visual_behaviour : MonoBehaviour
{

    // --- TMP Components ---
    public TextMeshProUGUI data;

    public void OnRefreshButtonClick()
    {
        string arrayType = PlayerPrefs.GetString("array_type");
        if (arrayType == "String")
        {
            // Retrieve and deserialize the string array
            string jsonString = PlayerPrefs.GetString("myStringArray");
            StringArrayWrapper wrapper = JsonUtility.FromJson<StringArrayWrapper>(jsonString);

            foreach (string element in wrapper.array)
            {
                Debug.Log(element);
            }
            if (wrapper != null && wrapper.array != null)
            {
                // Display the array data
                string arrayData = "";
                foreach (string element in wrapper.array)
                {
                    arrayData += element + "\n";
                }
                data.text = arrayData;
            }
            else
            {
                // Handle the case where the array or wrapper is null
                data.text = "String array is null or empty.";
            }
        }
        else if (arrayType == "Integer")
        {
            // Retrieve and deserialize the integer array
            string jsonString = PlayerPrefs.GetString("myIntArray");
            IntArrayWrapper wrapper = JsonUtility.FromJson<IntArrayWrapper>(jsonString);
            foreach (int element in wrapper.array)
            {
                Debug.Log(element);
            }
            if (wrapper != null && wrapper.array != null)
            {
                // Display the array data
                string arrayData = "";
                foreach (int element in wrapper.array)
                {
                    arrayData += element + "\n";
                }
                data.text = arrayData;
            }
            else
            {
                // Handle the case where the array or wrapper is null
                data.text = "Integer array is null or empty.";
            }

        }
    }

    internal class IntArrayWrapper
    {
        public int[] array;
    }

    internal class StringArrayWrapper
    {
        public string[] array;
    }
}

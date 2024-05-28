using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class remove_behaviour : MonoBehaviour
{
    // --- TMP Components ---
    public TMP_Dropdown dropdown;

    public TextMeshProUGUI array_warning;

    // --- Canvas Components ---

    public Canvas warning_message_ui;

    // --- Calling Scripts ---

    Show_keyboard show_keyboard;

   [SerializeField]GameObject show_keyboard_obj;

    public void OnRemoveElementButtonClick()
    {
        show_keyboard = show_keyboard_obj.GetComponent<Show_keyboard>();
        // get the value of the dropdown
        int value = dropdown.value;
        // remove the element at the index
        RemoveElement(value);

    } 

    public void RemoveElement(int index)
    {
        // set selected drodown value to first option
        dropdown.value = 0;
        string arrayType = PlayerPrefs.GetString("array_type");
        if (arrayType == "String")
        {
            //// Retrieve and deserialize the string array
            //string jsonString = PlayerPrefs.GetString("myStringArray");
            //StringArrayWrapper wrapper = JsonUtility.FromJson<StringArrayWrapper>(jsonString);
            //// Remove the element at the index
            //List<string> list = new(wrapper.array);
            //list.RemoveAt(index);
            //wrapper.array = list.ToArray();

            //// Serialize the updated array and save it back to PlayerPrefs
            //string newJsonString = JsonUtility.ToJson(wrapper);
            //PlayerPrefs.SetString("myStringArray", newJsonString);
            //PlayerPrefs.Save();

            // delete the element at the index by getting myStringArray from show_keyboard
            string[] array = show_keyboard.myStringArray;
            List<string> list = new(array);
            list.RemoveAt(index);
            show_keyboard.myStringArray = list.ToArray();

            // print array
            printarray(show_keyboard.myStringArray);

            // Update the text values in the array visualization
            UpdateArrayData(index, "");
        }
        else if (arrayType == "Integer")
        {
            // Retrieve and deserialize the integer array
            //string jsonString = PlayerPrefs.GetString("myIntArray");
            //IntArrayWrapper wrapper = JsonUtility.FromJson<IntArrayWrapper>(jsonString);
            //// Remove the element at the index
            //List<int> list = new(wrapper.array);
            //list.RemoveAt(index);
            //wrapper.array = list.ToArray();
            //// Serialize the updated array and save it back to PlayerPrefs
            //string newJsonString = JsonUtility.ToJson(wrapper);
            //PlayerPrefs.SetString("myIntArray", newJsonString);
            //PlayerPrefs.Save();

            // delete the value only from the index not the index itself by getting myIntArray from show_keyboard
            int[] array = show_keyboard.myIntArray;
            List<int> list = new(array);
            list.RemoveAt(index);
            show_keyboard.myIntArray = list.ToArray();

            // print array 
            printarray(show_keyboard.myIntArray);
            // Update the text values in the array visualization
            UpdateArrayData(index, "");
        }

        show_keyboard.occupied--;
        show_keyboard.not_occupied++;
    }

    private void UpdateArrayData<T>(int index, T element)
    {

        switch (index)
        {
            case 0:
                // get Text which has tag "0"
                GameObject.FindWithTag("0").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            case 1:
                // get Text which has tag "1"
                GameObject.FindWithTag("1").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            case 2:
                // get Text which has tag "2"
                GameObject.FindWithTag("2").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            case 3:
                // get Text which has tag "3"
                GameObject.FindWithTag("3").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            case 4:
                // get Text which has tag "4"
                GameObject.FindWithTag("4").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            case 5:
                // get Text which has tag "5"
                GameObject.FindWithTag("5").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            case 6:
                // get Text which has tag "6"
                GameObject.FindWithTag("6").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            case 7:
                // get Text which has tag "7"
                GameObject.FindWithTag("7").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            case 8:
                // get Text which has tag "8"
                GameObject.FindWithTag("8").GetComponent<TextMeshProUGUI>().text = element.ToString();
                break;
            default:
                break;

        }
    }

    public void printarray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log(array[i]);
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

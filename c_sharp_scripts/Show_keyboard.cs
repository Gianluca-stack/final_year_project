using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using static UnityEngine.Rendering.DebugUI;

public class Show_keyboard : MonoBehaviour
{

    // --- TMP Components ---

    public TextMeshProUGUI array_size_availability;

    public TextMeshProUGUI array_warning;

    public TMP_InputField inputField;

    public TMP_Dropdown dropdown;

    // --- Canvas Components ---

    public Canvas warning_message_ui;

    // --- Variables ---

    private string available;

    public int occupied = 0;

    public int not_occupied;

    public int size;

    // --- Arrays ---
    public string[] myStringArray;
    public int[] myIntArray;

    public int[] indextracker;

    public string test = "test";

    // --- GameObjects ---
    public GameObject dropdown_index;


    // Start is called before the first frame update
    void Start()
    {
        size = PlayerPrefs.GetInt("array_size");
        not_occupied = size;
        // create a new string and integer array
        myStringArray = new string[size];
        myIntArray = new int[size];
        indextracker = new int[size];
        // make dropdown Text mesh pro not interactable
        dropdown.interactable = false;
        inputField = GetComponent<TMP_InputField>(); // get the input field component
        inputField.onSelect.AddListener(x => OpenKeyboard()); // add a listener to the input field, when the input field is selected, open the keyboard
    }

    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
        SetCaretColorAlpha(1);
        //NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
        // when clicking on "Enter" on the keyboard, store the value in the array
        NonNativeKeyboard.Instance.OnTextSubmitted += (sender, e) =>
        {
            SetCaretColorAlpha(0);
            // get the value of the input field
            string value = inputField.text;
            if(PlayerPrefs.GetString("action") == "add")
            {

                if (occupied != size)
                {
                    if (PlayerPrefs.GetString("array_type") == "String")
                    {
                        int emptyIndex = FindEmptyIndex(myStringArray);
                        if (emptyIndex != -1)
                        {
                            // store the value in the array
                            myStringArray[emptyIndex] = value;

                        }
                        else
                        {
                            // if the index is -1, get the index of -1 in the indextracker array
                            for (int i = 0; i < indextracker.Length; i++)
                            {
                                if (indextracker[i] == -1)
                                {
                                    emptyIndex = i;
                                    // store the value in the array
                                    myStringArray[emptyIndex] = value;
                                }
                            }
                        }
                        // update not_occupied and occupied
                        occupied++;
                        not_occupied--;
                        // Update array data based on the index
                        UpdateArrayData(emptyIndex, value);

                    }
                    else if (PlayerPrefs.GetString("array_type") == "Integer")
                    {
                        Debug.Log("Adding Integer Element");
                        int emptyIndex = FindEmptyIndex(myIntArray);
                        if (emptyIndex != -1)
                        {
                            // store the value in the array
                            myIntArray[emptyIndex] = int.Parse(value);
                        }
                        else
                        {
                            Debug.Log("-------------------------");
                            Debug.Log("Index Tracker Array");
                            // loop through the indextracker array and print the indextracker array
                            for (int i = 0; i < indextracker.Length; i++)
                            {
                                Debug.Log("Index Tracker : " + indextracker[i]);
                            }

                            Debug.Log("-------------------------");
                            // if the index is -1, get the index of -1 in the indextracker array
                            for (int i = 0; i < indextracker.Length; i++)
                            {
                                if (indextracker[i] == -1)
                                {
                                    emptyIndex = i;
                                    // store the value in the array
                                    myIntArray[emptyIndex] = int.Parse(value);
                                }
                            }

                        }
                        // update not_occupied and occupied
                        occupied++;
                        not_occupied--;
                        Debug.Log("Empty Index : " + emptyIndex);
                        // Update array data based on the index
                        UpdateArrayData(emptyIndex, value);
                    }
                    else
                    {
                        warning_message_ui.gameObject.SetActive(true);
                        array_warning.text = "Inputed value with the wrong data type format!";
                    }
                    //PlayerPrefs.SetInt("occupied", PlayerPrefs.GetInt("occupied") + 1);

                }
                else
                {
                    warning_message_ui.gameObject.SetActive(true);
                    array_warning.text = "Array is full!";

                    // close the keyboard
                    NonNativeKeyboard.Instance.CloseKeyboard();

                    // print the array based on the type
                    if (PlayerPrefs.GetString("array_type") == "String")
                    {
                        Debug.Log(myStringArray);
                        printarray(myStringArray);
                    }
                    else if (PlayerPrefs.GetString("array_type") == "Integer")
                    {
                        Debug.Log(myIntArray);
                        printarray(myIntArray);
                    }


                }
                inputField.text = "";
            }else if(PlayerPrefs.GetString("action") == "update")
            {
                // on value changed in the dropdown, get the value of the dropdown
                Debug.Log("Listening...");
                // get the value of the dropdown
                int index = dropdown.value;

                // update the element in the array
                UpdateElement(value, index);
            }
            else
            {
                warning_message_ui.gameObject.SetActive(true);
                array_warning.text = "Action not found!";
            }   

        };
    }

    public void UpdateElement(string val, int index)
    {
        Debug.Log("Dropdown Value : " + index);
        Debug.Log("Updating Element");
        if (PlayerPrefs.GetString("array_type") == "String")
        {
            myStringArray[index] = val;
            UpdateArrayData(index, val);
        }
        else if (PlayerPrefs.GetString("array_type") == "Integer")
        {
            myIntArray[index] = int.Parse(val);
            UpdateArrayData(index, val);
        }
        inputField.text = "";
        // set dropdown value to 0
        dropdown.value = 0;

        // close the keyboard
        NonNativeKeyboard.Instance.CloseKeyboard();
    }

    public void SetCaretColorAlpha(float value)
    {
        inputField.customCaretColor = true;
        Color caretColor = inputField.caretColor;
        caretColor.a = value;

        inputField.caretColor = caretColor;
    }

    public void Update()
    {
        available = "Available Indexes: " + not_occupied + " | " + "Occupied Indexes: " + occupied;
        array_size_availability.text = available;
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
        // print the whole array including the empty indexes
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

    private int FindEmptyIndex<T>(T[] array) where T : IEquatable<T>
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log(array[i]);
            // Check if the element at index i is equal to the default value of type T
            if (EqualityComparer<T>.Default.Equals(array[i], default))
            {
                // store the index
                indextracker[i] = i;
                return i;
            }
            else
            {
                indextracker[i] = -1;
            }
        }
        return -1;
    }
}


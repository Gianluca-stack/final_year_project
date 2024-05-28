using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class update_element : MonoBehaviour
{
    private TMP_InputField update_input_field;

    public TMP_Dropdown dropdown;
    // --- Calling Scripts ---

    private Show_keyboard show_keyboard;

    [SerializeField] private GameObject show_keyboard_obj;
    // Start is called before the first frame update
    void Start()
    {
        show_keyboard = show_keyboard_obj.GetComponent<Show_keyboard>();
        update_input_field = GetComponent<TMP_InputField>(); // get the input field component
        update_input_field.onSelect.AddListener(y => OpenKeyboard()); // add a listener to the input field, when the input field is selected, open the keyboard
    }

    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = update_input_field;
        NonNativeKeyboard.Instance.PresentKeyboard(update_input_field.text);
        SetCaretColorAlpha(1);

        Debug.Log("Opening keyboard for Update Element script.");

        // set the keyboard to the input field text
        NonNativeKeyboard.Instance.OnTextSubmitted += (sender, e) =>
        {
            // update the element with the new text from the keyboard at the index
            int index = dropdown.value;
            if(PlayerPrefs.GetString("array_type") == "String")
            {
                string[] array = show_keyboard.myStringArray;
                array[index] = update_input_field.text;
                show_keyboard.myStringArray = array;

                // update the text in the array
                UpdateArrayData(index, update_input_field.text);
            }
            else if(PlayerPrefs.GetString("array_type") == "Integer")
            {
                int[] array = show_keyboard.myIntArray;
                array[index] = int.Parse(update_input_field.text);
                show_keyboard.myIntArray = array;

                // update the text in the array
                UpdateArrayData(index, update_input_field.text);
            }
        };
    }

    public void SetCaretColorAlpha(float value)
    {
        update_input_field.customCaretColor = true;
        Color caretColor = update_input_field.caretColor;
        caretColor.a = value;

        update_input_field.caretColor = caretColor;
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
}
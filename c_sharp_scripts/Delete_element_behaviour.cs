using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Delete_element_behaviour : MonoBehaviour
{
    // --- GameObjects ---

    public GameObject remove_element;

    // --- Canvas Components ---

    public Canvas warning_canvas;

    // --- TMP Components ---

    public TextMeshProUGUI array_warning;

    public TMP_Dropdown dropdown;

    // --- Buttons ---

    public Button[] action_buttons;

    public Button ui_close_btn;

    // --- Calling Scripts ---

    Show_keyboard show_keyboard;

    [SerializeField] GameObject show_keyboard_obj;

    public void Start()
    {
        show_keyboard = show_keyboard_obj.GetComponent<Show_keyboard>();
    }

    public void OnRemoveButtonClick()
    {
        // insert the options into the dropdown once 
        if (dropdown.options.Count == 0)
        {
            InsertOptions(show_keyboard.occupied);
        }
        remove_element.SetActive(true);
        PlayerPrefs.SetString("action", "remove");

        // for each button in the action_buttons array set the button to not interactable
        foreach (Button button in action_buttons)
        {
            button.interactable = false;
        }

        // set ui_close_btn to active
        ui_close_btn.gameObject.SetActive(true);
    }

    public void InsertOptions(int size)
    {
        // insert the options into the dropdown
        for (int i = 0; i < size; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = i.ToString() });
        }
    }
}

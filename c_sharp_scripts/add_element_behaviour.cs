using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using UnityEngine.UI;

public class add_element_behaviour : MonoBehaviour
{
    public GameObject keyboard;
    public Button[] action_buttons;
    public Button ui_close_btn;

    public TextMeshProUGUI[] array_descriptions;
    public TextMeshProUGUI[] add_descriptions;

    public void OnAddButtonClick()
    {
        keyboard.SetActive(true);
        // for each button in the action_buttons array set the button to not interactable
        foreach (Button button in action_buttons)
        {
            button.interactable = false;
        }

        // set ui_close_btn to active
        ui_close_btn.gameObject.SetActive(true);

        // set the array_descriptions to inactive
        foreach (TextMeshProUGUI description in array_descriptions)
        {
            description.gameObject.SetActive(false);
        }

        // set the add_descriptions to active
        foreach (TextMeshProUGUI description in add_descriptions)
        {
            description.gameObject.SetActive(true);
        }


        //// check if not_occupied doesnt exist in the player prefs create it
        //if (!PlayerPrefs.HasKey("not_occupied"))
        //{
        //    PlayerPrefs.SetInt("not_occupied", PlayerPrefs.GetInt("array_size"));
        //}

        //if(!PlayerPrefs.HasKey("occupied"))
        //{
        //    PlayerPrefs.SetInt("occupied", 0);
        //}
        PlayerPrefs.SetString("action", "add");

    }
}

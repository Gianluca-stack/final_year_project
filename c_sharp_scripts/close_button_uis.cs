using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;

public class close_button_uis : MonoBehaviour
{
    public Button[] action_buttons;
    public GameObject[] action_uis;

    public Button ui_close_btn;

    public TextMeshProUGUI[] add_descriptions;
    public TextMeshProUGUI[] array_descriptions;

    public void OnCloseButtonClick()
    {
        if (PlayerPrefs.GetString("action") == "add")
        {
            action_uis[0].SetActive(false);
            NonNativeKeyboard.Instance.CloseKeyboard();
            PlayerPrefs.SetString("action", "no_action");
            // set ui_close_btn to not active
            ui_close_btn.gameObject.SetActive(false);


            foreach (TextMeshProUGUI description in add_descriptions)
            {
                description.gameObject.SetActive(false);
            }

        }
        else if(PlayerPrefs.GetString("action") == "remove")
        {
            action_uis[1].SetActive(false);
            PlayerPrefs.SetString("action", "no_action");
            // set ui_close_btn to not active
            ui_close_btn.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetString("action") == "update")
        {
            action_uis[0].SetActive(false);
            //NonNativeKeyboard.Instance.CloseKeyboard();
            PlayerPrefs.SetString("action", "no_action");
            // set ui_close_btn to not active
            ui_close_btn.gameObject.SetActive(false);
        }
        // set array_descriptions to active
        foreach (TextMeshProUGUI description in array_descriptions)
        {
            description.gameObject.SetActive(true);

        }
        // for each button in the action_buttons array set the button to interactable
        foreach (Button button in action_buttons)
        {
            button.interactable = true;
        }
    }
}

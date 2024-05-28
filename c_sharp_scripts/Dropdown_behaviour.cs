using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class Dropdown_behaviour : MonoBehaviour
{
    public GameObject array_config;

    private GameObject stack_config;

    private GameObject trees_config;

    public GameObject dropdownmenu_ui;

    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        // set dropdown to uninteractable
        dropdown.interactable = false;
    }

    public void DropdownValueChanged(int result)
    {
        // if dropdown value is 0
        if(result == 1)
        {
            array_config.SetActive(true);
            dropdownmenu_ui.SetActive(false);
        }
        else if (result == 2)
        {
            stack_config.SetActive(true);
            dropdownmenu_ui.SetActive(false);
        }else if (result == 3)
        {
            trees_config.SetActive(true);
            dropdownmenu_ui.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the object is the controller
        if (other.gameObject.CompareTag("player_vr"))
        {
            // set dropdown to interactable
            dropdown.interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if the object is the controller
        if (other.gameObject.CompareTag("player_vr"))
        {
            // set dropdown to uninteractable
            dropdown.interactable = false;
        }
    }
}


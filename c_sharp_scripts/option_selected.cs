using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class option_selected : MonoBehaviour
{
    // variable to store an array of buttons
    public GameObject button;
    public TextMeshProUGUI difficulty_level_text;

    [SerializeField] string current_level;

    // Start is called before the first frame update
    void Start()
    {
        current_level = "Easy";
        PlayerPrefs.SetString("current_level", current_level);
        // get button by tag name
        GameObject button_easy = GameObject.FindGameObjectWithTag("easy");
        GameObject button_medium = GameObject.FindGameObjectWithTag("medium");
        GameObject button_hard = GameObject.FindGameObjectWithTag("hard");
        // if the current level is easy make the easy button uninteractable
        button_easy.GetComponent<UnityEngine.UI.Button>().interactable = false;
        button_medium.GetComponent<UnityEngine.UI.Button>().interactable = true;
        button_hard.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public void OnButtonClick(){

        GameObject button_easy = GameObject.FindGameObjectWithTag("easy");
        GameObject button_medium = GameObject.FindGameObjectWithTag("medium");
        GameObject button_hard = GameObject.FindGameObjectWithTag("hard");
        // get the tag name of the button
        string tag_name = button.tag;
        // store difficulty_level_text in a variable
        current_level = difficulty_level_text.text;

        // if current level is already easy make the easy button uninteractable


        // check if the tag name is equal to the following
        if(tag_name == "easy" && current_level != "Easy"){
            difficulty_level_text.text = "Easy";
            current_level = "Easy";
            button_easy.GetComponent<UnityEngine.UI.Button>().interactable = false;
            button_medium.GetComponent<UnityEngine.UI.Button>().interactable = true;
            button_hard.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else if(tag_name == "medium" && current_level != "Medium"){
            difficulty_level_text.text = "Medium";
            current_level = "Medium";
            button_medium.GetComponent<UnityEngine.UI.Button>().interactable = false;
            button_easy.GetComponent<UnityEngine.UI.Button>().interactable = true;
            button_hard.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else if(tag_name == "hard" && current_level != "Hard"){
            difficulty_level_text.text = "Hard";
            current_level = "Hard";
            button_hard.GetComponent<UnityEngine.UI.Button>().interactable = false;
            button_easy.GetComponent<UnityEngine.UI.Button>().interactable = true;
            button_medium.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        // store the current level in the player prefs
        PlayerPrefs.SetString("current_level", current_level);
        
    }
}

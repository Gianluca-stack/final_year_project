using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Script : MonoBehaviour
{
    public GameObject myUI;


    // Start is called before the first frame update
    void Start()
    {
        // hide the panel at the start
        myUI.SetActive(false);

    }

    private void OnTriggerEnter(Collider other) {
        // check if the player has entered the trigger
        if (other.gameObject.CompareTag("player_vr"))
        {
            
            // show the panel
            myUI.SetActive(true);
            // fade the panel
        }
    }

    private void OnTriggerExit(Collider other) {
        // check if the player has exited the trigger
        if (other.gameObject.CompareTag("player_vr"))
        {
            // hide the panel
            myUI.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene_two_menu_behaviour : MonoBehaviour
{
    public GameObject menu_canvas;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player_vr"))
        {
            menu_canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player_vr"))
        {
            menu_canvas.SetActive(false);
        }
    }
}


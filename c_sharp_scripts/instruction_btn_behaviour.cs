using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instruction_btn_behaviour : MonoBehaviour
{
    public GameObject[] gameObjects_canvas;

    public void OnInstructionButtonClick()
    {
        gameObjects_canvas[0].SetActive(true);
        gameObjects_canvas[1].SetActive(false);
    }

    public void OnCloseInstructionsButtonClick()
    {
        gameObjects_canvas[0].SetActive(false);
        gameObjects_canvas[1].SetActive(true);
    }
}

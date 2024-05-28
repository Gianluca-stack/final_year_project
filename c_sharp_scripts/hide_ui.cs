using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hide_ui : MonoBehaviour
{
    public Canvas[] Canvas;
    public GameObject[] GameObjects;
    public Button[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        // set all canvas to false
        foreach (Canvas canvas in Canvas)
        {
            canvas.gameObject.SetActive(false);
        }

        // set all gameobjects to false
        foreach (GameObject gameobject in GameObjects)
        {
            gameobject.SetActive(false);
        }

        // set all buttons to false
        foreach (Button button in Buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}

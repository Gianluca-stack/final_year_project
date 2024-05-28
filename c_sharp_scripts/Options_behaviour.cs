using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_behaviour : MonoBehaviour
{
    public GameObject options_ui;
    public GameObject starting_ui;
    // Start is called before the first frame update
    void Start()
    {
        options_ui.SetActive(false);
    }

    public void OnButtonClick(){
        options_ui.SetActive(true);
        starting_ui.SetActive(false);
    }

    public void OnCloseButtonClick(){
        options_ui.SetActive(false);
        starting_ui.SetActive(true);
    }
}

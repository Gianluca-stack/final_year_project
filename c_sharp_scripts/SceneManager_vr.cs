using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_vr : MonoBehaviour
{

    public string scene_name;

    // once clicking the button, the scene will be loaded
    public void LoadScene()
    {
        SceneManager.LoadScene(scene_name);
    }

    // once clicking the button, the application will be closed
    // public void QuitApp()
    // {
    //     Application.Quit();
    // }
}

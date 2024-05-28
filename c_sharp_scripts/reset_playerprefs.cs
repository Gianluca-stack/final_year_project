using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_playerprefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // reset all playerprefs
        PlayerPrefs.DeleteAll();
    }
}

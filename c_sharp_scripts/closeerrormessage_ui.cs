using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class closeerrormessage_ui : MonoBehaviour
{

    public TextMeshProUGUI array_warning;

    public Canvas warning_message_ui;

    public void OnCloseErrorMessageButtonClick()
    {
        warning_message_ui.gameObject.SetActive(false);
        array_warning.text = "";
    }
}

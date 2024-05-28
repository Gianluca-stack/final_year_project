using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Topic_Behaviour : MonoBehaviour
{
    public GameObject nextTopicUIObject;
    public GameObject previousTopicUIObject;

    public GameObject currentTopicUIObject;
    // Start is called before the first frame update

    public GameObject lastTopicUIObject;
    private string currentObjectName;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display_next_topic_ui()
    {

        // get currentObject name
        if(nextTopicUIObject.activeSelf == false && currentTopicUIObject.activeSelf == true)
        {
            nextTopicUIObject.SetActive(true);
            currentTopicUIObject.SetActive(false);

        }
    }

    public void Display_previous_topic_ui()
    {


        if(previousTopicUIObject.activeSelf == false && currentTopicUIObject.activeSelf == true)
        {
            previousTopicUIObject.SetActive(true);
            currentTopicUIObject.SetActive(false);
        }
    }
}

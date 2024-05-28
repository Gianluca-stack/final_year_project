using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setbutton_behaviour : MonoBehaviour
{
    public GameObject[] gameObjects;

    public Canvas array_visualization;

    public GameObject panelPrefab;

    public GameObject panelParent;

    public TextMeshProUGUI[] descriptions_about_arrays;

    // once user click set button
    public void OnSetButtonClick()
    {
        // set first gameobject to active
        gameObjects[0].SetActive(false);
        // set second gameobject to active
        gameObjects[1].SetActive(true);
        // set Canvas to active
        array_visualization.gameObject.SetActive(true);

        // set the descriptions_about_arrays to inactive
        foreach (TextMeshProUGUI description in descriptions_about_arrays)
        {
            description.gameObject.SetActive(false);
        }
        DisplayArray();
    }

    public void DisplayArray()
    {
        int PanelCount = PlayerPrefs.GetInt("array_size");

        RectTransform parentRect = panelParent.GetComponent<RectTransform>();
        float parentWidth = parentRect.rect.width;

        // Get the width of a single panel
        RectTransform panelRect = panelPrefab.GetComponent<RectTransform>();
        float panelWidth = panelRect.rect.width;

        // Calculate total width of all panels
        float totalWidth = PanelCount * (panelWidth + 10); // Add some spacing between panels, adjust as needed

        // Calculate starting X position to center panels
        float startX = -(totalWidth / 2) + (panelWidth / 2);

        for (int i = 0; i < PanelCount; i++)
        {
            GameObject panel = Instantiate(panelPrefab, panelParent.transform);

            // Calculate the position of the panel
            float panelPosX = startX + i * (panelWidth + 10); // Add some spacing between panels, adjust as needed

            // Set the position of the panel
            panel.transform.localPosition = new Vector3(panelPosX, 0, 0);

            // Set each panel to active and a different color
            panel.SetActive(true);
            // set the color to rgb(129, 133, 137)
            panel.GetComponent<UnityEngine.UI.Image>().color = new Color(129, 133, 137);

            // Add TextMeshProUGUI component as a child of the panel
            GameObject textObject = new GameObject("Text");
            textObject.transform.SetParent(panel.transform, false);
            TextMeshProUGUI textMesh = textObject.AddComponent<TextMeshProUGUI>();
            textMesh.rectTransform.sizeDelta = new Vector2(panelWidth, 30);
            textMesh.alignment = TextAlignmentOptions.Center;
            textMesh.fontSize = 10;
            // font color white
            textMesh.color = Color.black;
            // rotate text
            textMesh.transform.Rotate(0, 180, 0);

            // set panel to "Panel" tag
            panel.tag = "Panel";
        }

        // Set panel tags to i
        int temp_i = 0;
        GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");
        // start from the last element in the array
        for (int i = PanelCount - 1; i >= 0; i--)
        {
            TextMeshProUGUI textMesh = panels[temp_i].GetComponentInChildren<TextMeshProUGUI>();
            textMesh.gameObject.tag = i.ToString();
            temp_i++;
        }
    }
}

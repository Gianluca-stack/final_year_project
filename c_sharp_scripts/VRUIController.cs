using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIController : MonoBehaviour
{
    // Array of game objects
    // public GameObject[] myUI;

    public Canvas canvas;
    public Canvas info_ui;

    // public Collider other;

    tree_behaviour treebehaviour;

    [SerializeField] Terrain terrain;

    // private Vector3 closest_tree_position;

    // private readonly float uiHeight = 5f;

    // private readonly float x_axis = 1f;

    // private readonly float z_axis = 2f;
    // Start is called before the first frame update
    
    void Awake() {
        // hide the panel at the start
        canvas.gameObject.SetActive(true);
        treebehaviour = terrain.GetComponent<tree_behaviour>();

        // make canvas unclickable
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        // make canvas clickable
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void OnTriggerExit(Collider other)
    {
        // make canvas unclickable
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        canvas.gameObject.SetActive(true);
        info_ui.gameObject.SetActive(false);
    }
}

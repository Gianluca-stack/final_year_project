using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonFollow : MonoBehaviour
{

    public Transform visualtarget; // The target that the button will follow

    private Vector3 offset; // The offset between the button and the target
    private Transform pokeAttachTransform; // The transform of the XRPokeInteractor
    public XRBaseInteractable interactable;
    private bool isFollowing = false;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>(); // Get the XRBaseInteractable component
        interactable.hoverEntered.AddListener(Follow); // Add the Follow method to the hoverEntered event
    }

    public void Follow(BaseInteractionEventArgs hover){
        if(hover.interactableObject is XRPokeInteractor){ // Check if the interactableObject is an XRPokeInteractor
            XRPokeInteractor pokeInteractor = (XRPokeInteractor)hover.interactableObject; // Cast the interactableObject to XRPokeInteractor
            isFollowing = true; // Set isFollowing to true
            pokeAttachTransform = pokeInteractor.attachTransform; // Set the pokeAttachTransform to the attachTransform of the XRPokeInteractor
            offset = visualtarget.position - pokeAttachTransform.position; // Calculate the offset between the button and the target
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing){ // Check if the button is following the target
            visualtarget.position = pokeAttachTransform.position + offset;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class crate_behaviour : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody objectRigidbody;

    public GameObject objectToGrab;

    public BoxCollider boxCollider;

    public BoxCollider create_boxcollider;



    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        // get the object to grab Rigidbody component

        if (objectToGrab != null)
        {

            if (!objectToGrab.TryGetComponent<Rigidbody>(out objectRigidbody))
            {
                Debug.LogError("The object to grab does not have a Rigidbody component");
            }
        }
        else
        {
            Debug.LogError("objectToGrab is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            Debug.Log("Ball entered the crate");
            Debug.Log("Box Collider: " + boxCollider.enabled);
            // set boxCollider to false
            boxCollider.enabled = false;
            create_boxcollider.enabled = false;
            // set objectToGrab gravity to true
            objectRigidbody.useGravity = false;
        }
    }
}

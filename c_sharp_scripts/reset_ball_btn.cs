using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_ball_btn : MonoBehaviour
{
    public GameObject objectToGrab;

    // set variables to store the initial position and rotation of the object to grab
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // set variable for SphereCollider component
    private SphereCollider sphereCollider;
    private Rigidbody objectRigidbody;

    public PhysicMaterial physicMaterial;

    private void Awake()
    {
        // store ObjecttoGrab's initial position and rotation in a Vector3 and Quaternion respectively 
        initialPosition = objectToGrab.transform.position;
        initialRotation = objectToGrab.transform.rotation;

        // get objectToGrab's SphereCollider component
        
        // check if the object to grab has a SphereCollider component
        if (!objectToGrab.TryGetComponent<SphereCollider>(out sphereCollider))
        {
            Debug.LogError("The object to grab does not have a SphereCollider component");
        }

        // get the object to grab Rigidbody component
        if (!objectToGrab.TryGetComponent<Rigidbody>(out objectRigidbody))
        {
            Debug.LogError("The object to grab does not have a Rigidbody component");
        }

    }

    public void OnResetBallButtonClick()
    {
        // reset the object to grab's position and rotation to the initial position and rotation
        objectToGrab.transform.SetPositionAndRotation(initialPosition, initialRotation);

        // get SphereCollider material and set it to null
        sphereCollider.material = null;

        // enable the object to grab's SphereCollider component after 3 seconds
        StartCoroutine(EnableSphereCollider());
    }

    private IEnumerator EnableSphereCollider()
    {
        yield return new WaitForSeconds(3f);

        // set the object to grab's SphereCollider component material to the physicMaterial
        sphereCollider.material = physicMaterial;
    }

}

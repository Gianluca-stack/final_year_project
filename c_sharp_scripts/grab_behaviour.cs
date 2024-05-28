using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class grab_behaviour : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody objectRigidbody;

    public GameObject objectToGrab;

    public TextMeshProUGUI[] textMeshProUGUI;

    public GameObject panel;

    public BoxCollider table_collider;

    public BoxCollider[] plane_colliders;

    // set variables to store the initial position and rotation of the object to grab
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public PhysicMaterial physicMaterial;

    private int count = 0;

    void Awake()
    {

        grabInteractable = GetComponent<XRGrabInteractable>();
        // get the object to grab Rigidbody component

        if (objectToGrab != null)
        {
            
            if(!objectToGrab.TryGetComponent<Rigidbody>(out objectRigidbody))
            {
                Debug.LogError("The object to grab does not have a Rigidbody component");
            }
        }
        else
        {
            Debug.LogError("objectToGrab is not assigned.");
        }

        // store ObjecttoGrab's initial position and rotation in a Vector3 and Quaternion respectively 
        initialPosition = objectToGrab.transform.position;
        initialRotation = objectToGrab.transform.rotation;
    }

    [System.Obsolete]
    void OnEnable()
    {
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);

    }

    [System.Obsolete]
    void OnDisable()
    {
        grabInteractable.onSelectEntered.RemoveListener(OnGrab);
        grabInteractable.onSelectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        Debug.Log("Grabbed");
        // if textmeshproUGUI is already enabled, disable it
        if (textMeshProUGUI[0].enabled)
        {
            textMeshProUGUI[0].enabled = false;
        }
        //if panel is disabled, enable it
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        Debug.Log("Released");
        // gravity on
        objectRigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("table"))
        {
            Debug.Log("Table collision");

            count++;

            Debug.Log("Count: " + count);

            if (count == 1)
            {
                // disable table collider
                table_collider.enabled = false;
                // enbale each plane collider
                foreach (BoxCollider plane_collider in plane_colliders)
                {
                    plane_collider.enabled = true;
                }
                // set objectToGrab's SphereCollider component material to the physicMaterial
                objectToGrab.GetComponent<SphereCollider>().material = null;
                // wait for 5 seconds
                StartCoroutine(ResetBall());
                StartCoroutine(EnableSphereCollider());
                //StartCoroutine(EnableSphereCollider());
                textMeshProUGUI[1].text = "Now try adding more force on your bounce";
            }else if (count == 2)
            {
                // disable table collider
                table_collider.enabled = false;
                // enbale each plane collider
                foreach (BoxCollider plane_collider in plane_colliders)
                {
                    plane_collider.enabled = true;
                }
                // set objectToGrab's SphereCollider component material to the physicMaterial
                objectToGrab.GetComponent<SphereCollider>().material = null;
                // wait for 5 seconds
                StartCoroutine(ResetBall());
                StartCoroutine(EnableSphereCollider());
                textMeshProUGUI[1].text = "Notice that by increasing the amount of force on the ball it went higher and bounced a longer distance";

                // wait for 2 seconds
                StartCoroutine(DisplayText());
            }
        }
    }

    private IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(5f);



        // reset the object to grab's position and rotation to the initial position and rotation
        objectToGrab.transform.SetPositionAndRotation(initialPosition, initialRotation);

        // enable table collider
        table_collider.enabled = true;
        // disable each plane collider
        foreach (BoxCollider plane_collider in plane_colliders)
        {
            plane_collider.enabled = false;
        }
    }

    private IEnumerator EnableSphereCollider()
    {
        yield return new WaitForSeconds(0.5f);

        // set the object to grab's SphereCollider component material to the physicMaterial
        objectToGrab.GetComponent<SphereCollider>().material = physicMaterial;
    }

    private IEnumerator DisplayText()
    {
        // wait for 2 seconds
        yield return new WaitForSeconds(2f);

        textMeshProUGUI[1].text = "Great job! You have successfully completed the task, Isaac Newton is proud!";
        // set the object to grab's SphereCollider component material to the physicMaterial
        objectToGrab.GetComponent<SphereCollider>().material = physicMaterial;
    }
}


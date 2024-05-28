using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Opendoor : MonoBehaviour
{
    public Animator animator;
    public string  boolname = "Open";
    private void Start() {
        // get wall_inside tag 
        GameObject wall_inside = GameObject.FindGameObjectWithTag("wall_inside");
        // disable Box Collider
        wall_inside.GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // get wall_inside tag 
        GameObject wall_inside = GameObject.FindGameObjectWithTag("wall_inside");
        // get wall_outside tag
        GameObject wall_outside = GameObject.FindGameObjectWithTag("wall_outside");

        if (other != null && other.gameObject.CompareTag("wall_outside"))
        {
            OpenDoor();
            // disable Box Collider
            wall_outside.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Wall outside opened");
            // delay 15 seconds then enable Box Collider
            StartCoroutine(EnableInsideCollider());

        }else if (other != null && other.gameObject.CompareTag("wall_inside"))
        {
            OpenDoor();
            // enable Box Collider
            wall_inside.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Wall inside is disabled");
            StartCoroutine(EnableOutsideCollider());
        }
    }

    IEnumerator EnableInsideCollider()
    {
        yield return new WaitForSeconds(10);
        CloseDoor();
        // get wall_inside tag 
        GameObject wall_inside = GameObject.FindGameObjectWithTag("wall_inside");
        // enable Box Collider
        wall_inside.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator EnableOutsideCollider()
    {
        yield return new WaitForSeconds(10);

        CloseDoor();
        // get wall_outside tag
        GameObject wall_outside = GameObject.FindGameObjectWithTag("wall_outside");
        // enable Box Collider
        wall_outside.GetComponent<BoxCollider>().enabled = true;
    }

    public void OpenDoor()
    {
        bool isOpen = animator.GetBool(boolname); // get the current state of the door
        animator.SetBool(boolname, !isOpen); // set the opposite of the current state
    }

    public void CloseDoor()
    {
        bool isOpen = animator.GetBool(boolname); // get the current state of the door
        animator.SetBool(boolname, !isOpen); // set the opposite of the current state
    }

}

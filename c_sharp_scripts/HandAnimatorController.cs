using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction; // This is the trigger action
    [SerializeField] private InputActionProperty gripAction; // This is the grip action

    // poke action
    // [SerializeField] private InputActionProperty pokeAction; // This is the poke action

    private Animator animator;

    private void Start(){
        animator = GetComponent<Animator>(); // Get the animator component
    }
    
    private void Update(){
        float triggerValue = triggerAction.action.ReadValue<float>(); // 0 to 1, 0 is not pressed, 1 is fully pressed
        float gripValue = gripAction.action.ReadValue<float>(); // 0 to 1, 0 is not pressed, 1 is fully pressed





        animator.SetFloat("Trigger", triggerValue); // Set the trigger value to the animator
        animator.SetFloat("Grip", gripValue); // Set the grip value to the animator

    
    }
}

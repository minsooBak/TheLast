using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerInput _input;
    private IInteractable _interactObject;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _input.PlayerActions.Interaction.started += Interaction;
    }

    private void Interaction(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _interactObject?.Interaction();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _interactObject = other.GetComponent<IInteractable>();
            _interactObject?.InteractEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _interactObject?.InteractExit();
        _interactObject = null;
    }
}

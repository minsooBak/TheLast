using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
public class TestScript : MonoBehaviour
{
    private IInteractable _interactObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _interactObject?.Interaction();
        }
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

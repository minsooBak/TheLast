using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DungeonInfo : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshPro _text;

    public void InteractEnter()
    {
        _text.gameObject.SetActive(true);
    }

    public void InteractExit()
    {
        _text.gameObject.SetActive(false);
    }

    public void Interaction()
    {
        // TODO 던전Info UI창 띄우기
        Debug.Log("INFO");
    }
}

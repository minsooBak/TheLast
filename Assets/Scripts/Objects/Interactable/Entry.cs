using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entry : MonoBehaviour, IInteractable
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
        GameManager.ScenesManager.ChangeScene(Enums.SceneState.DungeonScene);
    }
}

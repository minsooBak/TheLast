using Enums;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharaterSelectScene : IBaseScene
{
    [SerializeField] private Button startButton;
    public override void Init()
    {
        //ScenesManager scenesManager = GameManager.ScenesManager;
        //AddListener(() => { scenesManager.ChangeScene(SceneState.CharaterSelectScene); });
    }

    //public void AddListener(UnityAction action)
    //{
    //    AddListener(action);
    //}
}

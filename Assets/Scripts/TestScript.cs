using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
public class TestScript : MonoBehaviour
{
    void Start()
    {
        if (GameManager.ScenesManager.CurrentState != SceneState.DungeonScene)
        {
            GameManager.ScenesManager.ChangeScene(SceneState.DungeonScene);
        }
    }
}

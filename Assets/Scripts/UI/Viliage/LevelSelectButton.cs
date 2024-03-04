using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private Button levelSelectButton;
    [SerializeField] private TextMeshProUGUI text;
    private int _level;
    public void Init(int level)
    {
        _level = level;
        levelSelectButton.onClick.AddListener(ChangeStage);
        text.text = $"Level {_level}";
    }

    private void ChangeStage()
    {
        DungeonManager.SelectedLevel = _level;
        GameManager.ScenesManager.ChangeScene(Enums.SceneState.DungeonScene);
    }
}

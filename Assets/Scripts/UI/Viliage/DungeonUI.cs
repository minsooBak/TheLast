using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUI : MonoBehaviour
{
    [SerializeField] private Transform root;
    private int _maxStage;
    void Start()
    {
        _maxStage = GameManager.PlayerManager.PlayerInfoManager.userData.stageLv;
        if(_maxStage == 0)
        {
            _maxStage = 1;
        }

        for (int i = 1; i <= _maxStage; i++)
        {
            LevelSelectButton button =
                GameManager.ResourceManager.Instantiate("Prefabs/UI/LevelButton",
                root).
                GetComponent<LevelSelectButton>();
            button.Init(i);
        }
    }
}

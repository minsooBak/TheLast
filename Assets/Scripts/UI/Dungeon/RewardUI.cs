using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _expText;
    [SerializeField] private Button _ExitButton;

    private void Start()
    {
        GameManager.DataBases.TryGetDataBase(out DungeonDataBase _dungeonDB);
        GameManager.DataBases.TryGetDataBase(out EnemyDataBase _enemyDB);

        DungeonData dungeonData = _dungeonDB.GetData(DungeonManager.SelectedLevel);

        _goldText.text = $"{dungeonData.Gold}";
        _expText.text = $"{dungeonData.Exp}";
        GameManager.PlayerManager.PlayerInfoManager.userData.gold += dungeonData.Gold;
        GameManager.PlayerManager.PlayerInfoManager.userData.exp += dungeonData.Exp;
        _ExitButton.onClick.AddListener(Exit);

    }

    private void Exit()
    {
        int nextLevel = DungeonManager.SelectedLevel + 1;

        int maxLevel = GameManager.PlayerManager.PlayerInfoManager.userData.stageLv;
        if (maxLevel < nextLevel)
            GameManager.PlayerManager.PlayerInfoManager.userData.stageLv = nextLevel;
        GameManager.ScenesManager.ChangeScene(Enums.SceneState.VillageScene);
    }
}

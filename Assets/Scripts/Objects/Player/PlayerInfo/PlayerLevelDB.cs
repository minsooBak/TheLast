using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelDB
{
    private Dictionary<int, PlayerLevelInfo> _level = new();
    private List<PlayerLevelInfo> _levelInfo;

    public PlayerLevelDB(byte _id)
    {
        var res = Resources.Load<PlayerBaseLevel>("Data/PlayerBaseLevel");
        var levelSo = Object.Instantiate(res);

        switch (_id)
        {
            case 1:
                _levelInfo = levelSo.MagicianLevel;
                break;
            case 2:
                _levelInfo = levelSo.OrcWarriorsLevel;
                break;
        }

        if (_levelInfo == null || _levelInfo.Count <= 0)
        {
            return;
        }
        var _levelInfoCount = _levelInfo.Count;

        for (int i = 0; i < _levelInfoCount; i++)
        {
            var level = _levelInfo[i];

            if (_level.ContainsKey(level._level))
            {
                _level[level._level] = level;
            }
            else
            {
                _level.Add(level._level, level);
            }
        }
    }
    public PlayerLevelInfo GetData(int level)
    {
        if (_level.ContainsKey(level))
        {
            return _level[level];
        }
        return null;
    }

    public int GetLevelCount()
    {
        return _levelInfo.Count;
    }
}

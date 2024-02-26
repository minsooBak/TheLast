using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelDB
{
    private Dictionary<int, PlayerLevelInfo> _level = new();

    public PlayerLevelDB()
    {
        var res = Resources.Load<PlayerBaseLevel>("Data/PlayerBaseLevel");
        var levelSo = Object.Instantiate(res);
        var enriries = levelSo.MagicianLevel;

        if (enriries == null || enriries.Count <= 0)
        {
            return;
        }
             var entityCount = enriries.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var level = enriries[i];

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
}

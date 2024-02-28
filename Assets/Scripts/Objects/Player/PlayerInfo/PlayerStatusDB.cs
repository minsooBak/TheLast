using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusDB
{
    private Dictionary<int, PlayerStatusInfo> _status = new();

    public PlayerStatusDB()
    {
        var res = Resources.Load<PlayerBaseStatus>("Data/PlayerBaseStatus");
        var statusSo = Object.Instantiate(res);
        var enriries = statusSo.PlayerStatuses;

        if (enriries == null || enriries.Count <= 0)
        {
            return;
        }
        
        var entityCount = enriries.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var status = enriries[i];

            if (_status.ContainsKey(status._id))
            {
                _status[status._id] = status;
            }
            else
            {
                _status.Add(status._id, status);
            }
        }
;    }
    public PlayerStatusInfo GetData(byte id)
    {
        if (_status.ContainsKey(id))
        {
            return _status[id];
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSlotManager : MonoBehaviour
{
    private SkillSlotUI[] _slots;
    [SerializeField] private PlayerSkillHandler _handler;
    private void Awake()
    {
        _slots = GetComponentsInChildren<SkillSlotUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _slots[0].Skill != null)
        {
            
            var id = _slots[0].Skill._id;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _slots[1].Skill != null)
        {
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && _slots[2].Skill != null)
        {
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && _slots[3].Skill != null)
        {
        }
    }
}

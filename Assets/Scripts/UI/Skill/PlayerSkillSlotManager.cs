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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
        }
    }
}

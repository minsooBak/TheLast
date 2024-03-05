using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerSkillHandler : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    private float interval = 0.25f;
    private float doubleClickedTime = -1.0f;
    private bool monsterTarget = false;

    Player player;
    private PlayerInput playerInput;


    private PlayerSkill skillInfo;
    private PlayerSkillDB skillDB;
    private PlayerInfo playerInfo;

    public Transform attackPoint;

    MagicianSkill magicianSkill;
    private SkillSlotUI[] skillSlotUIs;
    [SerializeField] private GameObject skillSlot;
    

    private byte slot1ID;
    private byte slot2ID;
    private byte slot3ID;
    private byte slot4ID;

    private float slot1Cooldown;
    private float slot2Cooldown;
    private float slot3Cooldown;
    private float slot4Cooldown;

    private void Awake()
    {
        playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
        skillInfo = GameManager.PlayerManager.SkillManager.PlayerSkill;
        skillDB = GameManager.PlayerManager.SkillManager.skillData;
        player = GetComponent<Player>();
        switch (GameManager.PlayerManager.PlayerInfoManager.userData.statusId)
        {
            case 1:
                magicianSkill = new MagicianSkill();
                break;
            case 2:

                break;
        }

        skillSlotUIs = skillSlot.GetComponentsInChildren<SkillSlotUI>();
        var slots = GameManager.PlayerManager.PlayerInfoManager.userData.playerData.skillSlots;
        int index = 0;
        foreach( var slot in slots)
        {
            skillSlotUIs[index++].SetSkill(slot.Skill, slot.Index);
        }
    }
    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            OnMouseClicked();
        }
        if (monsterTarget)
        {
            target = null;
            monsterTarget = false;
        }
        if (slot1Cooldown != 0)
        {
            slot1Cooldown = Mathf.Max((slot1Cooldown -= Time.deltaTime), 0);
        }
        if (slot2Cooldown != 0)
        {
            slot2Cooldown = Mathf.Max((slot2Cooldown -= Time.deltaTime), 0);
        }
        if (slot2Cooldown != 0)
        {
            slot4Cooldown = Mathf.Max((slot4Cooldown -= Time.deltaTime), 0);
        }
        if (slot2Cooldown != 0)
        {
            slot3Cooldown = Mathf.Max((slot3Cooldown -= Time.deltaTime), 0);
        }
    }
    private void OnMouseClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red);
        int _mask = LayerMask.GetMask("Enemy");
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, 100.0f, _mask))
        {
            target = _hit.transform.gameObject;
        }
        else
        {
            OnMouseDoubleClicked();
        }
    }
    private void OnMouseDoubleClicked()
    {
        if ((Time.time - doubleClickedTime) < interval)
        {
            monsterTarget = true;
            doubleClickedTime = -1.0f;
        }
        else
        {
            monsterTarget = false;
            doubleClickedTime = Time.time;
        }
    }
    public void SkillSolt1()
    {
        if (slot1Cooldown == 0 && skillSlotUIs[0].Skill != null)
        {
            slot1ID = skillSlotUIs[0].Skill._id;
            magicianSkill.SkillSelect(slot1ID, target, transform, attackPoint);
            slot1Cooldown = skillDB.GetData(slot1ID)._coolDown;
        }
    }
    public void SkillSolt2()
    {
        if (slot2Cooldown == 0 && skillSlotUIs[1].Skill != null)
        {
            slot2ID = skillSlotUIs[1].Skill._id;
            magicianSkill.SkillSelect(slot2ID, target, transform, attackPoint);
            slot2Cooldown = skillDB.GetData(slot2ID)._coolDown;
        }
    }
    public void SkillSolt3()
    {
        if (slot3Cooldown == 0 && skillSlotUIs[2].Skill != null)
        {
            slot3ID = skillSlotUIs[2].Skill._id;
            magicianSkill.SkillSelect(slot3ID, target, transform, attackPoint);
            slot3Cooldown = skillDB.GetData(slot3ID)._coolDown;
        }
    }
    public void SkillSolt4()
    {
        if (slot4Cooldown == 0 && skillSlotUIs[3].Skill != null)
        {
            slot4ID = skillSlotUIs[3].Skill._id;
            magicianSkill.SkillSelect(slot4ID, target, transform, attackPoint);
            slot4Cooldown = skillDB.GetData(slot4ID)._coolDown;
        }
    }

    private void OnDestroy()
    {
        GameManager.PlayerManager.PlayerInfoManager.userData.playerData.skillSlots = skillSlotUIs;
    }
}

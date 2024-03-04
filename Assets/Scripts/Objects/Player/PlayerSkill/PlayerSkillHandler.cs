using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
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

    private GameObject skillSlot;

    private PlayerSkill skillInfo;
    private PlayerSkillDB skillDB;
    private PlayerInfo playerInfo;

    public Transform attackPoint;

    MagicianSkill magicianSkill;

    private byte slot0ID;
    private byte slot1ID;
    private byte slot2ID;
    private byte slot3ID;

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
    }
    private void Start()
    {


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
        magicianSkill.SkillSelect(slot0ID, target, transform, attackPoint);
    }
    public void SkillSolt2()
    {
        magicianSkill.SkillSelect(slot1ID, target, transform, attackPoint);
    }
    public void SkillSolt3()
    {
        magicianSkill.SkillSelect(slot2ID, target, transform, attackPoint);
    }
    public void SkillSolt4()
    {
        magicianSkill.SkillSelect(slot3ID, target, transform, attackPoint);
    }
    public void SkillSoltChange(byte id,byte slotsNum)
    {
        switch (slotsNum)
        {
            case 0:
                slot0ID = id;
                break;
            case 1:
                slot1ID = id;
                break;
            case 2:
                slot2ID = id;
                break;
            case 3:
                slot3ID = id;
                break;
        }
    }
}

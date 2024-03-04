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

    private GameObject skillSlot;

    private PlayerSkill skillInfo;
    private PlayerSkillDB skillDB;
    private PlayerInfo playerInfo;

    public Transform attackPoint;

    MagicianSkill magicianSkill;

    private byte slot1ID;
    private byte slot2ID;
    private byte slot3ID;
    private byte slot4ID;

    private float slot1Cooldown = 10;
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
        Debug.Log(slot1ID);
        if (slot1Cooldown == 0)
        {
            magicianSkill.SkillSelect(slot1ID, target, transform, attackPoint);
            slot1Cooldown = skillDB.GetData(slot1ID)._coolDown;
        }
    }
    public void SkillSolt2()
    {
        Debug.Log(slot2ID);
        if (slot2Cooldown == 0)
        {
            magicianSkill.SkillSelect(slot2ID, target, transform, attackPoint);
            slot2Cooldown = skillDB.GetData(slot2ID)._coolDown;
        }
    }
    public void SkillSolt3()
    {
        Debug.Log(slot3ID);
        //if (slot3Cooldown == 0)
        //{
        //    magicianSkill.SkillSelect(slot3ID, target, transform, attackPoint);
        //    slot3Cooldown = skillDB.GetData(slot3ID)._coolDown;
        //}
        if (target != null)
        {
            if (playerInfo.Mp >= skillDB.GetData(106)._cost && skillInfo.playerSkillInfo[106] != 0)
            {
                playerInfo.Mp -= skillDB.GetData(106)._cost;
                GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = target.transform.position;
            }
        }
        else
        {
            if (playerInfo.Mp >= skillDB.GetData(106)._cost && skillInfo.playerSkillInfo[106] != 0)
            {
                playerInfo.Mp -= skillDB.GetData(106)._cost;
                GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = transform.position;
            }
        }
    }
    public void SkillSolt4()
    {
        Debug.Log(slot4ID);
        //if (slot4Cooldown == 0)
        //{
        //    magicianSkill.SkillSelect(slot4ID, target, transform, attackPoint);
        //    slot4Cooldown = skillDB.GetData(slot4ID)._coolDown;
        //}
        if (target != null)
        {
            GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = target.transform.position;
        }
        else
        {
            GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = transform.position;
        }
    }
    public void SkillSoltChange(byte id, byte slotsNum)
    {
        switch (slotsNum)
        {
            case 0:
                slot1ID = id;
                break;
            case 1:
                slot2ID = id;
                break;
            case 2:
                slot3ID = id;
                break;
            case 3:
                slot4ID = id;
                break;
        }
    }
}

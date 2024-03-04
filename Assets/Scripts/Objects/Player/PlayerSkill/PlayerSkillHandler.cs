using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    private int skillSlotCount;
    private Button[] skillButton;
    private GameObject skillSlot;

    private PlayerSkill skillInfo;
    private PlayerSkillDB skillDB;
    private PlayerInfo playerInfo;
    public Transform attackPoint;

    private void Awake()
    {
        playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
        skillInfo = GameManager.PlayerManager.SkillManager.PlayerSkill;
        skillDB = GameManager.PlayerManager.SkillManager.skillData;

        skillSlot = GameObject.Find("SkillSlot");
    }
    private void Start()
    {
        player = GetComponent<Player>();
        playerInput = player.Input;
        playerInput.PlayerActions.Attack.started += OnAttack;

        skillSlotCount = skillSlot.transform.childCount;

        skillButton = skillSlot.GetComponentsInChildren<Button>();

        for (int i = 0; i < skillButton.Length; i++)
        {

            int temp = i;
            skillButton[i].onClick.AddListener(() => SkillLevelUp(temp));
        }
    }
    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            OnMouseClicked();
        }
        if (target != null)
        {
            Debug.Log(target.transform.position);
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
            Debug.Log(target);
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
    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("SkillSolt1");
        string i = skillDB.GetData(101)._prefabPath;
        GameManager.ResourceManager.Instantiate(i, attackPoint);
    }
    public void SkillSolt2()
    {
        Debug.Log("SkillSolt2");
        GameObject instance = Instantiate(Resources.Load("", typeof(GameObject))) as GameObject;
    }
    public void SkillSolt3()
    {
        Debug.Log("SkillSolt3");
        GameObject instance = Instantiate(Resources.Load("", typeof(GameObject))) as GameObject;
    }
    public void SkillSolt4()
    {
        Debug.Log("SkillSolt4");
        GameObject instance = Instantiate(Resources.Load("", typeof(GameObject))) as GameObject;
    }

    public void SkillLevelUp(int num)
    {
        switch (num)
        {
            case 1:
                if (skillInfo.playerSkillInfo[101] > 3 
                    && playerInfo.SkillPoint <= skillDB.GetData(101)._skillPoint)
                {
                    skillInfo.playerSkillInfo[101] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(101)._skillPoint;
                }
                else
                {

                }
                break;
            case 2:
                if (skillInfo.playerSkillInfo[102] > 3 
                    && playerInfo.SkillPoint <= skillDB.GetData(102)._skillPoint
                    && skillInfo.playerSkillInfo[101] >= 1)
                {
                    skillInfo.playerSkillInfo[102] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(102)._skillPoint;
                }
                else
                {

                }
                break;
            case 3:
                if (skillInfo.playerSkillInfo[103] > 3
                    && playerInfo.SkillPoint <= skillDB.GetData(103)._skillPoint
                    && skillInfo.playerSkillInfo[101] >= 1)
                {
                    skillInfo.playerSkillInfo[103] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(103)._skillPoint;
                }
                else
                {

                }
                break;
            case 4:
                if (skillInfo.playerSkillInfo[104] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(104)._skillPoint
                && skillInfo.playerSkillInfo[101] >= 1)
                {
                    skillInfo.playerSkillInfo[104] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(104)._skillPoint;
                }
                else
                {

                }
                break;
            case 5:
                if (skillInfo.playerSkillInfo[105] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(105)._skillPoint
                && skillInfo.playerSkillInfo[104] >= 1)
                {
                    skillInfo.playerSkillInfo[105] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(105)._skillPoint;
                }
                else
                {

                }
                break;
            case 6:
                if (skillInfo.playerSkillInfo[106] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(106)._skillPoint
                && skillInfo.playerSkillInfo[102] >= 1)
                {
                    skillInfo.playerSkillInfo[106] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(106)._skillPoint;
                }
                else
                {

                }
                break;
            case 7:
                if (skillInfo.playerSkillInfo[107] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(107)._skillPoint
                && skillInfo.playerSkillInfo[103] >= 1)
                {
                    skillInfo.playerSkillInfo[107] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(107)._skillPoint;
                }
                else
                {

                }
                break;
            case 8:
                if (skillInfo.playerSkillInfo[108] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(108)._skillPoint
                && skillInfo.playerSkillInfo[105] >= 1 
                && skillInfo.playerSkillInfo[106] >= 1
                && skillInfo.playerSkillInfo[107] >= 1)
                {
                    skillInfo.playerSkillInfo[108] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(108)._skillPoint;
                }
                else
                {

                }
                break;
            case 9:
                if (skillInfo.playerSkillInfo[201] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(201)._skillPoint)
                {
                    skillInfo.playerSkillInfo[201] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(201)._skillPoint;
                }
                else
                {

                }
                break;
            case 10:
                if (skillInfo.playerSkillInfo[202] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(202)._skillPoint
                && skillInfo.playerSkillInfo[201] >= 1)
                {
                    skillInfo.playerSkillInfo[202] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(202)._skillPoint;
                }
                else
                {

                }
                break;
            case 11:
                if (skillInfo.playerSkillInfo[203] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(203)._skillPoint
                && skillInfo.playerSkillInfo[202] >= 1)
                {
                    skillInfo.playerSkillInfo[203] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(203)._skillPoint;
                }
                else
                {

                }
                break;
            case 12:
                if (skillInfo.playerSkillInfo[204] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(204)._skillPoint)
                {
                    skillInfo.playerSkillInfo[204] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(204)._skillPoint;
                }
                else
                {

                }
                break;
            case 13:
                if (skillInfo.playerSkillInfo[205] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(205)._skillPoint
                && skillInfo.playerSkillInfo[204] >= 1)
                {
                    skillInfo.playerSkillInfo[205] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(205)._skillPoint;
                }
                else
                {

                }
                break;
            case 14:
                if (skillInfo.playerSkillInfo[206] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(206)._skillPoint
                && skillInfo.playerSkillInfo[203] >= 1)
                {
                    skillInfo.playerSkillInfo[206] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(206)._skillPoint;
                }
                else
                {

                }
                break;
            case 15:
                if (skillInfo.playerSkillInfo[207] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(207)._skillPoint
                && skillInfo.playerSkillInfo[203] >= 1)
                {
                    skillInfo.playerSkillInfo[207] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(207)._skillPoint;
                }
                else
                {

                }
                break;
            case 16:
                if (skillInfo.playerSkillInfo[208] > 3
                && playerInfo.SkillPoint <= skillDB.GetData(208)._skillPoint
                && skillInfo.playerSkillInfo[205] >= 1)
                {
                    skillInfo.playerSkillInfo[208] += 1;
                    playerInfo.SkillPoint -= skillDB.GetData(208)._skillPoint;
                }
                else
                {

                }
                break;
        }
    }
}

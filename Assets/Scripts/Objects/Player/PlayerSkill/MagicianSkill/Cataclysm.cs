using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cataclysm : BaseSkill
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        Damage();
    }
    protected override void Damage()
    {
        switch (playerSkill.playerSkillInfo[105])
        {
            case 1:
                damage = (float)(skillManager.skillData.GetData(105)._Level1) * (playerInfo.StatInt);
                break;
            case 2:
                damage = (float)(skillManager.skillData.GetData(105)._Level2) * (playerInfo.StatInt);
                break;
            case 3:
                damage = (float)(skillManager.skillData.GetData(105)._Level3) * (playerInfo.StatInt);
                break;
        }
    }
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
            healthSystem.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    protected override void OnTriggerStay(Collider collision)
    {

    }
    protected override void OnTriggerExit(Collider collision)
    {

    }
}

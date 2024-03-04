using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyExplosion : BaseSkill
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
        switch (playerSkill.playerSkillInfo[104])
        {
            case 1:
                damage = (float)(skillManager.skillData.GetData(104)._Level1) * (playerInfo.StatInt);
                break;
            case 2:
                damage = (float)(skillManager.skillData.GetData(104)._Level2) * (playerInfo.StatInt);
                break;
            case 3:
                damage = (float)(skillManager.skillData.GetData(104)._Level3) * (playerInfo.StatInt);
                break;
        }
    }
    protected override void FixedUpdate()
    {

    }
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
        }
    }
    protected override void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            inTarget = true;
            Invoke("AddDamage", 2f);
        }
        else
        {
            inTarget = false;
        }
    }
    protected override void OnTriggerExit(Collider collision)
    {

    }
    protected void AddDamage()
    {
        if (inTarget)
        {
            healthSystem.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

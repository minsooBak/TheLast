using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMagic : BaseSkill
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
        switch (playerSkill.playerSkillInfo[108])
        {
            case 1:
                damage = (float)(skillManager.skillData.GetData(108)._Level1) * (playerInfo.StatInt);
                break;
            case 2:
                damage = (float)(skillManager.skillData.GetData(108)._Level2) * (playerInfo.StatInt);
                break;
            case 3:
                damage = (float)(skillManager.skillData.GetData(108)._Level3) * (playerInfo.StatInt);
                break;
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
        }
    }
    protected override void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            inTarget = true;
            Invoke("AddDamage",1);
        }
        else
        {
            inTarget = false;
        }
    }
    protected override void OnCollisionExit(Collision collision)
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

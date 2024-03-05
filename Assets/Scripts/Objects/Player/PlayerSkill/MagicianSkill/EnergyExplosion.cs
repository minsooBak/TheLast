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
        Invoke("SkillEnd", 1.6f);
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3.5f);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                healthSystem = collider.gameObject.GetComponent<CharacterHealthSystem>();
                Invoke("AddDamage", 1.2f);
            } 
        }
    }
    protected override void OnTriggerEnter(Collider collision)
    {

    }
    protected override void OnTriggerStay(Collider collision)
    {

    }
    protected override void OnTriggerExit(Collider collision)
    {

    }
    protected void AddDamage()
    {
        healthSystem.TakeDamage(damage);
    }
    protected override void SkillEnd()
    {
        Destroy(gameObject);
    }
}

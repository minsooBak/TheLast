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
    protected override void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 13f, 8);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                CharacterHealthSystem healthSystem = collider.GetComponent<CharacterHealthSystem>();
                Invoke("AddDamage", 1);
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
        Destroy(gameObject);
    }
}

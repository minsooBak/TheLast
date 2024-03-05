using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : BaseSkill
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
        switch (playerSkill.playerSkillInfo[107])
        {
            case 1:
                damage = (float)(skillManager.skillData.GetData(107)._Level1) * (playerInfo.StatInt);
                break;
            case 2:
                damage = (float)(skillManager.skillData.GetData(107)._Level2) * (playerInfo.StatInt);
                break;
            case 3:
                damage = (float)(skillManager.skillData.GetData(107)._Level3) * (playerInfo.StatInt);
                break;
        }
    }
    protected override void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3.5f, 8);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                CharacterHealthSystem healthSystem = collider.GetComponent<CharacterHealthSystem>();
                collider.gameObject.AddComponent<Slow>();
                InvokeRepeating("AddDamage", 0.5f, 0.5f);
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
        Invoke("SkillEnd", 2f);
    }
    protected void SkillEnd()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteors : BaseSkill
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        Damage();
        Invoke("SkillEnd", 4f);
    }
    protected override void Damage()
    {
        switch (playerSkill.playerSkillInfo[106])
        {
            case 1:
                damage = (float)(skillManager.skillData.GetData(106)._Level1) * (playerInfo.StatInt);
                break;
            case 2:
                damage = (float)(skillManager.skillData.GetData(106)._Level2) * (playerInfo.StatInt);
                break;
            case 3:
                damage = (float)(skillManager.skillData.GetData(106)._Level3) * (playerInfo.StatInt);
                break;
            case 0:
                damage = 5;
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 10);
    }
    protected override void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10, 8);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                CharacterHealthSystem healthSystem = collider.GetComponent<CharacterHealthSystem>();
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
    }
    protected override void SkillEnd()
    {
        Destroy(gameObject);
    }
}

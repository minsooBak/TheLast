using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance : BaseSkill
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        speed = 3f;
        Damage();
    }
    protected override void Damage()
    {
        switch (playerSkill.playerSkillInfo[103])
        {
            case 1:
                damage = (float)(skillManager.skillData.GetData(103)._Level1) * (playerInfo.StatInt);
                break;
            case 2:
                damage = (float)(skillManager.skillData.GetData(103)._Level2) * (playerInfo.StatInt);
                break;
            case 3:
                damage = (float)(skillManager.skillData.GetData(103)._Level3) * (playerInfo.StatInt);
                break;
        }
    }
    protected override void FixedUpdate()
    {
        if (target != null)
        {
            rigidbody.velocity = transform.TransformDirection(Vector3.forward) * speed;
            Vector3 targetPos = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetPos);
        }
        else
        {
            rigidbody.velocity = transform.TransformDirection(Vector3.forward) * speed;
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
            healthSystem.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

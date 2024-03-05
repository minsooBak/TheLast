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
        speed = 10f;
        Damage();
        Invoke("SkillEnd", 10f);
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
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
            healthSystem.TakeDamage(damage);
            Destroy(gameObject);
            collision.gameObject.AddComponent<Slow>();
        }
    }
    protected override void SkillEnd()
    {
        Destroy(gameObject);
    }
}

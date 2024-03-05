using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : BaseSkill
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        speed = 4f;
        Damage();
        Invoke("SkillEnd", 10f);
    }
    protected override void Damage()
    {
        switch (playerSkill.playerSkillInfo[102])
        {
            case 1:
                damage = (float)(skillManager.skillData.GetData(102)._Level1) * (playerInfo.StatInt);
                break;
            case 2:
                damage = (float)(skillManager.skillData.GetData(102)._Level2) * (playerInfo.StatInt);
                break;
            case 3:
                damage = (float)(skillManager.skillData.GetData(102)._Level3) * (playerInfo.StatInt);
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
        if (collision.gameObject.layer == 8)
        {
            healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
            healthSystem.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    protected override void SkillEnd()
    {
        Destroy(gameObject);
    }
}

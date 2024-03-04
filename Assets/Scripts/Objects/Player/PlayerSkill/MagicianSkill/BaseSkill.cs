using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    protected float speed;
    protected float damage;

    protected Rigidbody rigidbody;
    protected Player player;
    protected CharacterHealthSystem healthSystem;
    protected PlayerSkillInfo skillInfo;
    protected SkillManager skillManager;
    protected PlayerInfo playerInfo;

    protected GameObject target;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<Player>();
        target = player.skillHandler.target;
        skillManager = GameManager.PlayerManager.SkillManager;
        playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
    }

    protected virtual void Start()
    {
        skillInfo = skillManager.skillData.GetData(101);
        Damage();
    }
    protected virtual void Damage()
    {
        switch (skillInfo)
        {

        }
    }
    protected virtual void FixedUpdate()
    {
        if (target != null)
        {
            rigidbody.velocity = transform.forward * speed;
            Vector3 targetPos = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetPos);
        }
        else
        {
            rigidbody.velocity = transform.forward * speed;
        }
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
            healthSystem.TakeDamage(damage);
        }
    }
}
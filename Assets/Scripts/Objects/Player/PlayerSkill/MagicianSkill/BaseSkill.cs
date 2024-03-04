using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    protected float speed;
    protected float damage;
    protected float count;
    protected bool inTarget;

    protected Rigidbody rigidbody;
    protected Player player;
    protected CharacterHealthSystem healthSystem;
    protected SkillManager skillManager;
    protected PlayerInfo playerInfo;
    protected PlayerSkill playerSkill;

    protected GameObject target;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<Player>();
        target = player.skillHandler.target;
        skillManager = GameManager.PlayerManager.SkillManager;
        playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
        playerSkill = GameManager.PlayerManager.SkillManager.PlayerSkill;
    }

    protected virtual void Start()
    {
        speed = 6f;
        Damage();
    }
    protected virtual void Damage()
    {
        switch (playerSkill.playerSkillInfo[101])
        {
            case 1:
                damage = (float)(skillManager.skillData.GetData(101)._Level1) * (playerInfo.StatInt);
                break;
            case 2:
                damage = (float)(skillManager.skillData.GetData(101)._Level2) * (playerInfo.StatInt);
                break;
            case 3:
                damage = (float)(skillManager.skillData.GetData(101)._Level3) * (playerInfo.StatInt);
                break;
        }
    }
    protected virtual void FixedUpdate()
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
    protected virtual void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.gameObject.tag);
            healthSystem = collision.gameObject.GetComponent<CharacterHealthSystem>();
            healthSystem.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    protected virtual void OnTriggerStay(Collider collision)
    {
        
    }
    protected virtual void OnTriggerExit(Collider collision)
    {
        
    }
}
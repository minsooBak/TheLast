using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    protected float speed = 10f;
    protected float damage = 10f;

    protected Rigidbody rigidbody;
    protected Player player;
    protected CharacterHealthSystem healthSystem;

    protected GameObject target;

    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<Player>();
        target = player.skillHandler.target;
    }
    protected void FixedUpdate()
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
            healthSystem.TakeDamage(10f);
        }
    }
}
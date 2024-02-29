using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // TODO Enemy데이터 추가
    public EnemyData Data { get; private set; }

    public float targetingRadius = 5f;
    public float attackRange = 1.5f;

    // public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterHealthSystem healthSystem { get; private set; }
    public LayerMask targetLayer;
    public Transform target;
    private EnemyStateMachine stateMachine;
    public StateMachine StateMachin => stateMachine;

    [field: Header("data")]
    public float MaxHp { get; set; }
    public float Hp { get; set; }
    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        healthSystem = GetComponent<CharacterHealthSystem>();
        stateMachine = new EnemyStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    public void SetData(int id)
    {
        GameManager.DataBases.TryGetDataBase(out EnemyDataBase enemyDataBase);
        Data = enemyDataBase.GetData(id);
        MaxHp = Data.HP;
        Hp = Data.HP;
    }
    private void Update()
    {
        stateMachine.Update();
    }
    public bool IsAvaliableAttack
    {
        get
        {
            if (!target)
            {
                return false;
            }
            float distance = Vector3.Distance(transform.position, target.position);
            return (distance <= attackRange);
        }
    }

    internal Transform DetectPlayer()
    {
        target = null;
        Collider[] targetInTargetingRadius = Physics.OverlapSphere(
            transform.position, targetingRadius, targetLayer);
        if (targetInTargetingRadius.Length > 0)
        {
            target = targetInTargetingRadius[0].transform;
        }
        return target;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

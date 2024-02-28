using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // TODO Enemy데이터 추가
    public float targetingRadius = 5f;
    public float attackRange = 1.5f;
    
    // public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public LayerMask targetLayer;
    public Transform target;
    private EnemyStateMachine stateMachine;
    public StateMachine StateMachin => stateMachine;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();
        stateMachine = new EnemyStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }
    public bool IsAvaliableAttack
    {
        get
        {
            if(!target)
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
        if(targetInTargetingRadius.Length > 0)
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

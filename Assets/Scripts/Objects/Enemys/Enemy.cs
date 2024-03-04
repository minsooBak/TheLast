using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(NavMeshAgent)),
    RequireComponent(typeof(Animator)), RequireComponent(typeof(CharacterHealthSystem))]
public class Enemy : MonoBehaviour
{
    // TODO Enemy데이터 추가
    [field: Header("EnemyData")]
    [field: SerializeField] public EnemyInfo Data;

    public bool CanPatrol { get; private set; }
    // public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterHealthSystem HealthSystem { get; private set; }
    public LayerMask targetLayer;
    public Transform target;
    private EnemyStateMachine stateMachine;
    public EnemyStateMachine StateMachine => stateMachine;

    //TODO 돌아올 장소    
    public Vector3 WayPoint { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        HealthSystem = GetComponent<CharacterHealthSystem>();
        stateMachine = new EnemyStateMachine(this);
        Data = new EnemyInfo();
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    private void Start()
    {
        HealthSystem.OnDie += OnDie;
    }
    private void Update()
    {
        stateMachine.Update();
    }
    public void SetData(int ID)
    {
        Data.SetData(ID);
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
            return (distance <= Data.AttackRange);
        }
    }

    internal Transform DetectPlayer()
    {
        target = null;
        Collider[] targetInTargetingRadius = Physics.OverlapSphere(
            transform.position, Data.TargetingRadius, targetLayer);
        if (targetInTargetingRadius.Length > 0)
        {
            target = targetInTargetingRadius[0].transform;
        }
        return target;
    }
    public void SetWayPoint(Vector3 position)
    {
        WayPoint = position;
        CanPatrol = true;
    }
    public void OnDie()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Data.TargetingRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Data.AttackRange);
    }
}

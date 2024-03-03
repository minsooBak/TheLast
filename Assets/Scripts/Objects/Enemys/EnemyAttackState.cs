using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private string attackParameterName = "Attack";
    public EnemyAttackState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = _stateMachine.Enemy;
    }
    public void Enter()
    {
        Debug.Log("AttackEnter");
        _stateMachine.Enemy.Animator.SetTrigger(attackParameterName);
    }

    public void Exit()
    {
        Debug.Log("AttackExit");
       
    }

    public void HandleInput()
    {

    }

    public void PhysicsUpdate()
    {

    }

    public void Update()
    {
    }
}

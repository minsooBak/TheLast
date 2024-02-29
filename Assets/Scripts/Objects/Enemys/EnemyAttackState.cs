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
        _stateMachine.Enemy.Animator.SetBool(attackParameterName, true);
    }

    public void Exit()
    {
        Debug.Log("AttackExit");
        _stateMachine.Enemy.Animator.SetBool(attackParameterName, false);
    }

    public void HandleInput()
    {
        
    }

    public void PhysicsUpdate()
    {
        
    }

    public void Update()
    {
        Transform enemy = _enemy.DetectPlayer();
        if (enemy)
        {
            if (!_enemy.IsAvaliableAttack)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
    }
}

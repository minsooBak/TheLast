using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private string moveParameterName = "Move";
    public EnemyPatrolState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = _stateMachine.Enemy;
    }
    public void Enter()
    {
        _enemy.Agent?.SetDestination(_enemy.WayPoint);
        if(_enemy.Agent.remainingDistance > _enemy.Agent.stoppingDistance)
            _enemy.Animator.SetBool(moveParameterName, true);
    }

    public void Exit()
    {
        _enemy.Animator.SetBool(moveParameterName, false);
        _enemy.Agent.ResetPath();
    }

    public void HandleInput()
    {
        //nothing
    }

    public void PhysicsUpdate()
    {
        //nothing
    }

    public void Update()
    {
        Transform enemy = _enemy.DetectPlayer();
        if (enemy)
        {
            if (_enemy.IsAvaliableAttack)
            {
                _stateMachine.ChangeState(_stateMachine.AttackState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.MoveState);
            }
        }
        else
        {
            if(!_enemy.Agent.pathPending && (_enemy.Agent.remainingDistance <= _enemy.Agent.stoppingDistance))
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
            else
            {
                _enemy.Controller.Move(_enemy.Agent.velocity * Time.deltaTime * _enemy.Data.MoveSpeed);
            }
        }
    }
}

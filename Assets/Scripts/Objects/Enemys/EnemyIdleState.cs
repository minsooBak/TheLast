using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private string idleParameterName = "Idle";

    private float _elapsedTimeInIdle;
    private float _maxIdleTime;
    public EnemyIdleState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = _stateMachine.Enemy;
        _maxIdleTime = 1f;
    }
    public void Enter()
    {
        _enemy.Controller?.Move(Vector3.zero);
        _elapsedTimeInIdle = 0;
    }

    public void Exit()
    {

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
        _elapsedTimeInIdle += Time.deltaTime;
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
        else if (_enemy.CanPatrol && _elapsedTimeInIdle >= _maxIdleTime)
        {
            _stateMachine.ChangeState(_stateMachine.PatrolState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : IState
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private NavMeshAgent _agent;
    public EnemyMoveState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = _stateMachine.Enemy;
        _agent = _enemy.Agent;
    }
    public void Enter()
    {
        _enemy.Agent?.SetDestination(_enemy.target.position);
        Debug.Log("MoveEnter");
    }

    public void Exit()
    {
        Debug.Log("MoveExit");
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
            _agent.SetDestination(_enemy.target.position);
            if(_enemy.Agent.remainingDistance > _agent.stoppingDistance)
            {
                _enemy.Controller.Move(_agent.velocity * Time.deltaTime);

            }
        }
        if(!enemy && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}

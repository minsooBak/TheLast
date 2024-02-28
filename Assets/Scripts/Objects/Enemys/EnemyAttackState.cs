using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    private EnemyStateMachine _stateMachine;
    public EnemyAttackState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }   
    public void Enter()
    {
        Debug.Log("AttackEnter");
        _stateMachine.ChangeState(_stateMachine.IdleState);
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

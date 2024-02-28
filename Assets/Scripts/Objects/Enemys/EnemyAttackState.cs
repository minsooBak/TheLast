using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    protected EnemyStateMachine stateMachine;
    public EnemyAttackState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }   
    public void Enter()
    {
        Debug.Log("AttackEnter");
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
        throw new System.NotImplementedException();
    }
}

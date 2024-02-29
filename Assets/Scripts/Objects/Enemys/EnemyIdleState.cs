using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private string idleParameterName = "Idle";
    public EnemyIdleState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = _stateMachine.Enemy;
    }
    public void Enter()
    {
        _enemy.Controller?.Move(Vector3.zero);
        //_stateMachine.Enemy.Animator.SetBool(idleParameterName, true);
        //TODO Idle 애니메이션 off
        Debug.Log("IdleEnter");
    }

    public void Exit()
    {
        //TODO Idle 애니메이션 off
        //_stateMachine.Enemy.Animator.SetBool(idleParameterName, false);
        Debug.Log("IdleExit");
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
    }
}

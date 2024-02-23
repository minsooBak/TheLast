using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!stateMachine.Player.Controller.isGrounded
       && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }
        if (!stateMachine.Isrunning)
            stateMachine.ChangeState(stateMachine.IdleState);
        else
            StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
        base.OnMovementCanceled(context);
    }
    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnRunCanceled(context);
    }
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (stateMachine.Player.Controller.isGrounded)
            stateMachine.ChangeState(stateMachine.JumpState);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }
    private void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.ComboAttackState);
    }


}
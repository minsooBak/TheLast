using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;
    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }


    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.performed += OnRunStarted;
        input.PlayerActions.Run.canceled += OnRunCanceled;

        input.PlayerActions.Jump.started += OnJumpStarted;

        input.PlayerActions.Attack.performed += OnAttackPerformed;
        input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }


    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;
        input.PlayerActions.Run.started -= OnRunCanceled;

        input.PlayerActions.Jump.started -= OnJumpStarted;

        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void OnJumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    protected virtual void OnRunStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        stateMachine.Isrunning = true;
    }

    protected virtual void OnMovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
    protected virtual void OnAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }
    protected virtual void OnAttackCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }
    protected virtual void OnRunCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        stateMachine.Isrunning = false;
    }

    //Read InputActions
    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }


    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        Move(movementDirection);
        if (stateMachine.Isrunning && stateMachine.MovementInput != Vector2.zero)
        {
            StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
        }
        if (!stateMachine.Isrunning && stateMachine.MovementInput == Vector2.zero || !stateMachine.Player.Controller.isGrounded)
        {
            StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
        }
    }


    private Vector3 GetMovementDirection()
    {
        Vector3 foward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;
        foward.y = 0;
        right.y = 0;

        foward.Normalize();
        right.Normalize();

        return foward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }
    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.Controller.Move(
            ((movementDirection * movementSpeed) + stateMachine.Player.ForceReceiver.Movement) * Time.deltaTime
            );
    }
    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

}

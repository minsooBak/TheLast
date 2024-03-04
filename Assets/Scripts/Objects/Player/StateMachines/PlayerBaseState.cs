using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;
    private VirtualCameraController virtualCameraController;


    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundData;
        virtualCameraController = stateMachine.Player.VirtualCameraController;
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
        Vector3 cameraDirection = GetCameraDirection();
        Rotate(cameraDirection);
        Move(movementDirection);
        if (stateMachine.MovementInput != Vector2.zero)
            virtualCameraController.MoveCameraBackward();
        if (stateMachine.Isrunning && stateMachine.MovementInput != Vector2.zero)
        {
            StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
        }
        if (!stateMachine.Isrunning && stateMachine.MovementInput == Vector2.zero || !stateMachine.Player.Controller.isGrounded)
        {
            StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
        }
    }

    //캐릭터 이동시 방향은 플레이어 전방 
    private Vector3 GetMovementDirection()
    {
        if (!stateMachine.Player.Controller.isGrounded) return stateMachine.JumpDirection;
        else
        {
            Vector3 foward = stateMachine.Player.transform.forward;
            Vector3 right = stateMachine.Player.transform.right;
            foward.y = 0;
            right.y = 0;

            foward.Normalize();
            right.Normalize();
            stateMachine.JumpDirection = foward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
            return foward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
        }
    }
    private Vector3 GetCameraDirection()
    {
        //카메라 벡터 =  플레이어 위치 - 카메라 위치
        Vector3 cameraFoward = stateMachine.Player.transform.position - virtualCameraController.v_CameraTarget.position;//VirtualCameraController.instace.v_CameraTarget.position;
        cameraFoward.y = 0;
        return cameraFoward;
        
    }
    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.Controller.Move((stateMachine.Player.ForceReceiver.Movement) * Time.deltaTime);
        if (stateMachine.MovementInput.y != 0|| virtualCameraController.isVirtualCameraOn) //앞뒤로 이동중, 카메라이동중에는 플레이어 좌우 이동 가능
        {
            stateMachine.Player.Controller.Move(
            ((movementDirection * movementSpeed)+ stateMachine.Player.ForceReceiver.Movement) * Time.deltaTime
            );
        }
        else if(stateMachine.MovementInput.x != 0) //이동중이 아니거나 카메라 고정 시 플레이어 좌우 회전
        {
            float playerRot = stateMachine.Player.transform.rotation.eulerAngles.y+ stateMachine.MovementInput.x;
            Quaternion targetRotation = Quaternion.Euler(0, playerRot, 0);
            stateMachine.Player.transform.rotation = targetRotation;
        }
 
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

    private void Rotate(Vector3 cameraDirection)
    {
        //카메라 이동모드에서 마우스 좌측 = 플레이서 회전 없이 카메라만 회전, 마우스 우측 = 카메라 회전방향으로 플레이어 회전
        if (virtualCameraController.isVirtualCameraOn  && !Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraDirection);
            stateMachine.Player.transform.rotation = targetRotation;
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

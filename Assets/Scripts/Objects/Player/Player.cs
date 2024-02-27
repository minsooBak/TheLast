using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public PlayerInfo playerInfo { get; private set; }
    public PlayerInfoHandler playerInfoHandler { get; private set; }

    public CharacterHealthSystem healthSystem { get; private set; }

    private PlayerStateMachine stateMachine;
    

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        playerInfoHandler = GetComponent<PlayerInfoHandler>();
        healthSystem = GetComponent<CharacterHealthSystem>();

        playerInfo = new PlayerInfo();
        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
        healthSystem.OnDie += Dead;
        //Debug.Log(playerInfo.Hp);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void Dead()
    {
        Debug.Log("PlayerDie");
    }
}

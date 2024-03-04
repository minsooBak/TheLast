using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

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
    public CharacterHealthSystem healthSystem { get; private set; }
    public PlayerSkillHandler skillHandler { get; private set; }

    private PlayerStateMachine stateMachine;
    public byte id = 1;

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        healthSystem = GetComponent<CharacterHealthSystem>();
        skillHandler = GetComponent<PlayerSkillHandler>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        healthSystem.OnDie += Dead;
    }

    private void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.I))
        {
            var invenUI = GameManager.UIManager.GetUI<InventoryUI>();
            if (invenUI.IsActive())
                invenUI.Disable();
            else
                invenUI.Active();
        }
        else if(UnityEngine.Input.GetKeyDown(KeyCode.K))
        {
            var skillUI = GameManager.UIManager.GetUI<PlayerSkillUI>();
            if(skillUI.IsActive())
                skillUI.Disable();
            else
                skillUI.Active();
        }

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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

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
    public VirtualCameraController VirtualCameraController { get; private set; }

    private PlayerStateMachine stateMachine;
    private PlayerInfo _playerInfo;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public Slider hpSlider;
    public Slider mpSlider;
    public byte id = 1;

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        VirtualCameraController = GetComponentInChildren<VirtualCameraController>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        healthSystem = GetComponent<CharacterHealthSystem>();
        skillHandler = GetComponent<PlayerSkillHandler>();

        stateMachine = new PlayerStateMachine(this);
        _playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        healthSystem.OnDie += Dead;
    }

    private void Update()
    {
        hpText.text = _playerInfo.Hp.ToString() + " / " + _playerInfo.MaxHp.ToString();
        mpText.text = _playerInfo.Mp.ToString() + " / " + _playerInfo.MaxMp.ToString();
        hpSlider.value = _playerInfo.Hp / _playerInfo.MaxHp;
        mpSlider.value = _playerInfo.Mp / _playerInfo.MaxMp;

        if (UnityEngine.Input.GetKeyDown(KeyCode.I))
        {
            var invenUI = GameManager.UIManager.GetUI<InventoryUI>();
            if (invenUI.IsActive())
                invenUI.Disable();
            else
                invenUI.Active();
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

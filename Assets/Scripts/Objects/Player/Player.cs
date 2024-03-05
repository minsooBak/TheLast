using Enums;
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
    public bool isDie;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public Slider hpSlider;
    public Slider mpSlider;
    public byte id = 1;
    private float speed = 1f;

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
        hpText.text = _playerInfo.Hp.ToString("F0") + " / " + _playerInfo.MaxHp.ToString("F0");
        mpText.text = _playerInfo.Mp.ToString() + " / " + _playerInfo.MaxMp.ToString();
        hpSlider.value = Mathf.Lerp(hpSlider.value, _playerInfo.Hp / _playerInfo.MaxHp,speed*Time.deltaTime);
        mpSlider.value = _playerInfo.Mp / _playerInfo.MaxMp;

        if (UnityEngine.Input.GetKeyDown(KeyCode.I))
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
        else if(UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            var infoUI = GameManager.UIManager.GetUI<PlayerInfoUI>();
            if (infoUI.IsActive())
                infoUI.Disable();
            else
                infoUI.Active();
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
        Debug.Log("플레이어 사망");
        Animator.SetTrigger(AnimationData.DieParameterHash);
        isDie = true;
        StartCoroutine(PlayerDie());
    }
    IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(1.5f);
        isDie = false;
        ScenesManager scenesManager = GameManager.ScenesManager;
        scenesManager.ChangeScene(SceneState.VillageScene);
    }
}

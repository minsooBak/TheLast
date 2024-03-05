using UnityEngine;
using UnityEngine.Assertions;
using Enums;

public class IntroScene : IBaseScene
{
    public override void Init()
    {
        //초기 UI생성
        ResourceManager resourceManager = GameManager.ResourceManager;

        //캐릭터 선택씬으로
        ScenesManager scenesManager = GameManager.ScenesManager;
        Assert.IsTrue(GameObject.Find("IntroUI").TryGetComponent(out IntroUI introUI));
        introUI.AddListener(() => { scenesManager.ChangeScene(SceneState.CharaterSelectScene); });

        GameManager.UIManager.GetUI<AudioMixerController>();
    }
}

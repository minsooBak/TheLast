using UnityEngine;
using UnityEngine.Assertions;
using Enums;

public class IntroScene : IBaseScene
{
    public override void Init()
    {
        //초기 UI생성
        ResourceManager resourceManager = GameManager.ResourceManager;

        //플레이어 데이터 체크 후 맵데이터 세팅 및 버튼 이벤트 변경
        if (Utility.IsExistsFile("PlayerData"))
        {
            //던전or마을에 어느위치에 스폰되는지
            //맵매니저의 SettingData();
            //맵매니저의 Init();
            //인트로 -> 캐릭터선택 or 던전 or 마을 
        }
        else
        {
            //캐릭터 선택씬으로
            ScenesManager scenesManager = GameManager.ScenesManager;
            Assert.IsTrue(GameObject.Find("IntroUI").TryGetComponent(out IntroUI introUI));
            introUI.AddListener(()=> { scenesManager.ChangeScene(SceneState.VillageScene); });
            GameManager.DataBases.TryGetDataBase(out ItemDataBase data);
            Debug.Log(data.GetData(10002001).Name);
        }
    }
}

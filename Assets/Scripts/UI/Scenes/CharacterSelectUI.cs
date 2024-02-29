using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    CharacterDefaultData characterDefaultData;
    CharacterLoadData characterLoadData;
    [SerializeField] private List<TextMeshProUGUI> slotText1;
    [SerializeField] private List<TextMeshProUGUI> slotText2;
    [SerializeField] private List<TextMeshProUGUI> slotText3;
    [SerializeField] private List<GameObject> characterSlot;
    [SerializeField] private List<GameObject> emptySlot;
    [SerializeField] private List<Image> jobIcon;

    private void Awake()
    {
        characterDefaultData = new CharacterDefaultData();
        characterLoadData = new CharacterLoadData();
        characterDefaultData.Init();
        characterLoadData.Init();
    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        DisplaySelectScene();
    }
    private void DisplaySelectScene()
    {
        for(int i = 0; i < 3; i++) 
        {            
            if (characterLoadData.userDataList != null)
            {
                UserData userData = characterLoadData.userDataList.user[i];
                characterSlot[i].SetActive(true);
                emptySlot[i].SetActive(false);
                if (i==0) 
                { 
                    slotText1[0].text = userData.Level.ToString();
                    slotText1[1].text = (userData.statusId==(byte)1 ? "마법사" : "오크전사");
                    slotText1[2].text = userData.characterName.ToString();
                    jobIcon[i] = (userData.statusId == (byte)1 ? Resources.Load<Image>("UI/Icon/UI/wizard") : Resources.Load<Image>("UI/Icon/UI/orc"));
                }
                if (i == 1)
                {
                    slotText2[0].text = userData.Level.ToString();
                    slotText2[1].text = (userData.statusId == (byte)1 ? "마법사" : "오크전사");
                    slotText2[2].text = userData.characterName.ToString();
                    jobIcon[i] = (userData.statusId == (byte)1 ? Resources.Load<Image>("UI/Icon/UI/wizard") : Resources.Load<Image>("UI/Icon/UI/orc"));
                }
                if (i == 2)
                {
                    slotText3[0].text = userData.Level.ToString();
                    slotText3[1].text = (userData.statusId == (byte)1 ? "마법사" : "오크전사");
                    slotText3[2].text = userData.characterName.ToString();
                    jobIcon[i] = (userData.statusId == (byte)1 ? Resources.Load<Image>("UI/Icon/UI/wizard") : Resources.Load<Image>("UI/Icon/UI/orc"));
                }
            }
            else
            {
                characterSlot[i].SetActive(false);
                emptySlot[i].SetActive(true);
            }
        }
    }

}

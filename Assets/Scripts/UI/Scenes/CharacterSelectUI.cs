using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    private CharacterLoadData characterLoadData;
    public Dictionary<int, UserData> userDatas = new(3);
    [SerializeField] private List<TextMeshProUGUI> slotLvText;
    [SerializeField] private List<TextMeshProUGUI> slotClassText;
    [SerializeField] private List<TextMeshProUGUI> slotNameText;
    [SerializeField] private List<GameObject> characterSlot;
    [SerializeField] private List<GameObject> emptySlot;
    [SerializeField] private List<Light> lights;
    [SerializeField] private List<Image> charSlotBG;
    public GameObject mainUI;
    public GameObject generateUI;
    [SerializeField] private List<Image> jobIcon;
    
    private bool[] isExistData = { false, false, false };
    public int slotNum;
    public int selected;
    private Vector3[] slotPos = new Vector3[3];
    private GameObject[] obj = new GameObject[3];
    private Animator[] animators = new Animator[3];
    UserData userData;

    protected virtual void Awake()
    {
        characterLoadData = new CharacterLoadData();
        characterLoadData.Init();
        slotPos[0] = new Vector3(-7, 0, 2);
        slotPos[1] = new Vector3(0, 0, 2);
        slotPos[2] = new Vector3(7, 0, 2);
    }
    private void Start()
    {
        Init();
        selected = Animator.StringToHash("Selected");
    }
    public void Init()
    {
        if (characterLoadData.loadedUserData != null)
            userDatas = characterLoadData.loadedUserData;
        DisplaySelectScene();
    }
    private void DisplaySelectScene()
    {
        foreach (KeyValuePair<int, UserData> pair in userDatas)
        {
            int key = pair.Key;
            userData = pair.Value;
            DisplayCharacterSlot(key - 1, userData);
            if (!isExistData[key - 1])
                InstantiateCharacter(key - 1, userData);
        }
        for (int i = 0; i< 3; i++)
        {
            if(!userDatas.ContainsKey(i+1)&&mainUI)
                            DisplayEmptySlot(i);
        }

        //if (userDatas != null)
        //{
        //    if (userDatas.ContainsKey(i + 1))
        //    {
        //        UserData userData = userDatas[i + 1];
        //        characterSlot[i].SetActive(true);
        //        emptySlot[i].SetActive(false);
        //        if (i == 0)
        //        {
        //            slotLvText[0].text = "Lv." + userData.Level.ToString();
        //            slotLvText[1].text = (userData.statusId == (byte)1 ? "마법사" : "오크전사");
        //            slotLvText[2].text = userData.characterName.ToString();
        //            InstantiateCharacter(i, userData);
        //        }
        //        if (i == 1)
        //        {
        //            slotClassText[0].text = "Lv." + userData.Level.ToString();
        //            slotClassText[1].text = (userData.statusId == (byte)1 ? "마법사" : "오크전사");
        //            slotClassText[2].text = userData.characterName.ToString();
        //            InstantiateCharacter(i, userData);
        //        }
        //        if (i == 2)
        //        {
        //            slotNameText[0].text = "Lv." + userData.Level.ToString();
        //            slotNameText[1].text = (userData.statusId == (byte)1 ? "마법사" : "오크전사");
        //            slotNameText[2].text = userData.characterName.ToString();
        //            InstantiateCharacter(i, userData);
        //        }
        //    }
        //    else
        //    {
        //        characterSlot[i].SetActive(false);
        //        emptySlot[i].SetActive(true);
        //    }
        //}
        //else
        //{
        //    characterSlot[i].SetActive(false);
        //    emptySlot[i].SetActive(true);
        //}

    }
    private void DisplayCharacterSlot(int index, UserData userData)
    {
        characterSlot[index].SetActive(true);
        emptySlot[index].SetActive(false);
        slotLvText[index].text = "Lv." + userData.Level.ToString();
        slotClassText[index].text = (userData.statusId == (byte)1 ? "마법사" : "오크전사");
        slotNameText[index].text = userData.characterName.ToString();
    }
    private void DisplayEmptySlot(int index)
    {
        characterSlot[index].SetActive(false);
        emptySlot[index].SetActive(true);
    }
    public void DisplayMainUI()
    {
        mainUI.SetActive(true);
        generateUI.SetActive(false);
    }
    public void DisplayGenerateUI()
    {
        mainUI.SetActive(false);
        generateUI.SetActive(true);
    }

    public void InstantiateCharacter(int i, UserData userData)
    {
        string imagePath = (userData.statusId == (byte)1 ? "Icon/UI/wizard" : "Icon/UI/orc");
        Sprite temp = Resources.Load<Sprite>(imagePath);
        jobIcon[i].sprite = temp;
        string prefabPath = (userData.statusId == (byte)1) ? "Prefabs/UI/Wizard" : "Prefabs/UI/Grunt";
        obj[i] = GameManager.ResourceManager.Instantiate(prefabPath);
        animators[i] = obj[i].GetComponent<Animator>();
        obj[i].transform.position = slotPos[i];
        if (i == 0) obj[i].transform.rotation = Quaternion.Euler(0, 140, 0);
        if (i == 2) obj[i].transform.rotation = Quaternion.Euler(0, 230, 0);
        isExistData[i] = true;
    }
    public void OnButtonClick(int i)
    {
        slotNum = i;
        if (isExistData[i - 1])
        {
            foreach (Light light in lights)
            {
                light.gameObject.SetActive(false);
            }
            lights[i - 1].gameObject.SetActive(true);            
            ChangeColor(i - 1, charSlotBG, new Color(0.64f, 0.25f, 0.60f, 0.4f), new Color(0.64f, 0.25f, 0.60f, 1));
            animators[i - 1].SetTrigger(selected);
            Debug.Log("하하하");
        }
        else
        {
            DisplayGenerateUI();
        }
    }
    public void OnButtonClickDelete()
    {
        if(slotNum!=0)
        {
            userDatas.Remove(slotNum);
            Save();
            isExistData[slotNum - 1] = false;
            Destroy(obj[slotNum - 1]);
            Init();
        }
    }
    public void OnButtonClickStart()
    {
        GameManager.PlayerManager.PlayerInfoManager.userData = userDatas[slotNum];
        GameManager.PlayerManager.PlayerInfoManager.Init2();
        ScenesManager scenesManager = GameManager.ScenesManager;
        scenesManager.ChangeScene(SceneState.VillageScene);
    }

    public void Save()
    {
        characterLoadData.loadedUserData = userDatas;
        characterLoadData.SaveUserData();
    }

    public void ChangeColor(int index, List<Image> images,Color canceled, Color selected)
    {
        foreach (Image image in images)
        {
            image.color = canceled;
        }
        images[index].color = selected;
    }
}

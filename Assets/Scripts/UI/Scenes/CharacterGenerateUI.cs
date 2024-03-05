using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGenerateUI : MonoBehaviour
{
    private CharacterDefaultData characterDefaultData;
    private CharacterSelectUI characterSelectUI;
    public UserData generatedData;
    [SerializeField] private List<TextMeshProUGUI> statusText;
    [SerializeField] private List<Image> classImage;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator wizardAnim;
    [SerializeField] private Animator gruntAnim;
    public byte statusId;
    private int classIndex;
    private bool isValid;
    private string _name;
    Vector3 wizardPos = new Vector3(-890, -539, 20);
    Vector3 gruntPos = new Vector3(-883, -539, 20);

    private void Awake()
    {
        characterDefaultData = new CharacterDefaultData();
        generatedData = new UserData();
        characterDefaultData.Init();
        OnButtonClickClass(0);
    }
    private void Start()
    {
        characterSelectUI = GetComponentInParent<CharacterSelectUI>();
    }

    private void DisplayText(int index)
    {
        PlayerStatusInfo wizardInfo = new PlayerStatusInfo();
        PlayerStatusInfo gruntInfo = new PlayerStatusInfo();
        statusText[0].text = characterDefaultData.defaultData[index]._hp.ToString();
        statusText[1].text = characterDefaultData.defaultData[index]._mp.ToString();
        statusText[2].text = characterDefaultData.defaultData[index]._adef.ToString();
        statusText[3].text = characterDefaultData.defaultData[index]._mdef.ToString();
        statusText[4].text = characterDefaultData.defaultData[index]._str.ToString();
        statusText[5].text = characterDefaultData.defaultData[index]._int.ToString();
        statusText[6].text = characterDefaultData.defaultData[index]._luk.ToString();
        statusText[7].text = characterDefaultData.defaultData[index]._speed.ToString();
        description.text = characterDefaultData.defaultData[index]._description.ToString();

    }
    public void OnButtonClickClass(int index)
    {
        if (index == 0)
        {
            statusId = 1;
            cameraTransform.transform.localPosition = wizardPos;
            wizardAnim.SetTrigger(Animator.StringToHash("Selected"));
        }
        else
        {
            statusId = 2;
            cameraTransform.transform.localPosition = gruntPos;
            gruntAnim.SetTrigger(Animator.StringToHash("Selected"));
        }
        classIndex = index;
        classImage[0].color = classImage[1].color = new Color(1, 1, 1, 0.5f);
        classImage[index].color = Color.white;

        DisplayText(index);
    }
    public void OnButtonClickGenerate()
    {
        _name = characterName.text;
        if (CheckName(_name))
        {
            SetDatas(classIndex);
            characterSelectUI.Save();
            characterSelectUI.Init();
            characterName.text = "";
            characterSelectUI.DisplayMainUI();
        }
    }

    private void SetDatas(int index)
    {
        generatedData.characterSlot = characterSelectUI.slotNum;
        generatedData.statusId = statusId;
        generatedData.characterName = characterName.text;
        generatedData.Level = 1;
        generatedData.exp = 0;
        generatedData.stageLv = 1;
        characterSelectUI.userDatas.Remove(characterSelectUI.slotNum);
        characterSelectUI.userDatas.Add(characterSelectUI.slotNum, generatedData);
    }
    private bool CheckName(string name)
    {
        foreach(var item in characterSelectUI.userDatas)
        {
            if(item.Value.characterName == name)
            {
                Debug.Log("아이디가 중복됨");
                return false;
            }
        }
        char[] chars = name.ToCharArray();
        if (name == null || name.Length < 3 || name.Length > 7) 
        {
            Debug.Log("이름의 길이가 잘못됨");
            return false;            
        }
        else
        {
            for(int i = 0; i < chars.Length-1;i++)
            {
                bool temp = IsEnglish(chars[i]) || IsKorean(chars[i]) || IsNum(chars[i]);
                isValid = true;
                isValid = isValid && temp;
            }
            if (!isValid) Debug.Log("한글,영어,숫자가 아님");
            return isValid;
        }
    }
    private bool IsKorean(char c)
    {
        return 0xAC00 <= c && c <= 0xD7A3;
    }
    private bool IsNum(char c)
    {
        return c >= '0' && c <= '9';
    }
    private bool IsEnglish(char c)
    {
        return (c >= 'a' && c <= 'z')|| (c >= 'A' && c <= 'Z');
    }
}

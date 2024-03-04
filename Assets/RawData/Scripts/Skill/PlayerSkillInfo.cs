using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSkillInfo
{
    public byte _id;
    public string _name;
    public string _description;
    public float _Level1;//레벨에 따른 값
    public float _Level2;
    public float _Level3;
    public string _damageType;
    public SkllType _skillType;
    public int _skillPoint;//스킬 레벨업 시 필요한 포인트
    public float _coolDown;
    public byte _upgrade;
    public string _prefabPath;
    public string _spritePath;

    private Sprite _sprite;

    public Sprite Sprite
    {
        get
        {
            if (_sprite == null)
                _sprite = Resources.Load<Sprite>($"Icon/Skill/{_spritePath}");
            return _sprite;
        }
    }
}

public enum SkllType
{
    ActiveAttack,
    ActiveBuff
}
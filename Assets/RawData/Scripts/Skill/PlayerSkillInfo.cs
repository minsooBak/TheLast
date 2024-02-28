using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSkillInfo
{
    public byte _id;
    public string _name;
    public string _description;
    public float _Level1;
    public float _Level2;
    public float _Level3;
    public string _damageType;
    public string _skillType;
    public int _skillPoint;
    public float _coolDown;
    public int _upgrade;
    public string _prefabPath;
    public string _spritePath;
}

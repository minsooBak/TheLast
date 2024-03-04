using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInfo
{
    //Player Status
    private string playerName; //저장
    private string job;
    private short level;
    private float exp; //저장
    //health
    private float hp;
    private float maxHp;
    private float mp;
    private float maxMp;
    //defense
    private float aDef;
    private float mDef;
    //stat
    private float statStr;
    private float statInt;
    private float statLuk;
    //stat Up Point
    private int strUpPoint = 0; //저장
    private int intUpPoint = 0; //저장
    private int lukUpPoint = 0; //저장
    //point
    private int statPoint = 0;
    private int skillPoint = 0;
    //move
    private float jump;
    private float speed;

    //Status Property
    public string PlayerName { get { return playerName; } set { playerName = value; } }
    public string Job { get { return job; } set { job = value; } }
    public short Level { get { return level; } set { level = value; } }
    public float Exp { get { return exp; } set { exp = value; } }
    public float Hp { get { return hp; } set { hp = value; } }
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float Mp { get { return mp; } set { mp = value; } }
    public float MaxMp { get { return maxMp; } set { maxMp = value; } }
    public float ADef { get { return aDef; } set { aDef = value; } }
    public float MDef { get { return mDef; } set { mDef = value; } }
    public float StatStr { get { return statStr; } set { statStr = value; } }
    public float StatInt { get { return statInt; } set { statInt = value; } }
    public float StatLuk { get { return statLuk; } set { statLuk = value; } }
    public int StrUpPoint { get { return strUpPoint; } set { strUpPoint = value; } }
    public int IntUpPoint { get { return intUpPoint; } set { intUpPoint = value; } }
    public int LukUpPoint { get { return lukUpPoint; } set { lukUpPoint = value; } }
    public int StatPoint { get { return statPoint; } set { statPoint = value; } }
    public int SkillPoint { get { return skillPoint; } set { skillPoint = value; } }
    public float Jump { get { return jump; } set { jump = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

}
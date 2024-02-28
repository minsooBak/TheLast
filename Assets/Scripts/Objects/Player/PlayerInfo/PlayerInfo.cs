using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInfo
{
    //Player Status
    private string playerName;
    private string job;
    private short level = 1;
    private float exp = 0;
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
    //point
    private int statPoint;
    private int skillPoint;
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
    public float Jump { get { return jump; } set { jump = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

}
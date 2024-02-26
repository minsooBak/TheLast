using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInfo
{
    //Player Status
    private string playerName;
    private string job;
    private float exp;
    //health
    private float hp;
    private float Maxhp;
    private float mp;
    private float Maxmp;
    //defense
    private float adef;
    private float mdef;
    //stat
    private float statStr;
    private float statInt;
    private float statLuk;
    //move
    private float jump;
    private float speed;

    //Status Property
    public string PlayerName { get { return playerName; } set { playerName = value; } }
    public string Job { get { return job; } set { job = value; } }
    public float Exp { get { return exp; } set { exp = value; } }
    public float Hp { get { return hp; } set { hp = value; } }
    public float MaxHp { get { return Maxhp; } set { Maxhp = value; } }
    public float Mp { get { return mp; } set { mp = value; } }
    public float MaxMp { get { return Maxmp; } set { Maxmp = value; } }
    public float Adef { get { return adef; } set { adef = value; } }
    public float Mdef { get { return mdef; } set { mdef = value; } }
    public float StatStr { get { return statStr; } set { statStr = value; } }
    public float StatInt { get { return statInt; } set { statInt = value; } }
    public float StatLuk { get { return statLuk; } set { statLuk = value; } }
    public float Jump { get { return jump; } set { jump = value; } }
    public float Speed { get { return speed; } set { speed = value; } }
}
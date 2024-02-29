using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthSystem : MonoBehaviour
{
    //[SerializeField] private float m_hp;
    //[SerializeField] private float m_mp;
    //private float hp;
    //private float mp;
    private int characterType;
    private int playerHash;
    private int enemyHash;
    private Player player;
    private Enemy enemy;
    public event Action OnDie;
    public event Action OnSkill;
    public PlayerInfo playerInfo;
    //public event Action NotEnoughMana;


    private void Start()
    {
        playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
        characterType = Utility.GetHashWithTag(gameObject);
        playerHash = Utility.GetHashWithString("Player");
        enemyHash = Utility.GetHashWithString("Enemy");
        Init();
        TakeDamage(10f);
        TakeDamage(10f);
    }

    private void Init()
    {
        if (playerHash == characterType)
        {
            player = GetComponent<Player>();
            playerInfo.Hp = playerInfo.MaxHp;
            playerInfo.Mp = playerInfo.MaxMp;
        }
        if (enemyHash == characterType)
        {
            //enemy = GetComponent<Enemy>();
            //enemy.cur_hp = enemy.m_hp;
            //enemy.cur_mp = enemy.m_mp;
        }
    }
    public void TakeDamage(float damage)
    {
        if (playerHash == characterType)
        {
            if (playerInfo.Hp == 0) return;
            playerInfo.Hp = Mathf.Min(playerInfo.MaxHp, Mathf.Max(playerInfo.Hp - damage, 0));
            Debug.Log(playerInfo.Hp);

            if (playerInfo.Hp == 0)
                OnDie?.Invoke();
        }
        if (enemyHash == characterType)
        {
            //if (enemy.cur_hp == 0) return;
            //enemy.cur_hp = Mathf.Min(player.m_hp, Mathf.Max(enemy.cur_hp - damage, 0));

            //if (enemy.cur_hp == 0)
            //    OnDie?.Invoke();
        }

    }
    public void UseMp(float skillmp)
    {
        if (playerHash == characterType)
        {
            //if (player.cur_mp < skillmp)
            //{
            //    NotEnoughMana?.Invoke();
            //    return;
            //}
            //player.cur_mp = Mathf.Min(player.m_mp, player.cur_mp - skillmp);
            //OnSkill?.Invoke(); 
        }
        if (enemyHash == characterType)
        {
            //if (enemy.cur_mp < skillmp)
            //{
            //    NotEnoughMana?.Invoke();
            //    return;
            //}
            //enemy.cur_mp = Mathf.Min(player.m_mp, enemy.cur_mp - skillmp);
            //OnSkill?.Invoke();
        }
    }
}


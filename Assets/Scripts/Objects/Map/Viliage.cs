using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viliage : MonoBehaviour
{
    [SerializeField] private AudioClip _bgm;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(_bgm, Camera.main.transform.position, 10f, 10f);
    }    
}

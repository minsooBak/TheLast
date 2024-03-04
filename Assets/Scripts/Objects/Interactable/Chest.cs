using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private Animation _animation;

    void Awake()
    {
        _animation = GetComponent<Animation>();
    }

    public void InteractEnter()
    {
        return;
    }

    public void InteractExit()
    {
        return;
    }

    public void Interaction()
    {
        _animation.Play();

        Invoke("ShowUI", 1.5f);
    }

    private void ShowUI()
    {
        GameManager.ResourceManager.Instantiate("Prefabs/UI/RewardUI");
    }
}

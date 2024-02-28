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
    // For Debug
    void Start()
    {
        _animation.Play();
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
        // TODO 보상 UI 띄우기
    }
}

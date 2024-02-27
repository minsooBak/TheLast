using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour , IInteractable
{
    private Animator _ani;
    private int _dungeonDoorOpen = Animator.StringToHash("DungeonDoorOpen");
    private int _dungeonDoorClose = Animator.StringToHash("DungeonDoorClose");
    void Awake()
    {
       _ani = GetComponent<Animator>();
    }
    // For Debug
    void Start()
    {
        _ani.Play(_dungeonDoorOpen);
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
    }
}

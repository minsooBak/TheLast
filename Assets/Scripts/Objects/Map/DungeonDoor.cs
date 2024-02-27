using UnityEngine;

public class DungeonDoor : MonoBehaviour
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
    public void DoorClose()
    {
        _ani.Play(_dungeonDoorClose);
    }
    public void DoorOpen()
    {
        _ani.Play(_dungeonDoorOpen);
    }
}

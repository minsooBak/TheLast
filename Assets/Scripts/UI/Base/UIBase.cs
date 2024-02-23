using UnityEngine;

public class UIBase : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public bool IsActive()
    {
        return canvas.enabled;
    }

    public void Active()
    {
        canvas.enabled = true;
    }

    public void Disable()
    {
        canvas.enabled = false;
    }
}

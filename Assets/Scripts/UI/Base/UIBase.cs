using UnityEngine;

public class UIBase : MonoBehaviour
{
    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

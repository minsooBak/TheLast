using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private Button startButton;

    public void AddListener(UnityAction action)
    {
        startButton.onClick.AddListener(action);
    }
}

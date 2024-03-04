using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private AudioClip _bgm;

    public void AddListener(UnityAction action)
    {
        startButton.onClick.AddListener(action);
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private AudioClip _bgm;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(_bgm,Camera.main.transform.position,10f,10f);
    }
    public void AddListener(UnityAction action)
    {
        startButton.onClick.AddListener(action);
    }
}

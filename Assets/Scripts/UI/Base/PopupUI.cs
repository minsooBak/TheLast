using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : UIBase
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private Button button;
    public void Init(string header, string main, Action action = null)
    {
        headerText.text = header;
        mainText.text = main;
        button.onClick.AddListener(() => { action?.Invoke(); Disable(); button.onClick.RemoveAllListeners(); });
    }
}

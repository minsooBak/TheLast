using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropItemPopupUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _input;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private Image _inventoryBlockImage;

    [SerializeField] private Button _confirmBtn;
    public void Init(string header, string main, Action action)
    {
        _headerText.text = header;
        _mainText.text = main;
        _input.gameObject.SetActive(false);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        _confirmBtn.onClick.RemoveAllListeners();
        _confirmBtn.onClick.AddListener(()=> { action(); _inventoryBlockImage.enabled = false; Disable(); });
        _inventoryBlockImage.enabled = true;
    }

    public void Init(string header, string mainText, Action<int> action, int amount)
    {
        _input.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        _mainText.text = mainText;
        _headerText.text = header;
        _confirmBtn.onClick.RemoveAllListeners();
        _confirmBtn.onClick.AddListener(() => { Confirm(action, amount); _inventoryBlockImage.enabled = false; });
        _inventoryBlockImage.enabled = true;
    }

    private void Confirm(Action<int> action, int amount)
    {
        int result = int.Parse(_input.text);
        _input.text = string.Empty;

        if (result > amount)
        {
            _inventoryBlockImage.enabled = true;
            GameManager.UIManager.ShowUI<PopupUI>().Init("Inventory Popup", "The number you entered is greater than the quantity of items.");
            return;
        }

        action(result);
        Disable();
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

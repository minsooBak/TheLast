using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _highlight;
    private RectTransform _rectTransform;
    private int _amount = 0;

    public int Index { get; private set; }
    public ItemEntity Item { get; private set; }
    public int Amount { get { return _amount; } }
    public Transform IconTransform { get { return _icon.transform; } }
    public bool IsMaxAmount { get { return Amount == Item.MaxAmount; } }

    public void Init(int index)
    {
        Index = index;
    }   

    public void IconReset()
    {
        _icon.transform.SetParent(transform);
        _rectTransform.anchoredPosition = Vector2.zero;
    }

    public void Awake()
    {
        _rectTransform = _icon.GetComponent<RectTransform>();
    }

    public bool HasItem() { return Item != null; } 

    public Sprite Icon {
        get 
        { 
            return _icon.sprite;
        } 
        private set 
        {
            _icon.sprite = value; 
        } 
    }

    public void SetItem(ItemEntity item)
    {
        Item = item;
        if (item != null)
        {
            Icon = item.Sprite;
            SetAmount(_amount);
        }
        else
        {
            Icon = null;
            _amountText.enabled = false;
        }
    }

    public void SetAmount(int amount)
    {
        if (Item == null) return;

        _amount = amount;
        if (Amount <= 1)
        {
            _amountText.enabled = false;
            return;
        }

        if (!_amountText.enabled) _amountText.enabled = true;
        _amountText.text = Amount.ToString();
    }

    public void AddAmount()
    {
        if (++_amount <= 1)
        {
            _amountText.enabled = false;
            return;
        }

        if (!_amountText.enabled) _amountText.enabled = true;
        _amountText.text = Amount.ToString();
    }

    public void SetColor(Color color)
    {
        _highlight.color = color;
    }
}

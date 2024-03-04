using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class ItemDataInfoUI : UIBase
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _description;
    private StringBuilder _sb = new(200);
    private RectTransform _rectTransform;

    public void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(ItemEntity item)
    {
        _itemName.text = item.Name;
        StatText(item);
        _description.text = item.Description;
    }

    private void OnDisable()
    {
        _statText.text = string.Empty;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
        Rect baseRect = _rectTransform.rect;

        float x = transform.position.x + baseRect.xMax;
        if (x > Screen.width)
        {
            transform.position += new Vector3(Screen.width - x, 0);
        }

        float y = transform.position.y + baseRect.yMin;
        if (y < 0)
        {
            transform.position += new Vector3(0, -y); 
        }
    }

    private void StatText(ItemEntity item)
    {
        if (item.ItemType == ItemType.Consume) return;

        CheckStat(item.HP, "HP");
        CheckStat(item.MP, "MP");
        CheckStat(item.ADEF, "ADEF");
        CheckStat(item.MDEF, "MDEF");
        CheckStat(item.STR, "STR");
        CheckStat(item.INT, "INT");
        CheckStat(item.LUK, "LUK");

        CheckStatPercent(item.Jump, "Jump");
        CheckStatPercent(item.Speed, "Speed");

        _statText.text = _sb.ToString();
        _sb.Clear();
    }

    private void CheckStat(int amount, string name)
    {
        if (amount == 0) return;

        _sb.AppendLine($"{name} : {amount}");
    }

    private void CheckStatPercent(int amount, string name)
    {
        if (amount == 0) return;

        _sb.AppendLine($"{name} : {amount}%");
    }
}

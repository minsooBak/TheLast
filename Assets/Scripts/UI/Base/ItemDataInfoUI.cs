using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataInfoUI : UIBase
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _statText;
    [SerializeField] private TextMeshProUGUI _description;
    private StringBuilder _sb = new(200);

    public void Init(ItemEntity item)
    {
        _itemName.text = item.Name;
        StatText(item);
        _description.text = item.Description;
    }

    private void OnDisable()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    private void StatText(ItemEntity item)
    {
        _statText.text = string.Empty;
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

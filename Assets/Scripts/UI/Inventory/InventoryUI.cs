using UnityEngine;

public class InventoryUI : UIBase
{
    [SerializeField] private Transform parent;
    [SerializeField] private SlotUI[] slots;

    protected void Start()
    {
        var prefab = GameManager.ResourceManager.LoadPrefab("Prefabs/UI/Slot");
        slots = new SlotUI[20];
        for(int i = 0; i < slots.Length; ++i)
        {
            int indexX = i % 4;
            int indexY = i / 4;
            float x = (indexX * 140) + 40;
            float y = (indexY * -120) - 20;
            slots[i] = Instantiate(prefab, parent).GetComponent<SlotUI>();
            var rect = slots[i].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(x, y);
        }

        Debug.Log($"Create Slot");
    }
}

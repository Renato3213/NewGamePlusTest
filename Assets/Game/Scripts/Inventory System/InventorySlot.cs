using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UIElements;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private ItemData _itemData;

    public static Action OnItemInventoryDrop;

    public string SlotUID => _slotUID;
    [SerializeField] private string _slotUID;

    int _currentItemValue;

    void OnEnable()
    {
        Item.OnDragStart += CheckChildren;
    }

    void OnDisable()
    {
        Item.OnDragStart -= CheckChildren;
    }

    void CheckChildren()
    {
        if (transform.childCount == 0)
        {
            _itemData = null;
            _currentItemValue = 0;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) //ensures only one item can be in the slot
        {
            GameObject dropped = eventData.pointerDrag;
            Item item = dropped.GetComponent<Item>();
            GetItemInformation(item.ItemData);
            item.ParentAfterDrag = transform;
            _currentItemValue = item.Value;
            OnItemInventoryDrop?.Invoke();

            if (!item.OnInventory)
            {
                item.OnInventory = true;
                Inventory.Instance.AddItem(_itemData);
            }
        }
    }


    private void GetItemInformation(ItemData itemData)
    {
        _itemData = itemData;
    }

    public void Save(ref InventorySlotSaveData data)
    {
        data.itemData = _itemData;
        data.itemValue = _currentItemValue;
        data.slotUID = SlotUID;
    }

    public void Load(InventorySlotSaveData data)
    {
        CreateItemAndAttach(data.itemData, data.itemValue);
    }
    public void CreateItemAndAttach(ItemData data, int value)
    {
        if (transform.childCount != 0)
        {
            Destroy(transform.GetChild(0).gameObject); // since we are loading, destroy current inventory
        }

        if (data == null) return;


        GameObject _itemInSlot = Instantiate(data.EmptyItem, transform.position, Quaternion.identity, transform);
        Item item = _itemInSlot.GetComponent<Item>();
        item.ItemData = data;

        item.transform.localPosition = Vector2.zero;
        item.OnInventory = true;

        if (data.IsValuable)
            item.Value = value;

        item.InitializeItem();
    }
}

[System.Serializable]
public struct InventorySlotSaveData
{
    public ItemData itemData;
    public int itemValue;
    public string slotUID;
}

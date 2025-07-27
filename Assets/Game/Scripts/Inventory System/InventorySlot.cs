using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;
using System;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private ItemData _itemData;

    public static Action OnItemInventoryDrop;


    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) //ensures only one item can be in the slot
        {
            GameObject dropped = eventData.pointerDrag;
            Item item = dropped.GetComponent<Item>();
            item.OnInventory = true;
            GetItemInformation(item.ItemData);
            item.ParentAfterDrag = transform;
            OnItemInventoryDrop?.Invoke();
            Inventory.Instance.AddItem(_itemData);
        }
    }

    private void GetItemInformation(ItemData itemData)
    {
        _itemData = itemData;
    }
}

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
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            Item item = dropped.GetComponent<Item>();
            item.OnInventory = true;
            GetItemInformation(item.ItemData);
            item.ParentAfterDrag = transform;
            OnItemInventoryDrop?.Invoke();
        }
    }

    private void GetItemInformation(ItemData itemData)
    {
        _itemData = itemData;
    }
}

using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class DroppedItemSlot : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] RectTransform _itemHolder;
    [SerializeField] TextMeshProUGUI _itemTitle;
    [SerializeField] TextMeshProUGUI _itemDescription;
    [SerializeField] GameObject _droppedItem;

    private GameObject _itemInSlot;

    void OnEnable()
    {
        InventorySlot.OnItemInventoryDrop += ClosePanel;
        InventorySlot.OnItemInventoryDrop += DestroyDroppedItem;
        DroppedItem.OnInteractionEnded += ClosePanel;
        DroppedItem.OnItemInteracted += CreateItemAndOpenPanel;
    }

    private void OnDisable()
    {
        InventorySlot.OnItemInventoryDrop -= ClosePanel;
        InventorySlot.OnItemInventoryDrop -= DestroyDroppedItem;
        DroppedItem.OnInteractionEnded -= ClosePanel;
        DroppedItem.OnItemInteracted -= CreateItemAndOpenPanel;
    }

    void ClosePanel()
    {
        _panel.SetActive(false);
    }

    void DestroyDroppedItem() //item is no longer dropped
    {
        if (_itemInSlot != null)
        {
            _itemInSlot = null;
            Destroy(_droppedItem);
        }
    }

    public void CreateItemAndOpenPanel(ItemData data, GameObject go)
    {
        _droppedItem = go;

        if(_itemInSlot != null)
        {
            Destroy(_itemInSlot);
            _itemInSlot = null;
        }

        _itemInSlot = Instantiate(data.EmptyItem, _itemHolder.transform.position, Quaternion.identity, _itemHolder.transform);
        Item item = _itemInSlot.GetComponent<Item>();
        item.ItemData = data;

        if (data.IsValuable) //to set value on creation
        {
            item.Value = UnityEngine.Random.Range(data.MinValue, data.MaxValue);
            _itemDescription.text = $"Worth {item.Value} Coins!";
        }

        _panel.SetActive(true);
        item.InitializeItem();
    }
}

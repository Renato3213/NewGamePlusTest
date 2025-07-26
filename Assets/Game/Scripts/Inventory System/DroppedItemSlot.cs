using UnityEngine;
using TMPro;
public class DroppedItemSlot : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] RectTransform _itemHolder;
    private GameObject _itemInSlot;
    [SerializeField] TextMeshProUGUI _itemTitle;
    [SerializeField] TextMeshProUGUI _itemDescription;
    void OnEnable()
    {
        InventorySlot.OnItemInventoryDrop += ClosePanel;
        DroppedItem.OnInteractionEnded += ClosePanel;
        DroppedItem.OnItemInteracted += CreateItemAndOpenPanel;
    }

    private void OnDisable()
    {
        InventorySlot.OnItemInventoryDrop -= ClosePanel;
        DroppedItem.OnInteractionEnded -= ClosePanel;
        DroppedItem.OnItemInteracted -= CreateItemAndOpenPanel;
    }

    void ClosePanel()
    {
        _panel.SetActive(false);
    }

    public void CreateItemAndOpenPanel(ItemData data)
    {
        _panel.SetActive(true);

        if(_itemInSlot != null)
        {
            Destroy(_itemInSlot);
            _itemInSlot = null;
        }

        _itemInSlot = Instantiate(data.EmptyItem, _itemHolder.transform.position, Quaternion.identity, _itemHolder.transform);
        Item item = _itemInSlot.GetComponent<Item>();
        item.ItemData = data;
        item.InitializeItem();
    }
}

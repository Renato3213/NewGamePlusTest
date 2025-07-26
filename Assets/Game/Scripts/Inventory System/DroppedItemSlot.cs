using UnityEngine;
using TMPro;
public class DroppedItemSlot : MonoBehaviour
{
    [SerializeField] RectTransform _itemHolder;
    [SerializeField] TextMeshProUGUI _itemTitle;
    [SerializeField] TextMeshProUGUI _itemDescription;
    void OnEnable()
    {
        InventorySlot.OnItemInventoryDrop += CloseThisWindow;
    }

    private void OnDisable()
    {
        InventorySlot.OnItemInventoryDrop -= CloseThisWindow;
    }

    void CloseThisWindow()
    {
        gameObject.SetActive(false);
    }

    public void CreateItem(ItemData data)
    {

    }

    private void Initialize(Item item)
    {
        
    }
}

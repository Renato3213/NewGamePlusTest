using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> _items = new List<ItemData>();
    [SerializeField] private int _maxSlots;
    [SerializeField] private RectTransform _inventoryContent;
    [SerializeField] private List<string> _itemNames = new List<string>(); // List to know the names of items

    private static Inventory _instance;


    public int MaxSlots
    {
        get { return _maxSlots; }
    }

    public List<ItemData> Items
    {
        get { return _items; }
    }
    public List<string> ItemNames
    {
        get { return _itemNames; }
    }

    public static Inventory Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _maxSlots = _inventoryContent.childCount;
    }

    public bool AddItem(ItemData item)
    {
        if (_items.Count < _maxSlots)
        {
            _items.Add(item);
            _itemNames.Add(item.ItemUID);
            return true;
        }

        return false; // Inventory Full
    }

    public void RemoveItem(ItemData item)
    {
        _items.Remove(item);
        _itemNames.Remove(item.ItemUID);
    }

    public void LoadItems(List<string> savedItemNames, List<ItemData> allAvailableItems)
    {
        _items.Clear();
        _itemNames.Clear();

        foreach (var itemName in savedItemNames)
        {
            // Look the item with the corresponding name in the list of availableItems
            ItemData item = allAvailableItems.Find(i => i.name == itemName);
            if (item != null)
            {
                _items.Add(item);
                _itemNames.Add(itemName);
            }
        }
    }
}

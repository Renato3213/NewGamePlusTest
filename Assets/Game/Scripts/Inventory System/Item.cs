using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using TMPro;
public class Item : MonoBehaviour, IBeginDragHandler,IDragHandler ,IEndDragHandler
{
    public bool OnInventory { get { return _onInventory; } set { _onInventory = value; } } 
    [SerializeField] bool _onInventory;

    [SerializeField] private ItemData _itemData;

    [SerializeField] private Image _image;
    [SerializeField] private Image _isSelectedImage;

    [SerializeField] private Button _itemButton;
    [SerializeField] private Button _useItemButton;

    [SerializeField] private GameObject _informationPanel;

    [SerializeField] private TextMeshProUGUI _informationTitle;
    [SerializeField] private TextMeshProUGUI _informationDescription;

    public int Value { get { return _value; } set { _value = value; } } 
    [SerializeField] private int _value;

    private Transform _parentAfterDrag;
    public Transform ParentAfterDrag
    {
        get
        {
            return _parentAfterDrag;
        }
        set
        {
            _parentAfterDrag = value;
        }
    }

    public ItemData ItemData
    {
        get { return _itemData; }
        set { _itemData = value; }
    }

    public static Action OnUseHealthPotion;

    private void Start()
    {
        _itemButton = GetComponentInChildren<Button>();

        if (_itemButton != null)
        {
            _itemButton.onClick.AddListener(OnItemSelected);
        }


    }

    void OnEnable()
    {
        Inventory.OnItemClicked += SelectItem;
    }

    void OnDisable()
    {
        Inventory.OnItemClicked -= SelectItem;
    }

    public void InitializeItem()
    {
        _image.sprite = _itemData.ItemSprite;
        _informationTitle.text = _itemData.Name;
        _informationDescription.text = _itemData.Description;

        if (_itemData.IsValuable)
        {
            _informationDescription.text = $"Worth {_value} Coins!";
        }
        else //consider consumable
        {
            _useItemButton.gameObject.SetActive(true);
        }
    }

    public void DiscardItem()
    {
        Inventory.Instance.RemoveItem(_itemData);
        Destroy(gameObject);
    }

    public void UseItem() //since I only want one consumable for this project, this will do the job, even if its not the best approach
    {
        OnUseHealthPotion?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_parentAfterDrag);
        transform.localPosition = Vector2.zero;
        _image.raycastTarget = true;
    }

    void SelectItem(Item itemSelected)
    {
        Debug.Log("aa");
        if(itemSelected == this)
        {
            if (_itemData == null) return;

            if (_onInventory)
                _informationPanel.SetActive(!_informationPanel.activeSelf);


            _isSelectedImage.gameObject.SetActive(!_isSelectedImage.gameObject.activeSelf);
        }
        else
        {
            _informationPanel.SetActive(false);
            _isSelectedImage.gameObject.SetActive(false);
        }
    }
    public void OnItemSelected()
    {
        Inventory.OnItemClicked?.Invoke(this);
    }
}

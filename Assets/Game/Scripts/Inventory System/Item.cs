using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class Item : MonoBehaviour, IBeginDragHandler,IDragHandler ,IEndDragHandler
{
    public bool OnInventory { get { return _onInventory; } set { _onInventory = value; } } 
    [SerializeField] bool _onInventory;

    [SerializeField] private ItemData _itemData;

    [SerializeField] private Image _image;
    [SerializeField] private Image _isSelectedImage;

    [SerializeField] private Button _itemButton;

    [SerializeField] private GameObject _informationPanel;



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

    public Action OnButtonClicked;

    private void Start()
    {
        _itemButton = GetComponentInChildren<Button>();

        if (_itemButton != null)
        {
            _itemButton.onClick.AddListener(OnItemClicked);
        }


    }

    public void InitializeItem()
    {
        _image.sprite = _itemData.ItemSprite;
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

    public void OnItemClicked()
    {
        if (_itemData == null) return;

        if (_onInventory)
            _informationPanel.SetActive(!_informationPanel.activeSelf);

        OnButtonClicked?.Invoke();

        _isSelectedImage.gameObject.SetActive(!_isSelectedImage.gameObject.activeSelf);
    }
}

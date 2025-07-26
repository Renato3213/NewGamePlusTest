using UnityEngine;
using System;
public class DroppedItem : MonoBehaviour, IInteractable
{

    public bool IsDropped { get; private set; }

    [SerializeField] private ItemData _data;
    [SerializeField] private SpriteRenderer _sRenderer;

    public static Action<ItemData, GameObject> OnItemInteracted;
    public static Action OnInteractionEnded;

    void Start()
    {
        IsDropped = true;
    }

    public void InitializeItem()
    {
        _sRenderer.sprite = _data.ItemSprite;
    }


    public bool CanInteract()
    {
        return !IsDropped;
    }

    public void OnInteract()
    {
        OnItemInteracted?.Invoke(_data, this.gameObject);
        IsDropped = false;
    }

    public void OnEndInteraction()
    {
        OnInteractionEnded?.Invoke();
        IsDropped = true;
    }
}

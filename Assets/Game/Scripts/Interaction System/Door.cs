using UnityEngine;
using System;

public class Door : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }

    public string DoorUID { get; private set; }

    [SerializeField] private Sprite _openedSprite;
    private SpriteRenderer _sRenderer;

    [SerializeField] private bool _isWinCondition;

    public static Action OnDoorOpened;

    private void Start()
    {
        DoorUID ??= GlobalHelper.GenerateUID(gameObject);
        _sRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void OnInteract()
    {
        IsOpened = true;
        _sRenderer.sprite = _openedSprite;

        OnDoorOpened?.Invoke();
    }

    public void OnEndInteraction()
    {

    }
}

using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    public bool IsOpened { get; private set; }

    public string DoorUID { get; private set; }

    [SerializeField] private Sprite _openedSprite;
    private SpriteRenderer _sRenderer;

    [SerializeField] private bool isWinCondition;

    public static Action<bool> OnDoorOpened;

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

        OnDoorOpened(isWinCondition);
    }

    public void OnEndInteraction()
    {

    }
}

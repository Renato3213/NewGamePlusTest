using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }

    public string ChestID { get; private set; }

    [SerializeField] private GameObject[] _possibleItems;

    [SerializeField] private Sprite _openedSprite;

    private int _itemsToSpawn;

    private void Start()
    {
        ChestID ??= GlobalHelper.GenerateUID(gameObject);
    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }

    public void OnEndInteraction()
    {
        throw new System.NotImplementedException();
    }
}

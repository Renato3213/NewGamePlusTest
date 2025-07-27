using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }

    public string ChestUID { get; private set; }

    [SerializeField] private ItemData[] _possibleItems;
    [SerializeField] private DroppedItem _droppedItem;

    [SerializeField] private Sprite _openedSprite;
    private SpriteRenderer _sRenderer;

    [SerializeField] private int  _maxItemsToSpawn;

    private int _itemsToSpawn;

    [SerializeField] private float _throwForce;
    [SerializeField] private float _minAngleToThrow, _maxAngleToThrow;


    private void Start()
    {
        ChestUID ??= GlobalHelper.GenerateUID(gameObject);
        _itemsToSpawn = Random.Range(1, _maxItemsToSpawn);
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
        //spawn items and throw them at a random angle
        for (int i = 0; i < _itemsToSpawn + 1; i++)
        {
            DroppedItem item = Instantiate(_droppedItem, transform.position, Quaternion.identity);
            item.Data = _possibleItems[Random.Range(0, _possibleItems.Length)];
            item.InitializeItem();
            ThrowItem(item);
        }
    }

    void ThrowItem(DroppedItem item)
    {
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        float angleVariation = Random.Range(_minAngleToThrow, _maxAngleToThrow);

        Quaternion randomRotation = Quaternion.AngleAxis(angleVariation, Vector3.forward);

        Vector2 finalDirection = randomRotation * Vector3.up;

        rb.AddForce(finalDirection.normalized * _throwForce, ForceMode2D.Impulse);

    }

    public void OnEndInteraction()
    {

    }

    public void Save(ref ChestSaveData data)
    {
        data.IsOpened = IsOpened;
    }

    public void Load(ChestSaveData data)
    {
        IsOpened = data.IsOpened;

        if (IsOpened)
        {
            _sRenderer.sprite = _openedSprite;
        }
    }
}

[System.Serializable]
public struct ChestSaveData
{
    public bool IsOpened;
}

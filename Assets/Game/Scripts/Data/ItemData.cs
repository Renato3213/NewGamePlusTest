using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;

    public string Description => _description;
    [SerializeField] private string _description;

    public Sprite ItemSprite => _itemSprite;
    [SerializeField] private Sprite _itemSprite;

    public GameObject EmptyItem => _emptyItem;
    [SerializeField] private GameObject _emptyItem;

    public bool IsValuable => _isValuable; //if not valuable, consider consumable
    [SerializeField] private bool _isValuable;

    public int MinValue => _minValue;
    [SerializeField] private int _minValue;

    public int MaxValue => _maxValue;
    [SerializeField] private int _maxValue;

    public string ItemUID => this.name;
}

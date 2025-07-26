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
}

using UnityEngine;
using TMPro;
public class WinScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _victoryText;
    int totalCoinValue;
    void OnEnable()
    {
        foreach(var item in GameManager.Instance.InventorySlots)
        {
            totalCoinValue += (int)(item.CurrentValue * 0.5f);
        }
        _victoryText.text = $"Your inventory is worth {totalCoinValue} Coins!";
    }
    
}

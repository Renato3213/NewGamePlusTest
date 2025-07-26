using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;

    private void OnEnable()
    {
        DroppedItem.OnItemInteracted += ToggleInventory;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    void ToggleInventory(ItemData data, GameObject go) //just so i can open it through the same action
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}

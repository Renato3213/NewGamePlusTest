using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Canvas inventoryUI;

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
        inventoryUI.enabled = !inventoryUI.enabled;
    }

    void ToggleInventory(ItemData data, GameObject go) //just so i can open it through the same action
    {
        inventoryUI.enabled = !inventoryUI.enabled;
    }
}

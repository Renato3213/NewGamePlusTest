using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Canvas inventoryUI;
    [SerializeField] GameObject menuUI;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
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

    void ToggleMenu()
    {
        menuUI.SetActive(!menuUI.activeSelf);
    }
}

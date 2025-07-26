using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable
{

    public bool IsDropped { get; private set; }

    void Start()
    {
        IsDropped = true;
    }

  
    public bool CanInteract()
    {
        return !IsDropped;
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}

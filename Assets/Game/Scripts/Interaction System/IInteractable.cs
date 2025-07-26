using UnityEngine;

public interface IInteractable
{
    bool CanInteract();
    void OnInteract();

    void OnEndInteraction();
}

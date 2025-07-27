using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float _interactionRange = 2f;
    [SerializeField] private LayerMask _interactableLayer;

    [SerializeField] GameObject _interactionIndicator;

    private void Update()
    {
        if (!GameManager.Instance._canPlay) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _interactionRange, _interactableLayer);

        foreach (var hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null && interactable.CanInteract())
            {
                interactable.OnInteract();
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interactionIndicator.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactionIndicator.SetActive(false);

        IInteractable interactable;
        if(collision.TryGetComponent<IInteractable>(out interactable))
        {
            interactable.OnEndInteraction();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _interactionRange);
    }
}
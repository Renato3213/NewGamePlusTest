using UnityEngine;

public class Hazards : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable;
        if(collision.TryGetComponent<Damageable>(out damageable))
        {
            damageable.TakeDamage(1, 6f);
        }
    }
}

using UnityEngine;

public class Hazards : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable;
        if(collision.TryGetComponent<Damageable>(out damageable))
        {
            damageable.TakeDamage(damage, 6f);
        }
    }
}

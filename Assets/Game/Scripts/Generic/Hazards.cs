using UnityEngine;

public class Hazards : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("a");
        Damageable damageable;
        if(collision.TryGetComponent<Damageable>(out damageable))
        {
        Debug.Log("b");
            damageable.TakeDamage(damage, 6f);
        }
    }
}

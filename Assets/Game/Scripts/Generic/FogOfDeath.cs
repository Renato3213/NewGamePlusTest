using UnityEngine;

public class FogOfDeath : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    void FixedUpdate()
    {
        transform.Translate(direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable;
        if (collision.TryGetComponent<Damageable>(out damageable))
        {
            damageable.TakeDamage(3, 0f);
        }
        
    }
}

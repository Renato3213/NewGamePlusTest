using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private HealthManager healthComponent;
    [SerializeField] private bool isInvulnerable = false;
    [SerializeField] private float invulnerabilityTime = 0.5f;

    private float invulnerabilityTimer = 0f;

    private void Update()
    {
        if (isInvulnerable && invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;

            if (invulnerabilityTimer <= 0)
            {
                isInvulnerable = false;
            }
        }
    }

    public void TakeDamage(int damage, float knockbackForce)
    {
        if (isInvulnerable || healthComponent.IsDead) return;

        healthComponent.TakeDamage(damage);

        if (invulnerabilityTime > 0)
        {
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityTime;
        }

        if (TryGetComponent<Rigidbody2D>(out var rb))
        {
            float angleVariation = Random.Range(-10, 10);

            Quaternion randomRotation = Quaternion.AngleAxis(angleVariation, Vector3.forward);

            Vector2 finalDirection = randomRotation * Vector3.up;
            rb.AddForce(finalDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}

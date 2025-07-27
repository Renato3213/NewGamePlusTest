using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [System.Serializable]
    public class HealthEvent : UnityEvent<float> { }

    [SerializeField] private int maxHealth = 3;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => isDead;

    public HealthEvent OnHealthChanged;
    public UnityEvent OnDeath;
    public UnityEvent OnDamageTaken;
    public UnityEvent OnHealed;

    private int currentHealth;
    private float timeSinceLastDamage;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        Item.OnUseHealthPotion += Heal;
    }


    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        timeSinceLastDamage = 0f;

        OnHealthChanged?.Invoke(currentHealth / maxHealth);
        OnDamageTaken?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + 1, maxHealth);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
        OnHealed?.Invoke();
    }

    private void Die()
    {
        isDead = true;
        currentHealth = 0;
        OnHealthChanged?.Invoke(0f);
        OnDeath?.Invoke();


        Debug.Log(gameObject.name + " dead");
    }

    
}

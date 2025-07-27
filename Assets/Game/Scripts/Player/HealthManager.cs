using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [System.Serializable]
    public class HealthEvent : UnityEvent<float> { }

    [SerializeField] List<Image> _hearts;

    [SerializeField] private int _maxHealth = 3;

    public int CurrentHealth { get { return _currentHealth; } 
        set 
        { 
            ChangeHealthTo(value); 

        }}
    public int MaxHealth => _maxHealth;
    public bool IsDead => _isDead;

    public HealthEvent OnHealthChanged;
    public UnityEvent OnDeath;
    public UnityEvent OnDamageTaken;
    public UnityEvent OnHealed;

    private int _currentHealth;
    private float _timeSinceLastDamage;
    private bool _isDead = false;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        Item.OnUseHealthPotion += Heal;
        GameManager.Instance.Health = this;
    }


    public void TakeDamage(int amount)
    {
        if (_isDead) return;

        _hearts[_currentHealth-1].color = Color.black;
        _currentHealth -= amount;
        _timeSinceLastDamage = 0f;

        OnHealthChanged?.Invoke(_currentHealth / _maxHealth);
        OnDamageTaken?.Invoke();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        if (_isDead) return;

        _currentHealth = Mathf.Min(_currentHealth + 1, _maxHealth);
        _hearts[_currentHealth-1].color = Color.white;
        OnHealthChanged?.Invoke(_currentHealth / _maxHealth);
        OnHealed?.Invoke();
    }

    void ChangeHealthTo(int value)
    {
        _currentHealth = value;

        for (int i = 0; i < _hearts.Count; i++)
        {
            if(i > value - 1)
            {
                _hearts[i].color = Color.black;
            }
        }
    }

    private void Die()
    {
        _isDead = true;
        _currentHealth = 0;
        OnHealthChanged?.Invoke(0f);
        OnDeath?.Invoke();

        GameManager.Instance.Lose();

        Debug.Log(gameObject.name + " dead");
    }

    
}

using System;
using UnityEngine;

public enum CharacterType { PLAYER, ENEMY, FRIENDLY }

public class Health : MonoBehaviour
{
    public static event Action<CharacterType> OnDied;
    public int currentHealth { get; private set; }

    [field: SerializeField]
    public int startHealth { get; private set; } = 100;
    
    [SerializeField]
    private CharacterType characterType;

    [SerializeField]
    private string damageSound;

    private HealthBar healthBar;
    private EffectController effect;

    private void Awake()
    {
        currentHealth = startHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        effect = GetComponent<EffectController>();
    }

    private void Start()
    {
        healthBar.SetHealth(currentHealth, startHealth);
    }

    public void TakeDamage(int amount)
    {
        SoundManager.PlaySound(damageSound);
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth, startHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > startHealth)
        {
            currentHealth = startHealth;
        }
        healthBar.SetHealth(currentHealth, startHealth);
    }

    private void Die()
    {
        effect.PlayEffect();
        if (OnDied != null)
        {
            OnDied(characterType);
        }
        Destroy(gameObject);
    }
}

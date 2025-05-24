using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public float healRate = 10f;
    public float healDelay = 10f;
    public float currentHealth;
    private float lastDamageTime;
    private float lastHealTime = -Mathf.Infinity;

    public HPotionCountdown hpotionCountdown;
    public HealthBar healthBar;
    private PauseMenu pauseMenu;


    private void Start()
    {
        currentHealth = maxHealth;
        lastDamageTime = Time.time;

        healthBar.SetMaxHealth(maxHealth);

        pauseMenu = FindAnyObjectByType<PauseMenu>();

        if(hpotionCountdown != null )
        {
            hpotionCountdown.SetCooldownDuration(healDelay);
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            TryHeal();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage, Current Health: " + currentHealth);

        lastDamageTime = Time.time;
        

        healthBar.setHealth(currentHealth);

        if (currentHealth <= 0f)
        {
            OnDeath();
        }
    }

    private void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Player healed. Current Health: " + currentHealth);
        healthBar.setHealth(currentHealth);
    }

    private void TryHeal()
    {
        if (Time.time >= lastHealTime + healDelay && currentHealth < maxHealth)
        {
            Heal(50f);
            lastHealTime = Time.time;
            hpotionCountdown.TriggerCooldown();
        }
        else
        {
            Debug.Log("Heal on cooldown or already at full health.");
        }
    }
    
    private void OnDeath()
    {
        pauseMenu.TriggerDeath();
        Debug.Log("Player is Dead!");
    }
}

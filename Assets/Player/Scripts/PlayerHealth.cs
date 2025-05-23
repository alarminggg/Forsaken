using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float healRate = 10f;
    public float healDelay = 10f;

    public float currentHealth;
    private float lastDamageTime;

    public HealthBar healthBar;

    private PauseMenu pauseMenu;


    private void Start()
    {
        currentHealth = maxHealth;
        lastDamageTime = Time.time;

        healthBar.SetMaxHealth(maxHealth);

        pauseMenu = FindAnyObjectByType<PauseMenu>();
    }

    private void Update()
    {
        
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

    
    private void OnDeath()
    {
        pauseMenu.TriggerDeath();
        Debug.Log("Player is Dead!");
    }
}

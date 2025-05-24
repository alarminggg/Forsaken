using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    public bool isMainBoss = false;

    private PauseMenu pauseMenu;
    private void Start()
    {
        pauseMenu = FindAnyObjectByType<PauseMenu>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died");

        if(isMainBoss && pauseMenu != null)
        {
            pauseMenu.TriggerWin();
            Debug.Log("MAIN BOSS DEFEATED");
        }

        Destroy(gameObject); 
    }
}

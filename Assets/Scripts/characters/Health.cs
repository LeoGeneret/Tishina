using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int startingHealth = 5;

    private int currentHealth;

    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}

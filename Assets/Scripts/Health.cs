#region v1
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Fields

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Animator animator;
    [SerializeField] private bool destroyOnDeath = false;
    [SerializeField] private float destroyDelay = 1.5f;

    private static readonly int DeadTrigger = Animator.StringToHash("Dead");

    private int currentHealth;
    private bool isDead;

    #endregion

    public bool IsDead => isDead;

    #region Unity Lifecycle

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    #endregion

    #region Damage

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger(DeadTrigger);

        if (destroyOnDeath)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    #endregion
}
#endregion
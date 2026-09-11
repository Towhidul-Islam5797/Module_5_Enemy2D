#region v1

using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyScoreReward : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }
    }
}
#endregion
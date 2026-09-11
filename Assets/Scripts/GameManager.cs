#region v1
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text stateText;

    [Header("Game End")]
    [SerializeField] private float freezeDelay = 1.5f;

    private int score;
    private bool gameEnded;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SubscribeToPlayerDeath();
        SubscribeToSpawner();
        UpdateScoreText();
    }

    #endregion

    #region Setup

    private void SubscribeToPlayerDeath()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.OnDeath += HandleLose;
        }
    }

    private void SubscribeToSpawner()
    {
        EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.OnAllWavesComplete += HandleWin;
        }
    }

    #endregion

    #region Scoring

    public void AddScore(int amount)
    {
        if (gameEnded) return;

        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    #endregion

    #region Game State

    private void HandleWin()
    {
        if (gameEnded) return;
        EndGame("You Win!");
    }

    private void HandleLose()
    {
        if (gameEnded) return;
        EndGame("Game Over");
    }

    private void EndGame(string message)
    {
        gameEnded = true;

        if (stateText != null)
        {
            stateText.text = message;
        }

        StartCoroutine(FreezeAfterDelay());
    }

    private IEnumerator FreezeAfterDelay()
    {
        yield return new WaitForSecondsRealtime(freezeDelay);
        Time.timeScale = 0f;
    }

    #endregion
}
#endregion

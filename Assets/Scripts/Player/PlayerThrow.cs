using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private float throwCooldown = 0.4f;
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private Transform throwPoint;

    private PlayerController playerController;
    private float cooldownTimer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnThrow(InputValue value)
    {
        if (!value.isPressed) return;
        if (cooldownTimer > 0f) return;

        PerformThrow();
        cooldownTimer = throwCooldown;
    }

    private void Update()
    {
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void PerformThrow()
    {
        Vector2 direction = playerController.FacingRight ? Vector2.right : Vector2.left;
        projectilePool.Spawn(throwPoint.position, direction);
    }
}
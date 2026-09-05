using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;

    private Rigidbody2D rb;
    private ProjectilePool sourcePool;
    private float lifetimeTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, ProjectilePool pool)
    {
        sourcePool = pool;
        lifetimeTimer = lifetime;
        rb.linearVelocity = direction.normalized * speed;
    }

    private void Update()
    {
        UpdateLifetime();
    }

    private void UpdateLifetime()
    {
        lifetimeTimer -= Time.deltaTime;

        if (lifetimeTimer <= 0f)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        rb.linearVelocity = Vector2.zero;
        sourcePool.Release(this);
    }
}
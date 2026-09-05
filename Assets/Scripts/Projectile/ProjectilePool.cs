using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 20;

    private ObjectPool<Projectile> pool;

    private void Awake()
    {
        pool = new ObjectPool<Projectile>(
            CreateProjectile,
            OnGetProjectile,
            OnReleaseProjectile,
            OnDestroyProjectile,
            collectionCheck: true,
            defaultCapacity,
            maxSize);
    }

    public void Spawn(Vector2 position, Vector2 direction)
    {
        Projectile projectile = pool.Get();
        projectile.transform.position = position;
        projectile.Launch(direction, this);
    }

    public void Release(Projectile projectile)
    {
        pool.Release(projectile);
    }

    private Projectile CreateProjectile()
    {
        return Instantiate(projectilePrefab);
    }

    private void OnGetProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnReleaseProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}
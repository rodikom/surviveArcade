using UnityEngine;
public class ProjectileFactory : IProjectileFactory
{
    private readonly ProjectilePool pool;

    public ProjectileFactory(ProjectilePool pool)
    {
        this.pool = pool;
    }

    public Bullet Create(
        ProjectileType type,
        Vector3 position,
        Quaternion rotation,
        float damage,
        float lifeTime,
        string targetTag,
        Transform parent = null
    )
    {
        var bullet = pool.Get(type);

        bullet.transform.SetPositionAndRotation(position, rotation);

        if (parent != null)
            bullet.transform.SetParent(parent);

        bullet.Init(damage, lifeTime, targetTag, type);
        bullet.OnGetFromPool();

        return bullet;
    }
}
using UnityEngine;

public interface IProjectileFactory
{
    Bullet Create(
        ProjectileType type,
        Vector3 position,
        Quaternion rotation,
        float damage,
        float lifeTime,
        string targetTag,
        Transform parent = null
    );
}
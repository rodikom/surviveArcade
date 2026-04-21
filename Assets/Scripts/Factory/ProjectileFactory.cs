using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : IProjectileFactory
{
    private readonly Dictionary<ProjectileType, GameObject> prefabs;

    public ProjectileFactory(Dictionary<ProjectileType, GameObject> prefabs)
    {
        this.prefabs = prefabs;
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
        var prefab = prefabs[type];

        var go = SpawnService.Instantiate(prefab, position, rotation);

        if (parent != null)
            go.transform.SetParent(parent);

        var bullet = go.GetComponent<Bullet>();
        bullet.Damage = damage;
        bullet.LifeTime = lifeTime;
        bullet.TargetTag = targetTag;

        return bullet;
    }
}
using System.Collections.Generic;
public class ProjectilePool
{
    private readonly Dictionary<ProjectileType, ObjectPool<Bullet>> pools;

    public ProjectilePool(Dictionary<ProjectileType, ObjectPool<Bullet>> pools)
    {
        this.pools = pools;
    }

    public Bullet Get(ProjectileType type)
    {
        return pools[type].Get();
    }

    public void Return(ProjectileType type, Bullet bullet)
    {
        pools[type].Return(bullet);
    }
}
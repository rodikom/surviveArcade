using UnityEngine;

public class EnemyPool
{
    private readonly ObjectPool<Enemy> pool;

    public EnemyPool(ObjectPool<Enemy> pool)
    {
        this.pool = pool;
    }

    public Enemy Get(Vector3 position)
    {
        var enemy = pool.Get();
        enemy.transform.position = position;
        enemy.OnGetFromPool();
        return enemy;
    }

    public void Return(Enemy enemy)
    {
        pool.Return(enemy);
    }
}
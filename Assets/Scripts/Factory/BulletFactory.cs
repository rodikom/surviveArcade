using UnityEngine;

public class BulletFactory : IFactory<Bullet>
{
    private readonly GameObject prefab;

    public BulletFactory(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public Bullet Create()
    {
        var go = Object.Instantiate(prefab);
        return go.GetComponent<Bullet>();
    }
}
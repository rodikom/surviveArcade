using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : Weapon
{
    [SerializeField]
    protected float range = 7f;
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int magazineSize = 6;
    [SerializeField]
    private Transform bulletStartPos;

    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        bulletStartPos = GameObject.Find("BulletStartPos").transform;
    }

    public override void Attack()
    {
        var factory = ServiceLocator.Get<IProjectileFactory>();

        factory.Create(
            ProjectileType.Bullet,
            bulletStartPos.position,
            bulletStartPos.rotation,
            damage,
            range,
            "Enemy"
        );
    }
}

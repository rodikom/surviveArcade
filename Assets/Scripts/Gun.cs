using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : Weapon
{
    // Visible fields
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

    private void Update()
    {

    }

    public override void Attack()
    {
        Debug.Log("Piu Piu");

        var shootedBullet = Instantiate(bulletPrefab, bulletStartPos.position, bulletStartPos.rotation);

        shootedBullet.GetComponent<Bullet>().damage = damage;
        shootedBullet.GetComponent<Bullet>().LifeTime = range;
    }
}

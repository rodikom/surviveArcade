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

    // Invisible fields
    private List<GameObject> ammo = new List<GameObject>();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        bulletStartPos = GameObject.Find("BulletStartPos").transform;
    }

    private void Update()
    {
        //IsBulletExist();
        //CheckDistance();
    }

    public override void Attack()
    {
        //if (ammo.Count >= magazineSize) {
        //    return;
        //}
        Debug.Log("Piu Piu");

        var shootedBullet = Instantiate(bulletPrefab, bulletStartPos.position, bulletStartPos.rotation);

        shootedBullet.GetComponent<Bullet>().damage = damage;
        shootedBullet.GetComponent<Bullet>().LifeTime = range;
        //ammo.Add(shootedBullet);
    }

    //private void IsBulletExist()
    //{
    //    for (int i = 0; i < ammo.Count; i++) {
    //        if (ammo[i].IsDestroyed()) {
    //            ammo.RemoveAt(i);
    //        }
    //    }
    //}

    //private void CheckDistance()
    //{
    //    for (int i = 0; i < ammo.Count; i++) {
    //        var distance = Vector2.Distance(transform.position, ammo[i].transform.position);

    //        if (distance >= range) {
    //            Destroy(ammo[i]);
    //            ammo.RemoveAt(i);
    //        }
    //    }
    //}
}

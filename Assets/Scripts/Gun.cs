using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField]
    private int magazineSize = 10; // Розмір магазину
    [SerializeField]
    private float fireRate = 0.5f; // Частота вистрілів
    [SerializeField] 
    private Transform muzzle; // Точка, з якої виходять кулі
    [SerializeField]
    private GameObject bulletPrefab; // Префаб кулі
    //public AudioClip shootSound; // Звук вистрілу
    //public AudioClip reloadSound; // Звук перезарядки

    //private AudioSource audioSource;
    private int currentBulletsInMagazine;
    private bool canShoot = true;

    private void Start()
    {
        currentBulletsInMagazine = magazineSize;
        //audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentBulletsInMagazine < magazineSize) {
            StartCoroutine(Reload());
        }
    }

    public override void Attack()
    {
        if (canShoot && currentBulletsInMagazine > 0) {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        currentBulletsInMagazine--;
        Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        //audioSource.PlayOneShot(shootSound);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private IEnumerator Reload()
    {
        canShoot = false;
        // Імітуємо час перезарядки
        yield return new WaitForSeconds(2f);
        currentBulletsInMagazine = magazineSize;
        //audioSource.PlayOneShot(reloadSound);
        canShoot = true;
    }

    //[SerializeField]
    //protected int fireRate = 6;
    //[SerializeField]
    //protected float range = 7f;
    //[SerializeField]
    //protected GameObject bulletPrefab;
    //[SerializeField]
    //private SpriteRenderer spriteRenderer;

    //private GameObject[] ammo = new GameObject[6];

    //private void Awake()
    //{
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    for (int i = 0; i < ammo.Length; i++) {
    //        ammo[i] = bulletPrefab;
    //    }
    //}

    //private void Update()
    //{
    //    CheckDistance();
    //}

    //public override void Attack()
    //{
    //    if (ammo.Length == 0) {
    //        return;
    //    }

    //    var bulletStartPos = transform.Find("BulletStartPos").transform.position;

    //    //bulletStartPos.y *= (spriteRenderer.flipY) ? -1 : 1;

    //    var shootedBullet = Instantiate(bulletPrefab, bulletStartPos, transform.rotation);

    //    bullets.Add(shootedBullet);
    //}

    //private void CheckDistance()
    //{
    //    List<GameObject> bulletsToRemove = new List<GameObject>();

    //    foreach (var bullet in bullets) {
    //        var distance = Vector2.Distance(transform.position, bullet.transform.position);

    //        if (distance >= range) {
    //            bulletsToRemove.Add(bullet);
    //            if (bullet != null) {
    //                Destroy(bullet);
    //            }
    //        }
    //    }

    //    // Remove the bullets outside the range after the loop
    //    foreach (var bulletToRemove in bulletsToRemove) {
    //        bullets.Remove(bulletToRemove);
    //    }
    //}
}

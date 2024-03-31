using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Unity components
    private Rigidbody2D rb;

    // Bullet stats
    [SerializeField]
    private float speed = 10f;
    public float damage = 1f;

    private float lifeTime;
    public float LifeTime {
        get {
            return lifeTime;
        }
        set {
            lifeTime = value;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }
    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &&
            collision.gameObject.TryGetComponent<DamageableCharacter>(out var damageable)
            ) {

            damageable.OnHit(damage);
            Destroy(gameObject);
        }
    }
}

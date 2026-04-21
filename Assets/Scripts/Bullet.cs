using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField]
    protected float speed = 10f;

    protected float damage = 1f;
    public float Damage
    {
        get { 
            return damage; 
        }
        set {
            damage = value;
        }
    }

    private float lifeTime;
    public float LifeTime {
        get {
            return lifeTime;
        }
        set {
            lifeTime = value;
        }
    }

    private string targetTag;
    public string TargetTag { 
        get {
            return targetTag;
        }
        set {
            targetTag = value;
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpawnService.Destroy(gameObject, lifeTime);
    }
    protected virtual void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<DamageableCharacter>(out var damageable)) {
            if (collision.gameObject.CompareTag(targetTag)) {
                damageable.OnHit(damage);
            }
            SpawnService.Destroy(gameObject);
        }
    }
}

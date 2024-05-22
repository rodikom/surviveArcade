using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : Bullet
{
    private Transform target;

    protected override void FixedUpdate()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
        rb.velocity = (target.position - transform.position).normalized * speed;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
                       collision.gameObject.TryGetComponent<DamageableCharacter>(out var damageable)
                                  )
        {

            damageable.OnHit(damage);
            Destroy(gameObject);
        }
    }
}

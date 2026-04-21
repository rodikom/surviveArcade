using UnityEngine;
public class Bone : Bullet
{
    private Transform target;

    protected override void FixedUpdate()
    {
        if (target == null)
            target = FindAnyObjectByType<PlayerController>().transform;

        rb.velocity = (target.position - transform.position).normalized * speed;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.TryGetComponent(out DamageableCharacter dmg))
        {
            dmg.OnHit(damage);
        }

        base.OnCollisionEnter2D(collision);
    }
}
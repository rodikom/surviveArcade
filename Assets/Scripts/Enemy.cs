using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float detectionDistance;
    [SerializeField]
    protected float healthPoints;

    private SpriteRenderer spriteRenderer;

    private Vector2 spawnPoint;

    public float wanderRadius;
    private Vector2 targetPosition;
    private Vector2 wanderDirection;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spawnPoint = transform.position;

        GetNewWanderTarget();
    }

    void Update()
    {
        if (healthPoints <= 0)
        {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }

        if (target != null && Vector2.Distance(transform.position, target.position) <= detectionDistance)
        {
            speed = 4f;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            FlipSprite(target.position - transform.position);
        }
        else
        {
            MoveByDefault();
        }
    }

    void MoveByDefault()
    {
        speed = 1f;
        if (Vector2.Distance(transform.position, spawnPoint) <= 0.001f)
        {
            GetNewWanderTarget();
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) <= 0.001f)
        {
            targetPosition = spawnPoint;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        FlipSprite(targetPosition - (Vector2)transform.position);
    }

    void GetNewWanderTarget()
    {
        float randomAngle = Random.Range(0f, 360f);
        wanderDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
        targetPosition = (Vector2)transform.position + wanderDirection.normalized * wanderRadius;
    }

    void FlipSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            healthPoints -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);
        }
    }
}

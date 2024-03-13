using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float moveSpeed = 10f;

    private Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void OnMove(InputValue iv)
    {
        moveDirection = iv.Get<Vector2>();
    }

    private void Movement()
    {
        rb.velocity = moveDirection * moveSpeed;

        if (moveDirection.x > 0) {
            spriteRenderer.flipX = false;
        } else if (moveDirection.x < 0) {
            spriteRenderer.flipX = true;
        }
    }
}

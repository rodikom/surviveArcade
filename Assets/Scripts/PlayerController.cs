using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Visible fields
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private Animator animator;

    // Invisible fields
    private Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void OnMove(InputValue iv)
    {
        moveDirection = iv.Get<Vector2>();

        if (moveDirection != Vector2.zero) {
            animator.Play("run_player");
        } else {
            animator.Play("idle_player");
        }
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

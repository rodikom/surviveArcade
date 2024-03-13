using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    void FixedUpdate()
    { 
        transform.position += transform.right * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}

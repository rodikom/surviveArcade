using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float healthPoints = 3f;

    private void Update()
    {
        if (healthPoints <= 0) {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            healthPoints -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);
        }
    }
}

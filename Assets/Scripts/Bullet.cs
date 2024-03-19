using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    public float damage = 1f;

    void FixedUpdate()
    { 
        transform.position += transform.right * speed * Time.fixedDeltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Visible fields
    [SerializeField]
    protected float damage;

    public virtual void Attack() { }
}

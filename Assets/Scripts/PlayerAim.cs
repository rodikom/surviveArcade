using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    private Transform aimTransform;
    [SerializeField]
    private Camera mainCamera;
    private GameObject weapon;

    void Start()
    {
        weapon = GameObject.FindWithTag("Weapon");
        aimTransform = transform.Find("Aim");
    }

    void Update()
    {
        Aiming();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private void Aiming()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        if (angle > 90f || angle < -90f) {
            weapon.GetComponent<SpriteRenderer>().flipY = true;
        } else {
            weapon.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    private void OnFire()
    {
        if (weapon != null) {
            weapon.GetComponent<Weapon>().Attack();
        }
    }
}

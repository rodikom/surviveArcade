using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    // Visible fields
    [SerializeField]
    private Transform aimTransform;
    [SerializeField]
    private Camera mainCamera;

    // Invisible fields
    private GameObject weapon;
    private GameObject player;

    void Start()
    {
        weapon = GameObject.FindWithTag("Weapon");
        player = GameObject.FindWithTag("Player");
        aimTransform = transform.Find("Aim");
    }

    void Update()
    {
        if (!PauseScreen.isPaused) {
            Aiming();
        }
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
            player.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            weapon.GetComponent<SpriteRenderer>().flipY = false;
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnFire()
    {
        if (PauseScreen.isPaused) {
            return;
        }
        if (weapon != null) {
            weapon.GetComponent<Weapon>().Attack();
        }
    }
}

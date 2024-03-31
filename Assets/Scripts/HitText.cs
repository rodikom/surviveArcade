using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitText : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 0.3f;
    [SerializeField]
    private float floatSpeed = 3f;

    private TextMeshProUGUI textMesh;
    private RectTransform rTransform;

    private float timeElapset = 0f;
    private Color color;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        rTransform = GetComponent<RectTransform>();
        rTransform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        color = textMesh.color;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        timeElapset += Time.deltaTime;

        rTransform.position += Vector3.up * floatSpeed * Time.deltaTime;
        textMesh.color = new Color(color.r, color.g, color.b, 1 - timeElapset / lifeTime);
    }
}

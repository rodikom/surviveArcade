using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private TextMeshProUGUI healthText;

    private PlayerController player;
    
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float filledAmount = player.Health / player.MaxHealth;
        healthBar.fillAmount = filledAmount;

        healthText.text = player.Health.ToString() + " / " + player.MaxHealth.ToString();
    }
}

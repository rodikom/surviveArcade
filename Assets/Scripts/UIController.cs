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
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private Image bossWaveBG;
    [SerializeField]
    private Image bossWaveImage;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private TextMeshProUGUI killedEnemyCountText;
    [SerializeField]
    private TextMeshProUGUI restorHPCount;

    private PlayerController player;

    private bool bossWaveBarCreated = false;

    public static float timerTime = 0;
    private float bossWaveTimer = 0;

    public static int killedEnemyCount = 0;


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        gameController = GameObject.FindObjectOfType<GameController>();
        timerTime = 0;
        killedEnemyCount = 0;
    }

    void Update()
    {
        float filledAmount = player.Health / player.MaxHealth;
        healthBar.fillAmount = filledAmount;

        healthText.text = player.Health.ToString() + " / " + player.MaxHealth.ToString();

        timerTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timerTime / 60f);
        int seconds = Mathf.FloorToInt(timerTime % 60f);

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;

        if (gameController.IsBossWaveActive) {
            if (!bossWaveBarCreated) {
                bossWaveBarCreated=true;
                bossWaveBG.fillAmount = 1;
            }

            bossWaveTimer += Time.deltaTime;
            bossWaveImage.fillAmount = bossWaveTimer / gameController.BossWaveDuration;
        } else {
            if (bossWaveBarCreated) {
                bossWaveBG.fillAmount = 0;
                bossWaveImage.fillAmount=0;
                bossWaveBarCreated = false;
                bossWaveTimer = 0;
            }
        }

        killedEnemyCountText.text = killedEnemyCount.ToString();

        var resHPCount = player.RestorHPCount;
        restorHPCount.text = resHPCount.ToString();
    }
}

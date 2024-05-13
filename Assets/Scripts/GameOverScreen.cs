using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI killedEnemiesCounter;

    private void Start()
    {
        var timerTime = UIController.timerTime;

        int minutes = Mathf.FloorToInt(timerTime / 60f);
        int seconds = Mathf.FloorToInt(timerTime % 60f);

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timeText.text = timerString;
        killedEnemiesCounter.text = UIController.killedEnemyCount.ToString();
    }
    public void RestartGame()
    {
        Loader.Loading(Loader.Scene.Game);
    } 

    public void LoadMainMenu()
    {
        Loader.Loading(Loader.Scene.MainMenu);
    }
}

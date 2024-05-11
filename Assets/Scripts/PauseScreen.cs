using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject optionScreen;

    public static bool isPaused;

    private void Start()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (!isPaused) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
    }
    
    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void OpenOptions()
    {
        optionScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionScreen.SetActive(false);
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Loader.Loading(Loader.Scene.MainMenu);
    }
}

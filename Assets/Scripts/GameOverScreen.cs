using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartGame()
    {
        Loader.Loading(Loader.Scene.Game);
    } 

    public void LoadMainMenu()
    {
        Loader.Loading(Loader.Scene.MainMenu);
    }
}

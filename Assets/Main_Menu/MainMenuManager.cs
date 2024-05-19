using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start Game
    public void Play()
    {
        SceneManager.LoadScene("PacMan");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    // Enter Credit Scene
    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    // Exit Game
    public void Exit()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}

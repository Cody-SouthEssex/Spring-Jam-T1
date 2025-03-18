using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private InputHandler ih;
    private GameOverController gameOverController;
    public GameObject pauseMenuUI;
    public string mainMenuSceneName = "MainMenu";
    private bool isPaused = false;

    private void Start()
    {
        ih = FindObjectOfType<InputHandler>();
        gameOverController = GetComponent<GameOverController>();
    }

    void Update()
    {
        if (ih.GetInput(Control.Escape, KeyPressType.Down))
        {
            if (isPaused && !gameOverController.isGameOver)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();

    }
}
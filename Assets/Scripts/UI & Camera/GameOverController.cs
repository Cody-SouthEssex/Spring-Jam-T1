using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private string gamePlaySceneName = "Gameplay";
    private Health playerHealth;
    public bool isGameOver = false;
    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerHealth = player.GetComponent<Health>();
            playerHealth.OnDeath += OnPlayerDeath;
        }
        if (gameOverMenu)
        { 
            gameOverMenu.SetActive(false);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        }
    }

    public void OnPlayerDeath()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        if (gameOverMenu)
        {
            gameOverMenu.SetActive(true);
        }
    }
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
    private void Restart()
    {
        SceneManager.LoadScene(gamePlaySceneName);
    }
}

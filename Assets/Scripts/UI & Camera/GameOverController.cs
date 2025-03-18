using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private Health playerHealth;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerHealth = player.GetComponent<Health>();
            playerHealth.OnDeath += OnPlayerDeath;
        }
    }

    public void OnPlayerDeath()
    {
        isGameOver = true;
        Time.timeScale = 0f;
    }
}

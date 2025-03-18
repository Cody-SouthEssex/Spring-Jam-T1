using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameplay;
    public string introCutscene;

    public void LoadScene(bool playCutscene)
    {
        if (playCutscene)
        {
            SceneManager.LoadScene(introCutscene);
        }
        else
        {
            SceneManager.LoadScene(gameplay);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}

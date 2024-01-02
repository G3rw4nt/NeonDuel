using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameCore", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameCoreVsPlayer", LoadSceneMode.Single);
    }

    public void StartCPUGame()
    {
        SceneManager.LoadScene("GameCoreVsCPU", LoadSceneMode.Single);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

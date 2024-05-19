using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void StartMultiplayer()
    {
        SceneLoader.LoadScene(SceneNames.LoadingScreen);
    }

    public void Options()
    {
        Debug.Log("Implement...");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

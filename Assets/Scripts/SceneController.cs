using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneNames
{
    public const string MainMenu = "MainMenu";
    public const string LoadingScreen = "LoadingScreen";
    public const string Lobby = "Lobby";
    public const string Room = "Room";
    public const string Game = "Game";
}

public static class SceneLoader
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public static class LevelManager {

	public static void NextLevel()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

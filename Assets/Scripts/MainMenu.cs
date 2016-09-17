using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject menu_main;
    public GameObject menu_guide;

    //Kan geen public Scene field hebben in de editor
    public string level1;
    public string tutorial;

    public void ShowMainMenu() {
        print("Main Menu");

        menu_main.SetActive(true);
        menu_guide.SetActive(false);
    }

    public void ShowManual() {
        print("Manual");

        menu_main.SetActive(false);
        menu_guide.SetActive(true);
    }

    public void LoadTutorial() {
        //SceneManager.LoadScene(tutorial.ToString());
        SceneManager.LoadScene(tutorial);
    }

    public void LoadStartLevel() {
        //SceneManager.LoadScene(level1.ToString());
        SceneManager.LoadScene(level1);
    }
    public void Quit() {
        Application.Quit();
    }

    //----------
}

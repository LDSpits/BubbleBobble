using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {

	public static UIManager current;

	public GameObject scoreGO;  //GO = GameObject
	public GameObject highScoreGO;
	public GameObject lifeGO;
	public GameObject GameOverGO;

	Text scoreText;
	Text highScoreText;
	Text lifeText;

    //Pauzemenu


    public GameObject pause_panel;
    public GameObject pause_main;
    public GameObject pause_guide;

    private bool pauseMenuIsOn;
    
    public void PauseMenuSwitch() {
        if (pauseMenuIsOn) {
            Resume();
        }
        else {
            OpenPauseMenu();
        }
    }

    //----

	public static string ScoreText {
		set{
			current.scoreText.text = value;
		}
	}

	public static string HighScoreText {
		set{
			current.highScoreText.text = value;
		}
	}

	public static string LifeText {
		set{
			current.lifeText.text = value;
		}
	}
	public static bool SetUIGameOverState{
		set{
			current.GameOverGO.SetActive (value);
		}
	}

    void Start() {
        current = this;
        scoreText = scoreGO.GetComponent<Text>();
        highScoreText = highScoreGO.GetComponent<Text>();
        lifeText = lifeGO.GetComponent<Text>();
        GameOverGO.SetActive(false);
        Resume();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenuSwitch();
        }
    }
    //Pauzemenu

    public void OpenPauseMenu() {
        print("Pause menu");

        pause_panel.SetActive(true);
        pause_main.SetActive(true);
        pause_guide.SetActive(false);
        pauseMenuIsOn = true;
    }

    public void Resume() {
        print("Resume");

        pause_panel.SetActive(false);
        pause_main.SetActive(false);
        pause_guide.SetActive(false);
        pauseMenuIsOn = false;
    }
    public void GoToManual() {
        print("Manual");

        pause_panel.SetActive(true);
        pause_main.SetActive(false);
        pause_guide.SetActive(true);
    }
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit() {

    }

    //----------
}

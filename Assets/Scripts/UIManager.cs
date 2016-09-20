﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {

	private static UIManager instance;

    //BubbleLives Velden
    [SerializeField] private GameObject bubbleLivesP1;
    [SerializeField] private GameObject bubbleLivesP2;

    //Text refs
    [SerializeField] private Text highScoreTitle, highScoreText;
    [SerializeField] private Text scoreTitleP1, scoreTextP1; //Score P1
	[SerializeField] private Text scoreTitleP2, scoreTextP2; //Score P2

    //GameOver
    public GameObject gameOverGO;

    //Pauzemenu
    public GameObject pause_panel, pause_main, pause_guide;

    private bool pauseMenuIsOn;
    
    public void PauseMenuToggle() {
        if (pauseMenuIsOn) {
            Resume();
        }
        else {
            OpenPauseMenu();
        }
    }
    //----

    //Game Paused
    public static bool isPaused {
        get { return instance.pauseMenuIsOn; }
    }

    /* Player 1 */
    //Lives Player 1 
    public static void SetLivesP1(int lives) {
        DisplayLives(instance.bubbleLivesP1, lives);
    }

    //Score title Player 1
    public static string ScoreTitleP1 {
		set{ instance.scoreTitleP1.text = value; }
	}

    //Score text Player 1
    public static void UpdateScoreP1(long score) {
        instance.scoreTextP1.text = AddZeros(score);
    }

    /* Player 2 */
    //Lives Player 12
    public static void SetLivesP2(int lives) {
        DisplayLives(instance.bubbleLivesP2, lives);
    }

    //Score title Player 2
    public static string ScoreTitleP2 {
        set { instance.scoreTitleP2.text = value; }
    }

    //Score text Player 2
    public static void UpdateScoreP2(long score) {
        instance.scoreTextP2.text = AddZeros(score);
    }

    //Highscore Title
    public static string HighScoreTitle {
        set { instance.highScoreTitle.text = value; }
    }

    //Highscore Text
    public static void UpdateHighscore(long score)
    {
        instance.highScoreText.text = AddZeros(score);
    }
    //Game Over tonen
    public static bool SetUIGameOverState{
		set{ instance.gameOverGO.SetActive (value); }
	}

    void Awake() {
        instance = this;
    }

    void Start() {
        gameOverGO.SetActive(false);
        Resume();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenuToggle();
        }
    }

    
    private static void DisplayLives(GameObject bubbleLives, int lives) { //Werkt voor nu alleen nog voor speler 1 !!!!
        for(int i=0; i<bubbleLives.transform.childCount; i++) {
            if (i < lives)
                bubbleLives.transform.GetChild(i).gameObject.SetActive(true); //Bubbel tonen
            else
                bubbleLives.transform.GetChild(i).gameObject.SetActive(false); //Bubbel verbergen
        }
    }

    private static string AddZeros(long score) {
        //Word gebruikt om de score als tekst op het scherm te laten verschijnen
        //Voorbeeld: het getal 64 wordt '000064'
        string result = "";
        string scoreAsText = score.ToString();

        for(int i=0; i<(6-scoreAsText.Length); i++) {
            result += "0";
        }

        return result + scoreAsText;
    }

    
    //Pauzemenu
    public void OpenPauseMenu() {
        print("Pause menu");

        pause_panel.SetActive(true);
        pause_main.SetActive(true);
        pause_guide.SetActive(false);
        pauseMenuIsOn = true;
        AudioManager.PauseBackgroundMusic();
        Time.timeScale = 0;
    }

    public void Resume() {
        print("Resume");

        pause_panel.SetActive(false);
        pause_main.SetActive(false);
        pause_guide.SetActive(false);
        pauseMenuIsOn = false;
        AudioManager.PlayBackgroundMusic();
        Time.timeScale = 1;
    }
    public void GoToManual() {
        print("Manual");

        pause_panel.SetActive(true);
        pause_main.SetActive(false);
        pause_guide.SetActive(true);
    }
    public void Restart() {
        LevelManager.RestartLevel();
    }
    public void Quit() {
        LevelManager.Quit();
    }

    //----------
}

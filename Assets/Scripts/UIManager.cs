using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private static UIManager instance;

    //BubbleLives Velden
    [SerializeField] private GameObject bubbleLivesP1, bubbleLivesP2, bubbleLivesP3, bubbleLivesP4;

    //Tekstvelden
    [SerializeField] private Text scoreTextP1, scoreTextP2, scoreTextP3, scoreTextP4, highScoreText;

    //GameOver
    public GameObject gameOverGO;

    //Pauzemenu
    public GameObject pause_panel, pause_main, pause_guide;

    private bool pauseMenuIsOn;

    public void PauseMenuToggle()
    {
        if (pauseMenuIsOn) {
            SetUIState(UIState.None);
        }
        else {
            SetUIState(UIState.Pause);
        }
    }
    //----

    //Game Paused
    public static bool isPaused {
        get { return instance.pauseMenuIsOn; }
    }

    //Update Lives
    public static void UpdateLives(PlayerID id, int lives)
    {
        switch (id) {
            case PlayerID.player1: DisplayLives(instance.bubbleLivesP1, lives); break;
            case PlayerID.player2: DisplayLives(instance.bubbleLivesP2, lives); break;
            case PlayerID.player3: DisplayLives(instance.bubbleLivesP3, lives); break;
            case PlayerID.player4: DisplayLives(instance.bubbleLivesP4, lives); break;
        }
    }

    //Update score
    public static void UpdateScore(PlayerID id, long score)
    {
        switch (id) {
            case PlayerID.player1: instance.scoreTextP1.text = AddZeros(score);  break;
            case PlayerID.player2: instance.scoreTextP2.text = AddZeros(score); break;
            case PlayerID.player3: instance.scoreTextP3.text = AddZeros(score); break;
            case PlayerID.player4: instance.scoreTextP4.text = AddZeros(score); break;
        }
    }

    //Highscore Text
    public static void UpdateHighscore(long score)
    {
        instance.highScoreText.text = AddZeros(score);
    }

    //GUI State zetten

    public static void SetUIState(UIState state)
    {
        switch (state) {
            case UIState.None:
                instance.StateHelper(false, false, false);
                instance.pauseMenuIsOn = false;
                AudioManager.PlayBackgroundMusic();
                Time.timeScale = 1;
                break;
            case UIState.GameOver:
                instance.StateHelper(false, false, true);
                instance.pauseMenuIsOn = false;
                AudioManager.PauseBackgroundMusic();
                Time.timeScale = 0;
                break;
            case UIState.Pause:
                instance.StateHelper(true, false, false);
                instance.pauseMenuIsOn = true;
                AudioManager.PauseBackgroundMusic();
                Time.timeScale = 0;
                break;
            case UIState.Manual:
                instance.StateHelper(false, true, false);
                instance.pauseMenuIsOn = true;
                AudioManager.PauseBackgroundMusic();
                Time.timeScale = 0;
                break;
        }
    }
    
    private void StateHelper( bool main, bool guide, bool over)
    {
        instance.pause_panel.SetActive(main == true || guide == true); 
        instance.pause_main.SetActive(main);
        instance.pause_guide.SetActive(guide);
        instance.gameOverGO.SetActive(over);
    }

    void Awake() {
        instance = this;
    }

    void Start() {
        SetUIState(UIState.None);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenuToggle();
        }
    }

    private static void DisplayLives(GameObject bubbleLives, int lives) {
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

    public void Restart() {
        GameManager.RestartLevel();
    }
    public void Quit() {
        GameManager.Quit();
    }

    //----------
}

public enum UIState
{
    Pause,
    GameOver,
    Manual,
    None
}

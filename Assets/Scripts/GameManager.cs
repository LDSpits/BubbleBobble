using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    private readonly float RESPAWN_TIME = 3;
    private readonly float INVINCIBLITY_TIME = 3;

    private List<PlayerData> playerDataList = new List<PlayerData>(4);

    private int enemies = 0;
    private float seconds;

    private int highScore; 

    private void Awake(){

        if (instance) {
            DestroyImmediate(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    private void Start()
    {
        instance = this;
        AudioManager.PlayBackgroundMusic();
        highScore = PlayerPrefs.GetInt("HighScore");
        UIManager.UpdateHighscore(highScore);
        SceneManager.sceneLoaded += OnLoadScene;
    }
    //Als een level wordt geladen
    private void OnLoadScene(Scene arg0, LoadSceneMode arg1)
    {
        //Update de levens en score
        foreach(PlayerData data in playerDataList) {
            UIManager.UpdateScore(data.ID, data.Score);
            UIManager.UpdateLives(data.ID, data.Lives);
        }

        UIManager.UpdateHighscore(instance.highScore);
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(enemies == 0)
        {
            seconds += Time.deltaTime;
            if(seconds > 5)
            {
                seconds = 0;
                NextLevel();
            }  
        }
    }

    public static void AddScore(PlayerID id, int amount)
    {
        instance.playerDataList[(int)id].Score += amount;

        int newScore = instance.playerDataList[(int)id].Score;
        UIManager.UpdateScore(id, newScore);

        //Als de score hoger is dan de highscore, vernieuwen we de highscore & UI
        if (newScore > instance.highScore) {
            instance.highScore = newScore;
            UIManager.UpdateHighscore(newScore);
        }
    }

    public static void DecreaseLife(PlayerID id)
    {
        //Maak de speler dood
        PlayerData p = instance.playerDataList[(int)id];
        p.Player.Die();
        UIManager.UpdateLives(id, --p.Lives);

        //Controleer of er spelers met levens over zijn
        int totalLives = 0;
        foreach (PlayerData data in instance.playerDataList) {
            totalLives += data.Lives;
        }

        if (p.Lives > 0) //De speler heeft levens over, respawn hem
            instance.StartCoroutine(instance.RespawnRoutine(p.Player));

        if (totalLives <= 0) {
            UIManager.SetUIState(UIState.GameOver); 
        }
            
    }

    IEnumerator RespawnRoutine(Player playerComp)
    {
        //TODO: check of de level gewisseld is
        yield return new WaitForSeconds(RESPAWN_TIME);
        playerComp.Respawn();
        yield return new WaitForSeconds(INVINCIBLITY_TIME);
        playerComp.Mortalize();
    }

    public static PlayerID RegisterPlayer(Player player)
    {
        PlayerID id = (PlayerID)instance.playerDataList.Count;
        instance.playerDataList.Add(new PlayerData(id, player));
        return id;
    }

    public static void NextLevel()
    {
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
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

    internal class PlayerData
    {
        private Player _player;
        private int _lives = 5;
        private int _score = 0;

        private PlayerID id = PlayerID.player1;

        public Player Player {
            get { return _player; }
            set { _player = value; }
        }

        public int Lives
        {
            get{ return _lives; }
            set { _lives = value; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public PlayerID ID {
            get { return id; }
            set { id = value; }
        }

        public PlayerData(PlayerID id, Player player)
        {
            this.id = id;
            Player = player;
        }
    }

}

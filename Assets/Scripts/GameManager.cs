using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    private int enemies = 0;
    private float seconds;

    [SerializeField]
    private int p1Lives = 5;

    [SerializeField]
    private int p2Lives = 5;

    private int p1Score = 0;
    private int p2Score = 0;

    private int highScore; 

    void Awake(){

        if (instance) {
            DestroyImmediate(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    // Use this for initialization
    void Start () {
        instance = this;
        AudioManager.PlayBackgroundMusic();
        highScore = PlayerPrefs.GetInt("HighScore");
        UIManager.UpdateHighscore(highScore);
    }

   void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(enemies == 0)
        {
            seconds += Time.deltaTime;
            if(seconds > 5)
            {
                LevelManager.NextLevel();
                seconds = 0;
            }
                
            
        }
    }

    public static void setScore(Players.player player, int amount)
    {
        if (player == Players.player1)
        {
            print(instance.p1Score);
            instance.p1Score += amount;
            UIManager.UpdateScoreP1(instance.p1Score);

            if(instance.p1Score > instance.highScore)
            {
                UIManager.UpdateHighscore(instance.p1Score);
                PlayerPrefs.SetInt("HighScore", instance.p1Score);
            }
        }else
        {
            instance.p2Score += amount;
            UIManager.UpdateScoreP2(instance.p2Score);

            if(instance.p2Score > instance.highScore)
            {
                UIManager.UpdateHighscore(instance.p2Score);
                PlayerPrefs.SetInt("HighScore", instance.highScore);
            }
        }
    }

    public static void DecreaseLife(Players.player player)
    {
        if(player == Players.player1)
        {
            instance.p1Lives -= 1;
            UIManager.SetLivesP1(instance.p1Lives);
        }else
        {
            instance.p2Lives -= 1;
            UIManager.SetLivesP2(instance.p2Lives);
        }

        if (instance.p1Lives <= 0)
        {
            UIManager.SetUIGameOverState = true;
        }
    }

}



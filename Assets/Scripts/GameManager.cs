using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    private int p1Lives = 3;
    private int p2Lives = 3;

    private int p1Score = 0;
    private int p2Score = 0;

    private int highScore; 

    // Use this for initialization
    void Start () {
        instance = this;
        AudioManager.PlayBackgroundMusic();
        highScore = PlayerPrefs.GetInt("highScore");
    }
	
	// Update is called once per frame
	void Update () {
        if (p1Lives <= 0)
        {
            UIManager.SetUIGameOverState = true;
        }
	}

    public static void setScore(players player, int amount)
    {
        if (player == players.player1)
        {
            print(instance.p1Score);
            instance.p1Score += amount;
            UIManager.UpdateScoreP1(instance.p1Score);

            if(instance.p1Score > instance.highScore)
            {
                UIManager.UpdateHighscore(instance.p1Score);
                PlayerPrefs.SetInt("p1Highscore", instance.p1Score);
                PlayerPrefs.Save();
            }
        }else
        {
            instance.p2Score += amount;
            UIManager.UpdateScoreP2(instance.p2Score);

            if(instance.p2Score > instance.highScore)
            {
                UIManager.UpdateHighscore(instance.p2Score);
                PlayerPrefs.SetInt("highScore", instance.highScore);
                PlayerPrefs.Save();
            }
        }

    }

    public static void DecreaseLife(players player)
    {
        if(player == players.player1)
        {
            instance.p1Lives -= 1;
        }else
        {
            instance.p2Lives -= 1;
        }
    }

    public enum players
    {
        player1,
        player2
    }

}

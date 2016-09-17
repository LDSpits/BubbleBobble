using UnityEngine;
using System.Collections;

public static class GameManager {

    private static int score = 0;
    private static int lives = 3;
    private static int highscore = PlayerPrefs.GetInt("highScore");

    public static void SetScore(int amount)
    {
        score += amount;
        UIManager.UpdateScoreP1(score);

        if(score > highscore)
        {
            highscore = score;
            UIManager.HighScoreText = string.Format("high score : {0}", highscore);
            PlayerPrefs.SetInt("highScore", highscore);
            PlayerPrefs.Save();
        }
    }

    public static void DecreaseLife()
    {
        lives -= 1;
    }

}

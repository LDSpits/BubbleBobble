using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager current;

	public GameObject scoreGO;
	public GameObject highScoreGO;
	public GameObject lifeGO;
	public GameObject GameOverGO;

	Text scoreText;
	Text highScoreText;
	Text lifeText;

	public static string ScoreText {
		get{
			return current.scoreText.text;
		}
		set{
			current.scoreText.text = value;
		}
	}

	public static string HighScoreText {
		get{
			return current.highScoreText.text;
		}
		set{
			current.highScoreText.text = value;
		}
	}

	public static string LifeText {
		get{
			return current.lifeText.text;
		}
		set{
			current.lifeText.text = value;
		}
	}
	public static bool SetUIGameOverState{
		set{
			current.GameOverGO.SetActive (value);
		}
	}
	void Start () {
		current = this;
		scoreText = scoreGO.GetComponent<Text> ();
		highScoreText = highScoreGO.GetComponent<Text> ();
		lifeText = lifeGO.GetComponent<Text> ();
		GameOverGO.SetActive (false);
	}


}

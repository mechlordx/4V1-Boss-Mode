using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {

	score Scores;
	player_controls PC;

	public Text P1Score;
	public Text P2Score;
	public Text P3Score;
	public Text P4Score;

	public Button QuitButton;
	public Button MenuBotton;

	int playerNumber = 0;

	// Use this for initialization
	void Start () {
		Scores = GameObject.Find("GameController").GetComponent<score> ();
		PC = GameObject.Find ("GameController").GetComponent<player_controls> ();

		P1Score.text = ("Player 1: "+ (int)Scores.scores[0]);
		P2Score.text = ("Player 2: "+ (int)Scores.scores[1]);
		P3Score.text = ("Player 3: "+ (int)Scores.scores[2]);
		P4Score.text = ("Player 4: "+ (int)Scores.scores[3]);

	}
	
	// Update is called once per frame
	void Update () {
	
		if(PC.getButton(playerNumber, 2, 1))
			Application.LoadLevel("splash");

		if(PC.getButton(playerNumber, 1, 1))
			Application.Quit();

	}

	public void ExitGame (){
		Application.Quit();
	}

	public void GoToMainMenu () {
		Application.LoadLevel("splash");
	}
}

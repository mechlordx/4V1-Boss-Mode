using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI_Manager : MonoBehaviour {
	public Text P1Score;
	public Text P2Score;
	public Text P3Score;
	public Text P4Score;
	public Text Clock;

	int timer;

	string timerString = "42";

	score SPage;

	// Use this for initialization
	void Start () {
		SPage = GameObject.FindGameObjectWithTag("GameController").GetComponent<score> ();

	}
	
	// Update is called once per frame
	void Update () {
		timer = (int) SPage.timer;
		timerString = timer.ToString();

		Clock.text = timerString;
		P1Score.text = ("Player 1: "+ (int) SPage.scores[0]);
		P2Score.text = ("Player 2: "+ (int) SPage.scores[1]);
		P3Score.text = ("Player 3: "+ (int) SPage.scores[2]);
		P4Score.text = ("Player 4: "+ (int) SPage.scores[3]);
	}

	void OnCanvas(){


	}
}

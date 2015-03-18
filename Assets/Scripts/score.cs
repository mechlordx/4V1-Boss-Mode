using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {

	public float timer;
	public float maxtimer = 90f;
	bool paused = false;
	bool started = false;
	public float[] scores;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(started)
		{
			float deltatime = Time.deltaTime;
			if(!paused)
			{
				timer += -deltatime;
				int playerNumber = GameObject.Find ("Boss").GetComponent<boss_control>().playerNumber;
				if(playerNumber!=-1)
					scores[playerNumber] += deltatime;
				if(timer<0f)
				{
					Debug.Log ("Scores: " + "Player 1: " + scores[0]
					           + " Player 2: " + scores[1]
					           + " Player 3: " + scores[2]
					           + " Player 4: " + scores[3]);
					started = false;
					GameObject.Find ("GameController").GetComponent<loader>().loadScene(5);//changed from 0 to 5 for my score screen
				}
			}
		}
	}

	public void begin()
	{
		scores = new float[4];
		for(int x=0;x<scores.Length;x++)
			scores[x] = 0f;
		timer = maxtimer;
		started = true;
		paused = false;
	}

	public void pause()
	{
		paused = true;
	}

	public void unpause()
	{
		paused = false;
	}
}

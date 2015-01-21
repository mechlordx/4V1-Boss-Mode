using UnityEngine;
using System.Collections;

public class healthbars : MonoBehaviour {
	public float bossHealth = 1000;
	public float P1Health = 100;
	public float P2Health = 100;
	public float P3Health = 100;
	public float P4Health = 100;
	public float BossbarDisplay = 0;
	public float P1BarDisplay = 0;
	public float P2BarDisplay = 0;
	public float P3BarDisplay = 0;
	public float P4BarDisplay = 0;

	GameObject daBoss;


	Vector2 BossPos = new Vector2(80, 40);
	Vector2 BossSize = new Vector2(1000,20);
	Vector2 P1Pos = new Vector2 (100, 800);
	Vector2 P1Size = new Vector2 (200, 20);
	Vector2 P2Pos  = new Vector2 (320, 800);
	Vector2 P2Size = new Vector2 (200, 20);
	Vector2 P3Pos  = new Vector2 (540, 800);
	Vector2 P3Size = new Vector2 (200, 20);
	Vector2 P4Pos = new Vector2 (760, 800);
	Vector2 P4Size = new Vector2 (200, 20);


	Texture2D BossprogressBarEmpty;
	Texture2D BossprogressBarFull;
	Texture2D P1ProgressBarEmpty;
	Texture2D P1ProgressBarFull;
	Texture2D P2ProgressBarEmpty;
	Texture2D P2ProgressBarFull;
	Texture2D P3ProgressBarEmpty;
	Texture2D P3ProgressBarFull;
	Texture2D P4ProgressBarEmpty;
	Texture2D P4ProgressBarFull;

	// Use this for initialization
	void Start () {
		daBoss = GameObject.FindGameObjectWithTag("Boss");
	}
	
	// Update is called once per frame
	void Update () {
		BossbarDisplay = bossHealth/1000;
		P1BarDisplay = P1Health/100;
		P2BarDisplay = P2Health/100;
		P3BarDisplay = P3Health/100;
		P4BarDisplay = P4Health/100;
	}


	void OnGUI() {
		//Boss Health
		//draw the background:
		GUI.BeginGroup(new Rect(BossPos.x, BossPos.y, BossSize.x, BossSize.y));
		GUI.Box(new Rect(0,0, BossSize.x, BossSize.y), BossprogressBarEmpty);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, BossSize.x * BossbarDisplay, BossSize.y));
		GUI.Box(new Rect(0,0, BossSize.x, BossSize.y), BossprogressBarFull);
		GUI.EndGroup();
		GUI.EndGroup();

		if(bossHealth <= 0)
			Destroy(daBoss);


		//P1
		//draw the background:
		GUI.BeginGroup(new Rect(P1Pos.x, P1Pos.y,  P1Size.x, P1Size.y));
		GUI.Box(new Rect(0,0, P1Size.x, P1Size.y), P1ProgressBarEmpty);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, P1Size.x * P1BarDisplay, P1Size.y));
		GUI.Box(new Rect(0,0, P1Size.x, P1Size.y), P1ProgressBarFull);
		GUI.EndGroup();
		GUI.EndGroup();


		//P2
		//draw the background:
		GUI.BeginGroup(new Rect(P2Pos.x, P2Pos.y,  P2Size.x, P2Size.y));
		GUI.Box(new Rect(0,0, P2Size.x, P2Size.y), P2ProgressBarEmpty);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, P2Size.x * P2BarDisplay, P2Size.y));
		GUI.Box(new Rect(0,0, P2Size.x, P2Size.y), P2ProgressBarFull);
		GUI.EndGroup();
		GUI.EndGroup();


		//P3
		//draw the background:
		GUI.BeginGroup(new Rect(P3Pos.x, P3Pos.y,  P3Size.x, P3Size.y));
		GUI.Box(new Rect(0,0, P3Size.x, P3Size.y), P3ProgressBarEmpty);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, P3Size.x * P3BarDisplay, P3Size.y));
		GUI.Box(new Rect(0,0, P3Size.x, P3Size.y), P3ProgressBarFull);
		GUI.EndGroup();
		GUI.EndGroup();


		//P4
		//draw the background:
		GUI.BeginGroup(new Rect(P4Pos.x, P4Pos.y,  P4Size.x, P4Size.y));
		GUI.Box(new Rect(0,0, P4Size.x, P4Size.y), P4ProgressBarEmpty);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, P4Size.x * P4BarDisplay, P4Size.y));
		GUI.Box(new Rect(0,0, P4Size.x, P4Size.y), P4ProgressBarFull);
		GUI.EndGroup();
		GUI.EndGroup();

	}

}

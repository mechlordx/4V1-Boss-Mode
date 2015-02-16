using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boss4 : MonoBehaviour {
	int iter = 0;
	public int playerNumber = -1;
	string pNo;
	player_controls controlsRef;
	float cooldown = 0f;
	float maxcooldown = 0.35f;

	GameObject bullet;
	GameObject slowbullet;
	GameObject scythe;
	GameObject slowAoe;
	GameObject playerA;
	GameObject playerB;
	GameObject playerC;
	GameObject a;
	GameObject b;
	GameObject[] Players = new GameObject[3];

	Color transperency;
	Color opaque;
	Vector3 TempPos;

	player_move pmA;
	player_move pmB;
	player_move pmC;

	// Use this for initialization
	void Start () {
		transperency.a = 0.2f;
		opaque.a = 0.2f;
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		bullet = (GameObject)Resources.Load ("Prefabs/forcebullet");
		slowbullet = (GameObject)Resources.Load ("Prefabs/slowbullet");
		scythe = (GameObject) Resources.Load ("Prefabs/SyctheAttack");
		slowAoe = (GameObject) Resources.Load("Prefabs/slowAOE");
		pNo = playerNumber.ToString();
		iter = 0;
		/*
		foreach(GameObject P in GameObject.FindGameObjectsWithTag("Player"))
		{
			if(!P.name.Contains(pNo)){
				Players[iter] = P;
				iter++;
			}
		}

		playerA = Players[0];
		playerB = Players[1];
		playerC = Players[2];

		pmA = playerA.GetComponent<player_move> ();
		pmB = playerB.GetComponent<player_move> ();
		pmC = playerC.GetComponent<player_move> ();
		*/

	}
	
	// Update is called once per frame
	void Update () {
		if(playerNumber == -1)
			playerNumber = transform.parent.GetComponent<boss_control> ().playerNumber;
		cooldown += -Time.deltaTime;

		 
		if(controlsRef.getButton(playerNumber, 0))
		{
			if(cooldown<0f)
			{

					cooldown = maxcooldown;
					GameObject a = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
					a.transform.parent = transform;
					a.transform.localEulerAngles = Vector3.zero;
					a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
					a.transform.parent = null;


			}
		}
		

		else if(controlsRef.getButton(playerNumber, 1 , 1))
		{
			if(cooldown<0f)
			{

				if(a != null){
					cooldown = maxcooldown;
					a = (GameObject) GameObject.Instantiate(slowAoe, transform.position, Quaternion.identity);
					a.renderer.material.color = transperency;
					a.transform.parent = transform;
					a.transform.localEulerAngles = Vector3.zero;
					//a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
					//a.GetComponent<bullet>().force = 30000f;
					//a.GetComponent<bullet>().speed = 23f;
				}
				else{
					cooldown = maxcooldown;
					a.transform.forward = Vector3.forward;
				}
			}
		}
		else if(controlsRef.getButton(playerNumber, 1, -1)){
			cooldown = maxcooldown;
			b =(GameObject) GameObject.Instantiate(slowAoe, a.transform.position, Quaternion.identity);
			Destroy(a);

		}
		else if(controlsRef.getButton(playerNumber, 2))
		{
			if(cooldown<0f)
			{
				cooldown = maxcooldown;
				GameObject a = (GameObject) GameObject.Instantiate(scythe, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = (Vector3.zero + new Vector3(0, 0, 90f));
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
		}/*
		//Checks to see if 2 targets have been hit with the swap and switches their places
		if(pmA.swap){
			if(pmB.swap)
				SwapPlaces(playerA, playerB);
			else if(pmC.swap)
				SwapPlaces(playerA, playerC);
		}
		else if(pmB.swap)
			if(pmC.swap)
				SwapPlaces(playerB, playerC);
		*/
	}
	
	public void SwapPlaces (GameObject a, GameObject b){
		TempPos = a.gameObject.transform.position;
		a.gameObject.transform.position = b.gameObject.transform.position;
		b.gameObject.transform.position = TempPos;

		pmA.swap = false;
		pmB.swap = false;
		pmC.swap = false;
	}
}

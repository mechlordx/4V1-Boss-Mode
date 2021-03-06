﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boss4 : MonoBehaviour {
	public AudioSource AS;

	int iter = 0;
	public int playerNumber = -1;
	string pNo;
	player_controls controlsRef;
	float cooldown = 0f;
	float maxcooldown = 0.6f; // cooldown stat
	float AOEcooldown = 0f;
	float AOEmaxcooldown = 10f; // cooldown stat
	float cooldown2 = 0f;
	float maxcooldown2 = 0.6f; // cooldown stat
	float internalCooldown = 0f;
	float tempVec;

	GameObject bullet;
	GameObject slowbullet;
	GameObject scythe;
	GameObject slowAoe;
	GameObject AoeGhost;
	GameObject playerA;
	GameObject playerB;
	GameObject playerC;
	GameObject a;
	GameObject b;
	GameObject[] Players = new GameObject[3];

	Color transperency;
	Color opaque;
	Vector3 TempPos;
	Vector3 AOEmoveVec;

	player_move pmA;
	player_move pmB;
	player_move pmC;

	// Use this for initialization
	void Awake () {
		AS = GameObject.Find ("GameController").GetComponent<AudioSource> ();
		transperency.a = 0.5f;
		opaque.a = 0.2f;
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		bullet = (GameObject)Resources.Load ("Prefabs/forcebullet");
		slowbullet = (GameObject)Resources.Load ("Prefabs/slowbullet");
		//scythe = (GameObject) Resources.Load ("Prefabs/projectile");
		scythe = (GameObject) Resources.Load ("Prefabs/SyctheAttack");
		slowAoe = (GameObject) Resources.Load("Prefabs/slowAOE");
		AoeGhost = (GameObject) Resources.Load ("Prefabs/slowAOE");
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
		float timeFactor = transform.parent.gameObject.GetComponent<boss_control> ().cooldownFactor;
		if(playerNumber == -1)
			playerNumber = transform.parent.GetComponent<boss_control> ().playerNumber;
		cooldown += -(Time.deltaTime*timeFactor);
		cooldown2 += -(Time.deltaTime * timeFactor);
		AOEcooldown -= (Time.deltaTime*timeFactor);
		internalCooldown += -Time.deltaTime;

		//getbutton states 1 = down, -1 = up, 0 = getkey
		 
		if(controlsRef.getButton(playerNumber, 0))
		{
			if(cooldown<0f && internalCooldown<0f)
			{
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[3], 1f);
				cooldown = maxcooldown;
				internalCooldown = 0.45f; // cooldown stat
				GameObject a = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;


			}
		}
		

		else if(controlsRef.getButton(playerNumber, 1 , 0))
		{
			if(a == null){
				if(AOEcooldown<0f && internalCooldown<0f){
					a = (GameObject) GameObject.Instantiate(AoeGhost, transform.position, transform.rotation);
					a.transform.position = new Vector3(transform.position.x,0.55f, transform.position.z);
					//a.renderer.material.color = transperency;
					a.transform.parent = transform;
					a.transform.localEulerAngles = Vector3.zero;
					a.transform.localPosition = a.transform.parent.transform.localPosition;
				}
			}

			else{
				if(AOEcooldown<0f && internalCooldown<0f){
					a.transform.position = new Vector3(a.transform.position.x, 0.55f, a.transform.position.z);
					AOEmoveVec = a.transform.position;
					AOEmoveVec += transform.forward;
					a.transform.position = AOEmoveVec;
				}
			}
		}
	
		else if(controlsRef.getButton(playerNumber, 1, -1)){
			if(AOEcooldown<0f && internalCooldown<0f){
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[10], 1f);
				GameObject.Instantiate(slowAoe, a.transform.position, Quaternion.identity);
				Destroy(a);
				AOEcooldown = AOEmaxcooldown;
				internalCooldown = 0.45f; // cooldown stat
			}
		}
		else if(controlsRef.getButton(playerNumber, 2))
		{
			if(cooldown2<0f && internalCooldown<0f)
			{
				//scythe.transform.localEulerAngles = Vector3.zero + new Vector3(180,0,0);
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[5], 1f);
				cooldown2 = maxcooldown2;
				internalCooldown = 0.45f; // cooldown stat
				GameObject a = (GameObject) GameObject.Instantiate(scythe, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero + new Vector3(0,0,90);
				//a.transform.localEulerAngles = Vector3.zero;
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

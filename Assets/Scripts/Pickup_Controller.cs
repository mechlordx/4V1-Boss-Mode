﻿using UnityEngine;
using System.Collections;

public class Pickup_Controller : MonoBehaviour {
	newcockpit NC;
	boss_forms BF;
	player_move PM;

	public GameObject HoldingPlayer;

	public int PickupType;

	float timer = 0;
	float timerReset = 0.5f;

	public bool canPickup = true;

	// Use this for initialization
	void Start () {
		BF = GameObject.Find ("Boss").GetComponent<boss_forms>();
		NC = GameObject.Find ("Cockpit").GetComponent<newcockpit>();

		//determined by the pickup dropper
		if(PickupType == 1)
			gameObject.GetComponent<Renderer>().material.color = Color.magenta;
		if(PickupType == 2)
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		if(PickupType == 3)
			gameObject.GetComponent<Renderer>().material.color = Color.black;
		if(PickupType == 4)
			gameObject.GetComponent<Renderer>().material.color = Color.grey;
	}
	
	// Update is called once per frame
	void Update () {
		//If the pickup is set to false (so when a player drops it as of now) it is turned off for a half second
		//this way the player doesn't automatically pick it right back up
		if(!canPickup)
			timer = timerReset;

		if(timer > 0)
			timer -= Time.deltaTime;

		else if(!canPickup)
			canPickup = true;

	}

	void OnTriggerEnter (Collider col) {
		if(col.gameObject.tag == "Player"){
			print("YOU GOT ME");
			PM = col.GetComponent<player_move>();
			PM.curPickups.Add(PickupType);
			boss_control BC = BF.gameObject.GetComponent<boss_control>();

			BC.applyDebuff(PickupType);

			Destroy(gameObject);
		}
	}
}
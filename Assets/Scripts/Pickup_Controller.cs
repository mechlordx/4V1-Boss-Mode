using UnityEngine;
using System.Collections;

public class Pickup_Controller : MonoBehaviour {
	newcockpit NC;
	boss_forms BF;
	player_move PM;
	Pickup_Dropper PD;

	public GameObject HoldingPlayer;

	public int PickupType;

	float timer = 0;
	float timerReset = 0.5f;

	public bool canPickup = true;
	// Use this for initialization
	void Awake () {
		PD = GameObject.Find("Boss").GetComponent<Pickup_Dropper>();
		BF = GameObject.Find ("Boss").GetComponent<boss_forms>();
		NC = GameObject.Find ("Cockpit").GetComponent<newcockpit>();

		//determined by the pickup dropper
		if(PickupType == 1)
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		if(PickupType == 2)
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		if(PickupType == 3)
			gameObject.GetComponent<Renderer>().material.color = Color.black;
		if(PickupType == 4)
			gameObject.GetComponent<Renderer>().material.color = Color.green;
		Color a = gameObject.GetComponent<Renderer>().material.color;
		a.a = 190;
		gameObject.GetComponent<Renderer>().material.color = a;

		GameObject particles = (GameObject) Resources.Load ("Prefabs/pickupParticle");
		particles = (GameObject)GameObject.Instantiate (particles, transform.position, Quaternion.identity);
		particles.transform.parent = transform;
		particles.GetComponent<ParticleSystem> ().startColor = gameObject.GetComponent<Renderer> ().material.color;
		particles.transform.Rotate (new Vector3 (-90f, 0f, 0f));
		transform.position += new Vector3 (0f, 0.5f, 0f);
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
			PD.PickupsOnGround--;
			Destroy(gameObject);
		}
	}
}

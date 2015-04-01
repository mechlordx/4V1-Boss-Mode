using UnityEngine;
using System.Collections;

public class Pickup_Controller : MonoBehaviour {
	newcockpit NC;
	boss_forms BF;
	player_move PM;

	public GameObject HoldingPlayer;

	public int PickupType;
	

	// Use this for initialization
	void Start () {
		BF = GameObject.Find ("Boss").GetComponent<boss_forms>();
		NC = GameObject.Find ("Cockpit").GetComponent<newcockpit>();

		//determined by the pickup dropper
		if(PickupType == 0)
			gameObject.GetComponent<Renderer>().material.color = Color.magenta;
		if(PickupType == 1)
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		if(PickupType == 2)
			gameObject.GetComponent<Renderer>().material.color = Color.black;
		if(PickupType == 1)
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col) {
		if(col.gameObject.tag == "Player"){
			PM = col.GetComponent<player_move>();
			PM.curPickups.Add(PickupType);
			Destroy(gameObject);
		}
	}
}

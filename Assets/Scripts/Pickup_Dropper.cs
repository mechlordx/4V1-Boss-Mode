using UnityEngine;
using System.Collections;

public class Pickup_Dropper : MonoBehaviour {
	Pickup_Controller PickCon;
	player_move PM;

	Vector3 Pos;
	float timer = 0;
	int PickupType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(timer <= 0)
			{
				Pos = new Vector3(Random.Range(-14,14), 1, Random.Range(-14,14));
				
			if((Mathf.Abs(Vector3.Distance(Pos,GameObject.Find ("Player1").transform.position))<=5) ||(Mathf.Abs(Vector3.Distance(Pos,GameObject.Find ("Player2").transform.position))<=5) ||(Mathf.Abs(Vector3.Distance(Pos,GameObject.Find ("Player3").transform.position))<=5) ||(Mathf.Abs(Vector3.Distance(Pos,GameObject.Find ("Player4").transform.position))<=5) || (Mathf.Abs(Vector3.Distance(Pos,GameObject.Find ("Boss").transform.position))<=5))
				return;

				PickupType = Random.Range(1,5);
				GameObject temp = (GameObject)Instantiate(Resources.Load ("Prefabs/Pickup " + PickupType));
				
				timer = Random.Range (5, 30);
			}
		else
			timer -= Time.deltaTime;
	}
}

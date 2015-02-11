using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss3 : MonoBehaviour {
	GameObject Wall;
	GameObject bullet;
	GameObject StunAttack;
	List<GameObject> SpawnedWalls = new List<GameObject>();
	public int playerNumber = -1;
	player_controls controlsRef;
	float cooldown = 0f;
	float maxcooldown = 0.35f;
	float Walldist = 5;
	GameObject b;

	Vector3 tempvec;
	// Use this for initialization
	void Start () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		Wall =(GameObject)Resources.Load ("Prefabs/Wall");
		bullet = (GameObject)Resources.Load ("Prefabs/forcebullet");
		StunAttack = (GameObject) Resources.Load ("Prefabs/Stunbullet");
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
		//wall stun
		else if(controlsRef.getButton(playerNumber, 1))
		{
			if(cooldown<0f)
			{
				cooldown = maxcooldown;
			

				GameObject a = (GameObject) GameObject.Instantiate(Wall, (transform.forward * Walldist), Quaternion.identity);
				//a.transform.parent = transform;
				a.transform.localEulerAngles = transform.eulerAngles;

				if(SpawnedWalls.Count < 3)
					SpawnedWalls.Add(a);
				else
				{
					b = SpawnedWalls[0];
					Destroy (b);
					SpawnedWalls.Remove(b);
					SpawnedWalls.Add(a);
				}
			
			}
		}
		//stun
		else if(controlsRef.getButton(playerNumber, 2))
		{
			if(cooldown<0f)
			{
				cooldown = maxcooldown;
				GameObject a = (GameObject) GameObject.Instantiate(StunAttack, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
		}
		
	}
}

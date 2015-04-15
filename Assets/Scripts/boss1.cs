using UnityEngine;
using System.Collections;

public class boss1 : MonoBehaviour {

	public int playerNumber = -1;
	player_controls controlsRef;
	float cooldown = 0f;
	float cooldown2 = 0f;
	float maxcooldown = 0.35f; // cooldown stat
	float maxcooldown2 = 1f; // cooldown stat
	float internalCooldown = 0f;
	GameObject bullet;
	// Use this for initialization
	void Start () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		bullet = (GameObject)Resources.Load ("Prefabs/forcebullet");
	}
	
	// Update is called once per frame
	void Update () {
		float timeFactor = transform.parent.gameObject.GetComponent<boss_control> ().cooldownFactor;
		if(playerNumber == -1)
			playerNumber = transform.parent.GetComponent<boss_control> ().playerNumber;
		cooldown += -(Time.deltaTime*timeFactor);
		cooldown2 += -(Time.deltaTime * timeFactor);;
		internalCooldown += -(Time.deltaTime);

		if(controlsRef.getButton(playerNumber, 0))
		{
			if(cooldown<0f && internalCooldown<0f)
			{
				cooldown = maxcooldown;
				internalCooldown = 0.35f; // cooldown stat
				GameObject a = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
		}
		else if(controlsRef.getButton(playerNumber, 1))
		{
			if(cooldown2<0f && internalCooldown<0f)
			{
				cooldown2 = maxcooldown2;
				internalCooldown = 0.35f; // cooldown stat
				GameObject a = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
				a.GetComponent<bullet>().force = 30000f;
				a.GetComponent<bullet>().speed = 23f;
				a.GetComponent<bullet>().redo();
			}
		}
		
	}

	public void SwapPlaces (GameObject a, GameObject b){

	}
}
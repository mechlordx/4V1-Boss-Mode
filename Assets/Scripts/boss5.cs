using UnityEngine;
using System.Collections;

public class boss5 : MonoBehaviour {
	
	public int playerNumber = -1;
	player_controls controlsRef;
	float cooldown = 0f;
	float maxcooldown = 0.35f;
	float gatlingCooldown = 0.2f;
	float laserCooldown = 0f;
	float maxLaserCooldown = 3f;
	int state = 0;

	GameObject bullet;
	GameObject laserBullet;
	GameObject skylasercross;

	// Use this for initialization
	void Start () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		bullet = (GameObject)Resources.Load ("Prefabs/forcebullet");
		laserBullet = (GameObject)Resources.Load ("Prefabs/laserbullet");
		skylasercross = (GameObject)Resources.Load ("Prefabs/skylasercross");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(playerNumber == -1)
			playerNumber = transform.parent.GetComponent<boss_control> ().playerNumber;
		cooldown += -Time.deltaTime;

		if(state==0) // Normal
		{
			laserCooldown += -Time.deltaTime;
			if(cooldown<0f && controlsRef.getButton(playerNumber, 0))
			{
				cooldown = maxcooldown;
				GameObject a = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
			else if(cooldown<0f && controlsRef.getButton(playerNumber, 1))
			{
				cooldown = gatlingCooldown;
				GameObject a = (GameObject) GameObject.Instantiate(laserBullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
			else if(laserCooldown<0f && controlsRef.getButton(playerNumber, 2))
			{
				state = 1;
				GameObject.Find ("Boss").GetComponent<boss_control>().disableTurn = true;
				laserCooldown = maxLaserCooldown;
				GameObject a = (GameObject) GameObject.Instantiate(skylasercross, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
		}
		else if(state==1)
		{
			if(!controlsRef.getButton(playerNumber, 2))
			{
				GameObject.Find ("skylasercross(Clone)").GetComponent<skylaser>().detonate();
				state = 0;
			}
		}
	}
}
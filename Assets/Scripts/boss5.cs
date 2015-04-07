using UnityEngine;
using System.Collections;

public class boss5 : MonoBehaviour {
	public AudioSource AS;

	public int playerNumber = -1;
	player_controls controlsRef;
	float cooldown = 0f;
	float maxcooldown = 0.6f;
	float gatlingCooldown = 0.2f;
	float laserCooldown = 0f;
	float maxLaserCooldown = 3f;
	int state = 0;

	GameObject bullet;
	GameObject laserBullet;
	GameObject skylasercross;

	// Use this for initialization
	void Start () {
		AS = GameObject.Find ("GameController").GetComponent<AudioSource> ();
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		bullet = (GameObject)Resources.Load ("Prefabs/forcebullet");
		laserBullet = (GameObject)Resources.Load ("Prefabs/laserbullet");
		skylasercross = (GameObject)Resources.Load ("Prefabs/skylasercross");
	}
	
	// Update is called once per frame
	void Update () 
	{
		float timeFactor = transform.parent.gameObject.GetComponent<boss_control> ().cooldownFactor;
		if(playerNumber == -1)
			playerNumber = transform.parent.GetComponent<boss_control> ().playerNumber;
		cooldown += -(Time.deltaTime*timeFactor);

		if(state==0) // Normal
		{
			laserCooldown += -(Time.deltaTime*timeFactor);
			if(cooldown<0f && controlsRef.getButton(playerNumber, 0))
			{
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[4], 1f);
				cooldown = maxcooldown;
				GameObject a = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
			else if(cooldown<0f && controlsRef.getButton(playerNumber, 1))
			{
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[3], 1f);
				cooldown = gatlingCooldown;
				GameObject a = (GameObject) GameObject.Instantiate(laserBullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
			else if(laserCooldown<0f && controlsRef.getButton(playerNumber, 2))
			{
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[2], 1f);
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
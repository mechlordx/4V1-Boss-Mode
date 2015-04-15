using UnityEngine;
using System.Collections;

public class boss5 : MonoBehaviour {
	public AudioSource AS;

	public int playerNumber = -1;
	player_controls controlsRef;
	float cooldown = 0f;
	float maxcooldown = 0.6f; // cooldown stat
	float cooldown2 = 0f;
	float maxcooldown2 = 0.1f; // cooldown stat
	float cooldown3 = 0f;
	float maxcooldown3 = 3f; // cooldown stat
	float internalCooldown = 0f;
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
		cooldown2 += -(Time.deltaTime * timeFactor);
		cooldown3 += -(Time.deltaTime * timeFactor);
		internalCooldown += -Time.deltaTime;

		if(state==0) // Normal
		{
			cooldown3 += -(Time.deltaTime*timeFactor);
			if(cooldown<0f && controlsRef.getButton(playerNumber, 0))
			{
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[4], 1f);
				cooldown = maxcooldown;
				internalCooldown = 0.45f; // cooldown stat
				GameObject a = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
			else if(cooldown2<0f && controlsRef.getButton(playerNumber, 1) && internalCooldown<0f)
			{
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[3], 1f);
				cooldown2 = maxcooldown2;
				internalCooldown = 0.1f; // cooldown stat
				GameObject a = (GameObject) GameObject.Instantiate(laserBullet, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
			}
			else if(cooldown3<0f && controlsRef.getButton(playerNumber, 2) && internalCooldown<0f)
			{
				AS.PlayOneShot(GameObject.Find("GameController").GetComponent<SoundController>().SFX[2], 1f);
				state = 1;
				GameObject.Find ("Boss").GetComponent<boss_control>().disableTurn = true;
				cooldown3 = maxcooldown3;
				internalCooldown = 0.45f; // cooldown stat
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
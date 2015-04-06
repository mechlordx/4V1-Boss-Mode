using UnityEngine;
using System.Collections;

public class Boss2 : MonoBehaviour {

	public int playerNumber = -1;
	player_controls controlsRef;
	float cooldown = 0f;
	float maxcooldown = 0.35f;
	GameObject bullet;
	GameObject bullet2;
	// Use this for initialization
	void Start () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		bullet = (GameObject)Resources.Load ("Prefabs/forcebullet");
		bullet2 = (GameObject)Resources.Load ("Prefabs/slowbullet");
	}
	
	// Update is called once per frame
	void Update () {
		float timeFactor = transform.parent.gameObject.GetComponent<boss_control> ().cooldownFactor;
		if(playerNumber == -1)
			playerNumber = transform.parent.GetComponent<boss_control> ().playerNumber;
		cooldown += -(Time.deltaTime*timeFactor);
		
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
		else if(controlsRef.getButton(playerNumber, 1))
		{
			if(cooldown<0f)
			{
				cooldown = maxcooldown;
				GameObject a = (GameObject) GameObject.Instantiate(bullet2, transform.position, Quaternion.identity);
				a.transform.parent = transform;
				a.transform.localEulerAngles = Vector3.zero;
				a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
				a.transform.parent = null;
				//a.GetComponent<bullet>().force = 30000f;
//				a.GetComponent<bullet>().speed = 23f;
			}
		}
		
	}
}

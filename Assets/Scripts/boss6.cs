using UnityEngine;
using System.Collections;

public class boss6 : MonoBehaviour {
	
	public int playerNumber = -1;
	player_controls controlsRef;
	float cooldown = 0f;
	float maxcooldown = 0.6f;
	float zapCooldown = 0f;
	float maxZapCooldown = 4f;
	int storeCurve = 0;
	int maxStoreCurve = 19;
	int state = 0;
	float timepassed = 0f;
	float curvetime = 0.45f;
	float bonusstore = 0.18f;
	float angle = 10f;
	GameObject bullet;
	GameObject zapbullet;
	// Use this for initialization
	void Start () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		bullet = (GameObject)Resources.Load ("Prefabs/forcebullet");
		zapbullet = (GameObject)Resources.Load ("Prefabs/zapbullet");
	}
	
	// Update is called once per frame
	void Update () {
		if(playerNumber == -1)
			playerNumber = transform.parent.GetComponent<boss_control> ().playerNumber;
		cooldown += -Time.deltaTime;
		zapCooldown += -Time.deltaTime;

		if(state==0)
		{
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
				if(zapCooldown<0f)
				{
					if(GameObject.Find ("electriccharge(Clone)"))
						Destroy (GameObject.Find ("electriccharge(Clone)"), 0f);
					zapCooldown = maxZapCooldown;
					GameObject a = (GameObject) GameObject.Instantiate(zapbullet, transform.position, Quaternion.identity);
					a.transform.parent = transform;
					a.transform.localEulerAngles = Vector3.zero;
					a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
					a.transform.parent = null;
				}
			}
			else if(controlsRef.getButton(playerNumber, 2))
			{
				state = 1;
				storeCurve = 1;
				timepassed = 0f;
			}
		}
		else if(state==1)
		{
			timepassed += Time.deltaTime;
			if(timepassed>curvetime)
			{
				timepassed += -curvetime * (1f + (storeCurve * bonusstore));
				storeCurve += 2;
				if(storeCurve>maxStoreCurve)
					storeCurve = maxStoreCurve;
				// Update visuals with stored energy
			}
			if(controlsRef.getButton(playerNumber, 2, -1))
			{
				// release energy
				GameObject a;
				float startangle = 0.5f * (storeCurve - 1) * -angle;
				for(int x=0;x<storeCurve;x++)
				{
					a = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
					a.transform.parent = transform;
					a.transform.localEulerAngles = Vector3.zero;
					a.transform.Rotate(new Vector3(0f, startangle + x*angle, 0f));
					a.transform.localPosition = Vector3.zero + new Vector3(0f, 1f, 0f);
					a.transform.parent = null;
				}
				state = 0;
			}
		}
	}
	
	public void SwapPlaces (GameObject a, GameObject b){
		
	}
}
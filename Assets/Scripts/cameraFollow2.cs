using UnityEngine;
using System.Collections;

public class cameraFollow2 : MonoBehaviour {
	newcockpit CP;

	public GameObject player;
	GameObject boss;
	public float angle;
	public float distance;
	public float height;
	float curAngle;
	float curDistance;
	float curHeight;

	bool lerping = false;
	float lerpFactor = 0.05f;
	float lerpPower = 0.05f;

	Vector3 playerPos;
	Vector3 bossPos;
	Vector3 dif;

	public int mode = 0;
	int lastMode = 0;

	// Use this for initialization
	void Awake () {
		CP = GameObject.Find("Cockpit").GetComponent<newcockpit> ();
		if(player==null)
			player = GetComponent<cameraFollow>().playerFollow;
		boss = GameObject.Find ("Boss");
		curAngle = angle;
		curHeight = height;
		curDistance = distance;
		lastMode = mode;
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = player.transform.position;
		bossPos = boss.transform.position;

		if(CP.attachedPlayer == player || (playerPos.x==0f && playerPos.z==0f)){
			mode = 1;
			dif = new Vector3(1,0,1);
		}
		else if (CP.attachedPlayer != player && mode != 2){
			mode = 0;
			dif = playerPos - bossPos;
		}
		else
			dif = new Vector3(1,0,1);

		if(mode!=lastMode)
			lerping = true;
		lastMode = mode;

		if(lerping)
		{
			Vector3 desired = modePos();
			Vector3 newCur = Vector3.zero;
			float lerpLength = lerpFactor*(desired.y - curDistance);
			if(lerpLength<0f)
				lerpLength += -lerpPower;
			else
				lerpLength += lerpPower;

			float lerpPerc = Mathf.Abs(lerpLength)/Mathf.Abs(desired.y - curDistance);
			if(lerpPerc>=1f)
			{
				newCur = modePos ();
				lerping = false;
			}
			else
			{
				curAngle += (desired.x - curAngle)*lerpPerc;
				curDistance += (desired.y - curDistance)*lerpPerc;
				curHeight += (desired.z - curHeight)*lerpPerc;
			}
			findPos(curAngle, curDistance, curHeight);
		}
		else
		{
			findPos(modePos());
		}
	}

	Vector3 modePos()
	{
		// Vec3(
		// Camera Angle,
		// Distance to center,
		// Height off of Ground)
		if(mode==0) // Player in arena
		{
			return new Vector3(angle, distance, height);
		}
		else if(mode==1) // Boss
		{
			return new Vector3(45f, 10f, 15f); // Find good numbers for this
		}
		else if(mode==2) // Should probably have a mode for brawling
		{
			return new Vector3(45f, 10f, 15f);
		}
		return Vector3.zero;
	}

	void findPos(Vector3 a)
	{
		findPos(a.x, a.y, a.z);
	}

	void findPos(float myAngle, float myDistance, float myHeight)
	{
		dif.y = 0f;

		if(mode == 1 || mode == 2){
			transform.position = new Vector3(Mathf.Sin((boss.transform.eulerAngles.y * Mathf.PI /180)), 0f,Mathf.Cos((boss.transform.eulerAngles.y * Mathf.PI /180))) * -1 * myDistance;
			Debug.Log ("Boss Angle:" + boss.transform.eulerAngles.y + " CamXdist: " + Mathf.Cos((boss.transform.eulerAngles.y * Mathf.PI /180)) * myDistance + " CamZdist: " + Mathf.Sin((boss.transform.eulerAngles.y * Mathf.PI /180)) * myDistance);
			transform.LookAt (new Vector3 (bossPos.x, 0f, bossPos.z));
			transform.position += new Vector3 (0f, myHeight, 0f);
			transform.eulerAngles = new Vector3(myAngle, transform.eulerAngles.y, transform.eulerAngles.z);


		}
		else{
			transform.position = Vector3.Normalize(dif) * myDistance;
			transform.LookAt (new Vector3 (bossPos.x, 0f, bossPos.z));
			transform.position += new Vector3 (0f, myHeight, 0f);
			transform.eulerAngles = new Vector3(myAngle, transform.eulerAngles.y, transform.eulerAngles.z);
		}

	}
}

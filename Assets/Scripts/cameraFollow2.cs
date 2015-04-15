using UnityEngine;
using System.Collections;

public class cameraFollow2 : MonoBehaviour {

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
		dif = playerPos - bossPos;

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
			return new Vector3(0f, 0f, 0f); // Find good numbers for this
		}
		else if(mode==2) // Should probably have a mode for brawling
		{
			return new Vector3(0f, 0f, 0f);
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
		transform.position = Vector3.Normalize (dif) * myDistance;
		transform.LookAt (new Vector3 (bossPos.x, 0f, bossPos.z));
		transform.position += new Vector3 (0f, myHeight, 0f);
		transform.eulerAngles = new Vector3(myAngle, transform.eulerAngles.y, transform.eulerAngles.z);
	}
}

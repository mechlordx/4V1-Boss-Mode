using UnityEngine;
using System.Collections;

public class boss_control : MonoBehaviour {

	public int playerNumber = -1;
	public float turnspeed = 17f;
	public float turnbuffer = 9f;
	public bool disableTurn = false;
	public float cooldownFactor = 1f;
	public float projectileFactor = 1f;
	public float forceFactor = 1f;
	float desiredangle = 0f;
	float deadzone = 0.3f;
	float hor = 0f;
	float ver = 0f;
	float internalTurnSpeed;
	float internalTurnBuffer;
	player_controls controlsRef;


	// Use this for initiazation
	void Awake () {
		internalTurnSpeed = turnspeed;
		internalTurnBuffer = turnbuffer;
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
	}
	
	// Update is called once per frame
	void Update() {
		hor = controlsRef.getAnyAxis (playerNumber, false);
		ver = controlsRef.getAnyAxis (playerNumber, true);
		if(Vector3.Magnitude(new Vector3(hor, 0f, ver))>1f)
		{
			float mag = Vector3.Magnitude(new Vector3(hor, 0f, ver));
			hor = hor * (1f / mag);
			ver = ver * (1f / mag);
		}
		if((Mathf.Abs(hor) < deadzone && Mathf.Abs (ver) < deadzone) || disableTurn)
			desiredangle = -1f;
		else
		{
			desiredangle = Vector2.Angle(new Vector2(1f,0f),new Vector2(hor,ver));
			if(Vector2.Angle(new Vector2(0f, -1f), new Vector2(hor,ver))<90f)
				desiredangle = 360f - desiredangle;
		}
	}

	void FixedUpdate()
	{
		float diff = Mathf.Abs (Mathf.DeltaAngle(transform.eulerAngles.y, desiredangle));

		transform.Rotate (new Vector3 (0f, turnspeed * controlsRef.getAnyAxis (playerNumber, true)));

		/*if(desiredangle!=-1)
		{
			if(diff<turnbuffer)
				transform.eulerAngles = new Vector3(transform.eulerAngles.x,
				                                    desiredangle,
				                                    transform.eulerAngles.z);
			else
			{
				bool turnRight = (((desiredangle - transform.eulerAngles.y) + 360f) % 360f) > 180.0f ? false : true;
				
				if(turnRight)
					transform.Rotate(new Vector3(0f, turnspeed, 0f));
				else
					transform.Rotate(new Vector3(0f, -turnspeed, 0f));
				if(turnRight!=(((desiredangle - transform.eulerAngles.y) + 360f) % 360f) > 180.0f ? false : true)
					transform.eulerAngles = new Vector3(transform.eulerAngles.x,
					                                    desiredangle,
					                                    transform.eulerAngles.z);
			}
		}*/
	}

	public void resetTurns()
	{
		turnspeed = internalTurnSpeed;
		turnbuffer = internalTurnBuffer;
		projectileFactor = 1f;
		forceFactor = 1f;
		cooldownFactor = 1f;
	}

	public void applyDebuff(int type)
	{
		if(type==1)
		{
			turnspeed = turnspeed*0.9f;
			turnbuffer = turnbuffer*0.9f;
		}
		else if(type==2)
		{
			cooldownFactor = cooldownFactor*0.9f;
		}
		else if(type==3)
		{
			projectileFactor = projectileFactor*0.9f;
		}
		else if(type==4)
		{
			forceFactor = forceFactor*0.89f;
		}
	}
}
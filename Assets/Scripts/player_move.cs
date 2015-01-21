using UnityEngine;
using System.Collections;

public class player_move : MonoBehaviour {

	public int playerNumber = 0;
	public float maxspeed = 10f;
	public float turnspeed = 30f;
	public float turnbuffer = 15f;
	float desiredangle = 0f;
	player_controls controlsRef;
	// Use this for initialization
	void Awake () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
	}

	void Update() {
		float hor = controlsRef.getAnyAxis (playerNumber, false);
		float ver = controlsRef.getAnyAxis (playerNumber, true);
		if(hor == 0f & ver == 0f)
			desiredangle = -1f;
		else
		{
			desiredangle = Vector2.Angle(new Vector2(1f,0f),new Vector2(hor,ver));
			if(Vector2.Angle(new Vector2(0f, -1f), new Vector2(hor,ver))<90f)
				desiredangle = 360f - desiredangle;
		}
	}

	void FixedUpdate () {
		if(desiredangle!=-1)
		{
			float diff = Mathf.Abs (Mathf.DeltaAngle(transform.localEulerAngles.y, desiredangle));
			if(diff<turnbuffer)
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
				                                         desiredangle,
				                                         transform.localEulerAngles.z);
			else
			{
				bool turnRight = (((desiredangle - transform.eulerAngles.y) + 360f) % 360f) > 180.0f ? false : true;

				if(turnRight)
					transform.Rotate(new Vector3(0f, turnspeed, 0f));
				else
					transform.Rotate(new Vector3(0f, -turnspeed, 0f));
				if(turnRight!=(((desiredangle - transform.eulerAngles.y) + 360f) % 360f) > 180.0f ? false : true)
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
					                                         desiredangle,
					                                         transform.localEulerAngles.z);
			}
		}
	}
}

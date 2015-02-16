using UnityEngine;
using System.Collections;

public class skylaser : MonoBehaviour {

	float speed = 0.65f;
	float deadzone = .3f;
	float desiredangle = -1f;
	float hor = 0f;
	float ver = 0f;
	int playerNumber = 0;
	player_controls controlsRef;

	// Use this for initialization
	void Awake () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls> ();
		playerNumber = GameObject.Find ("Boss").GetComponent<boss_control> ().playerNumber;
	}
	
	// Update is called once per frame
	void Update () 
	{
		hor = controlsRef.getAnyAxis (playerNumber, false);
		ver = controlsRef.getAnyAxis (playerNumber, true);
		if(Vector3.Magnitude(new Vector3(hor, 0f, ver))>1f)
		{
			float mag = Vector3.Magnitude(new Vector3(hor, 0f, ver));
			hor = hor * (1f / mag);
			ver = ver * (1f / mag);
		}
		if(Mathf.Abs(hor) < deadzone & Mathf.Abs (ver) < deadzone)
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
		if(desiredangle!=-1f)
		{
			transform.position += new Vector3(Mathf.Sin(desiredangle * Mathf.Deg2Rad) * speed,
			                                  0f,
			                                  Mathf.Cos(desiredangle * Mathf.Deg2Rad) * speed);
		}
	}

	public void detonate()
	{
		Collider[] hitObjects = Physics.OverlapSphere (transform.position, 1.5f);
		foreach(Collider a in hitObjects)
		{
			if(a.gameObject.tag=="Player")
			{
				int playerNumber = a.GetComponent<player_move>().playerNumber;
				if(GameObject.Find ("Cockpit").GetComponent<newcockpit>().attachedPlayer.GetComponent<player_move>().playerNumber!=playerNumber)
				{ // If not in the boss cockpit
					a.transform.position += -new Vector3(0f, -10f, 0f);
				}
			}
		}
		Destroy (gameObject, 0f);
	}
}

using UnityEngine;
using System.Collections;

public class player_move : MonoBehaviour {

	public int playerNumber = 0;
	public float maxspeed = 10f;
	public float maxforce = 20f;
	public float turnspeed = 30f;
	public float turnbuffer = 15f;
	public float a;
	float desiredangle = 0f;
	float deadzone = .25f;
	float hor = 0f;
	float ver = 0f;
	player_controls controlsRef;

	// Must make so that you can still add force in the opposite direction when 
	// over the maxspeed
	// Make different forces for different level of maxspeed

	// Use this for initialization
	void Awake () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
	}

	void Update() {
		hor = controlsRef.getAnyAxis (playerNumber, false);
		ver = controlsRef.getAnyAxis (playerNumber, true);
		if(Vector3.Magnitude(new Vector3(hor, 0f, ver))>1f)
		{
			float mag = Vector3.Magnitude(new Vector3(hor, 0f, ver));
			hor = hor * (1f / mag);
			ver = ver * (1f / mag);
		}
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
		float diff = Mathf.Abs (Mathf.DeltaAngle(transform.localEulerAngles.y, desiredangle));
		if(desiredangle!=-1)
		{
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
		float totalvelocity = Vector3.Magnitude(rigidbody.velocity-new Vector3(0f,rigidbody.velocity.y,0f))+0.07f*Vector3.Magnitude(new Vector3(hor, 0f, ver))
			- maxspeed; // Built in variable for how much the joystick force counts as.
		if(totalvelocity < 0
		   && Vector3.Magnitude(new Vector3(hor, 0f, ver)) > deadzone)
		{
			float maxpower = 0f;
			if(diff<90f)
				maxpower += (maxforce * Mathf.Cos(diff * Mathf.Deg2Rad));
			float rampat = 0.2f;
			if(totalvelocity>(-maxspeed*rampat))
			{
				float c = Mathf.Abs (totalvelocity) / (maxspeed * rampat);
				if(maxpower>(c*maxforce))
					maxpower = c*maxforce;
			}
			//float wantedpower = ((Vector3.Magnitude(new Vector3(hor, 0f, ver))-deadzone)/(1f-deadzone)) * maxforce;
			float wantedpower = (Vector3.Magnitude(new Vector3(hor, 0f, ver))/1f) * maxforce;
			if(wantedpower>maxpower)
				wantedpower = maxpower;

			rigidbody.AddForce(wantedpower * new Vector3(ver, 0f, hor));
		}
		a = Vector3.Magnitude (rigidbody.velocity);
	}
}

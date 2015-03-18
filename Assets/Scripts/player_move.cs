using UnityEngine;
using System.Collections;

public class player_move : MonoBehaviour {

	public int playerNumber = 0;
	bool nolimits = false;
	public float maxspeed = 10f;
	public float areaslowed = 6f;
	public float truemaxspeed = 10f;
	public float maxforce = 20f;
	public float turnspeed = 30f;
	public float turnbuffer = 15f;
	public float boostSeconds = 2.5f;
	public float boostDuration = 0.5f;
	public float boostMaxSpeed = 0f;
	public float boostForce = 0f;
	float boostTimer = 0f;
	float Timer = 5f;
	float MaxTimer = 5f;

	bool boosting = false;

	bool punching = false;
	bool startPunch = false;
	bool hasPunched = false;
	float punchDelay = 0.1f;
	float punchLength = 0.5f;
	float punchingTimer = 0f;

	float punchForce = 4000f;
	int punchZones = 3;
	float punchRadius = 0.45f;
	float punchRadiusDecay = 0.75f;
	float punchInitZone = 0.5f;
	float punchExtend = 0.5f;

	//public int maxjumptimer = 6;
	//public float jumpforce = 50f;
	public GameObject groundsphere;
	public float a;
	Vector3 spawnpoint;
	float desiredangle = 0f;
	float deadzone = .25f;
	float hor = 0f;
	float ver = 0f;
	player_controls controlsRef;
	bool startjump = false;
	bool jumping = false;
	bool isgrounded = false;
	public bool swap = false;
	public bool slow = false;
	public bool stun = false;
	int currentjumptimer = 0;

	// Must make so that you can still add force in the opposite direction when 
	// over the maxspeed
	// Make different forces for different level of maxspeed

	// Use this for initialization
	void Awake () {
		spawnpoint = transform.position;
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
	}

	void Update() {
		if(transform.position.y<-10f)
			transform.position = spawnpoint;
		if(!boosting&&!punching)
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
		if(controlsRef.getButton(playerNumber, 0, 1))
			startjump = true;
		if(controlsRef.getButton(playerNumber, 1, 1))
			startPunch = true;

		if (stun && Timer > 0f){
			Timer -= Time.deltaTime;
			maxspeed = 0;
		}
		if(stun && Timer <= 0f)
		{
			stun = false;

			Timer = MaxTimer;
			maxspeed = truemaxspeed;
		}
		if(slow && Timer > 0f && stun == false)
		{
			Timer -= Time.deltaTime;
			maxspeed = 5f;
		}
		if(slow && Timer <= 0f && stun == false){
			slow = false;
			Timer = MaxTimer;
			maxspeed = truemaxspeed;
		}
	}

	void FixedUpdate () {
		isgrounded = checkForGround ();
		if(isgrounded)

		if(isgrounded)
			jumping = false;
		/**
//Old way of movement
		float diff = Mathf.Abs (Mathf.DeltaAngle(transform.eulerAngles.y, desiredangle));
		if(desiredangle!=-1)
		{
			if(diff<turnbuffer)
				transform.eulerAngles = new Vector3(transform.eulerAngles.x,
				                                         desiredangle,
				                                         transform.eulerAngles.z);
			else
			{
				bool turnRight = (((desiredangle) + 360f) % 360f) > 180.0f ? false : true;

				if(turnRight)
					transform.Rotate(new Vector3(0f, turnspeed, 0f));
				else
					transform.Rotate(new Vector3(0f, -turnspeed, 0f));
				if(turnRight!=(((desiredangle) + 360f) % 360f) > 180.0f ? false : true)
					transform.eulerAngles = new Vector3(transform.eulerAngles.x,
					                                         desiredangle,
					                                         transform.eulerAngles.z);
			}
		}
		float totalvelocity = Vector3.Magnitude(GetComponent<Rigidbody>().velocity-new Vector3(0f,GetComponent<Rigidbody> ().velocity.y,0f))+0.07f*Vector3.Magnitude(new Vector3(hor, 0f, ver))
			- maxspeed; // Built in variable for how much the joystick force counts as.
		if((totalvelocity < 0
		   && Vector3.Magnitude(new Vector3(hor, 0f, ver)) > deadzone) || nolimits)
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

			GetComponent<Rigidbody>().AddForce(wantedpower * new Vector3(ver, 0f, hor));
		}
		a = Vector3.Magnitude (GetComponent<Rigidbody> ().velocity);
/**/
		/**/
//rotate & move forward and back
		transform.Rotate (new Vector3 (0f, (turnspeed/10) * controlsRef.getAnyAxis (playerNumber, true)));
		if(GetComponent<Rigidbody>().velocity.magnitude < maxspeed)
			transform.Translate(Vector3.forward * controlsRef.getAnyAxis(playerNumber,false) * maxspeed * Time.deltaTime);
/**/


		if(startjump && (ver != 0f || hor != 0f) && !punching)
		{
			if(!boosting && boostTimer<=0f)
			{
				boosting = true;
				maxspeed += boostMaxSpeed;
				maxforce += boostForce;
				boostTimer = boostDuration;
			}
		}
		boostTimer += -Time.deltaTime;
		if(boosting)
		{
			if(boostTimer<=0f)
			{
				boosting = false;
				maxspeed += -boostMaxSpeed;
				maxforce += -boostForce;
				boostTimer = boostSeconds;
			}
		}

		if(startPunch)
		{
			if(!punching)
			{
				punching = true;
				hasPunched = false;
				punchingTimer = punchLength;
			}

		}

		punchingTimer += -Time.deltaTime;
		if(punching)
		{
			if(hasPunched)
			{
				if(punchingTimer<0f)
				{
					punching = false;
				}
			}
			else
			{
				if(punchingTimer<(punchLength-punchDelay))
				{
					hasPunched = true;
					// Throw punch
					throwPunch(playerNumber);
				}
			}
		}

		// Jumping
		/*if(jumping)
		{
			if(currentjumptimer>0 && controlsRef.getButton(playerNumber, 0))
			{
				rigidbody.AddForce(new Vector3(0f, jumpforce * currentjumptimer/maxjumptimer, 0f));
			}
			else
			{
				rigidbody.useGravity = true;
				jumping = false;
				currentjumptimer = 0;
			}
		}
		else if(startjump && isgrounded)
		{
			jumping = true;
			currentjumptimer = maxjumptimer + 2;
			rigidbody.useGravity = false;
		}
		*/
		currentjumptimer += -1;
		startjump = false;
		startPunch = false;
	}

	void throwPunch(int myPlayerNumber)
	{
		int[] hitCounts = new int[4];
		for(int x=0;x<4;x++)
			hitCounts[x] = 0;

		float curRadius = punchRadius;
		float curDist = punchInitZone;

		for(int x=0;x<punchZones;x++)
		{
			Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * curDist), curRadius);

			foreach(Collider hit in hitColliders)
			{
				if(hit.gameObject.GetComponent<player_move>())
				{
					hitCounts[hit.gameObject.GetComponent<player_move>().playerNumber] += 1;
				}
			}

			curRadius = curRadius * punchRadiusDecay;
			curDist += punchExtend;
		}
		hitCounts [myPlayerNumber] = 0;

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		Vector3 myFoward = transform.forward;

		foreach(GameObject player in players)
		{
			int curPlayerNumber = player.GetComponent<player_move>().playerNumber;
			player.GetComponent<Rigidbody>().AddForce(myFoward * hitCounts[curPlayerNumber] * punchForce);
		}
	}

	bool checkForGround()
	{
		if(jumping)
			return false;
		var colliders = Physics.OverlapSphere (groundsphere.transform.position, .5f);
		foreach(Collider thing in colliders)
		{
			if(thing.gameObject.tag=="Ground")
			{
				if(thing.gameObject.transform.parent!=null)
				{
					if(thing.gameObject.transform.parent.gameObject.name == ("Player" + (playerNumber+1).ToString()))
					{

					}
					else
					{
						if(jumping || (!jumping && startjump))
						{
							// Knock the other player down
						}
						return true;
					}
				}
				else
					return true;
			}
		}
		return false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name.Contains("Player"))
		{
			if(GameObject.Find ("Ladder").GetComponent<ladder>().attachedPlayer!=null)
			{
				if(GameObject.Find ("Ladder").GetComponent<ladder>().attachedPlayer.GetComponent<player_move>().playerNumber ==
				   other.gameObject.GetComponent<player_move>().playerNumber)
					GameObject.Find ("Ladder").GetComponent<ladder>().throwplayer(gameObject);
			}
		}

		if(other.gameObject.name.Contains ("Swap"))
		   swap = true;

		if (other.gameObject.name.Contains("Stun")){
			stun = true;
			Timer = MaxTimer;
		}
		if (other.gameObject.name.Contains("slow"))
		{
			slow = true;
			Timer = MaxTimer;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.name.Contains("Player"))
		{
			if(GameObject.Find ("Ladder").GetComponent<ladder>().attachedPlayer!=null)
			{
				if(GameObject.Find ("Ladder").GetComponent<ladder>().attachedPlayer.GetComponent<player_move>().playerNumber ==
				   other.gameObject.GetComponent<player_move>().playerNumber)
					GameObject.Find ("Ladder").GetComponent<ladder>().throwplayer(gameObject);
			}
		}

		if(other.gameObject.name.Contains("AOE")){
				maxspeed = areaslowed;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.name.Contains("AOE")){
			if(other.gameObject.GetComponent<AOE_slow> ().isOn)
				maxspeed = truemaxspeed;
		}
	}

	public void deactivate(int number)
	{
		if(number!=playerNumber)
		{	
			transform.position = spawnpoint;
			transform.LookAt (GameObject.Find ("Boss").transform.position);
			transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
			GetComponent<player_move>().enabled = false;
		}
	}

	public void reactivate(int number)
	{
		if(number!=playerNumber)
		{	
			GetComponent<player_move>().enabled = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class ladder : MonoBehaviour {

	public GameObject attachedPlayer;
	public float climbspeed;
	public float heightlimit;
	public bool locked = false;
	GameObject thrower;
	int throwtimer = 0;
	int maxthrowtimer = 60;
	float drag;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(attachedPlayer!=null && throwtimer<0)
		{
			attachedPlayer.transform.position += new Vector3(0f, climbspeed*Time.deltaTime, 0f);
			if(attachedPlayer.transform.position.y>heightlimit)
			{
				attachedPlayer.transform.position = GameObject.Find ("Cockpit").transform.position;
				unattach();
				locked = true;
			}
			else if(attachedPlayer.transform.position.y<-5f)
				unattach();
		}
		else if(throwtimer==30) // Throwdown
		{
			GameObject a = thrower;
			GameObject b = attachedPlayer;
			Vector3 throwerpos = thrower.transform.position;
			Vector3 attachedpos = attachedPlayer.transform.position;
			thrower.transform.position = new Vector3(attachedpos.x,
			                                         throwerpos.y,
			                                         attachedpos.z);
			attachedPlayer.transform.position = new Vector3(throwerpos.x,
			                                                attachedpos.y,
			                                                throwerpos.z);
			attachedPlayer.transform.eulerAngles = thrower.transform.eulerAngles;

			Vector3 force = Vector3.Normalize(throwerpos - attachedpos);
			force += new Vector3(0f,-force.y,0f);
			force += new Vector3(0f, .1f, 0f);
			attachedPlayer.rigidbody.AddForce(force * 10000f);

			unattach();
			attach(thrower);
			thrower = null;
		}
		else if(throwtimer==1)
			locked = false;
		else if(thrower!=null)
			thrower.rigidbody.velocity = Vector3.zero;
		throwtimer += -1;
	}

	void OnTriggerEnter(Collider other)
	{
		if(attachedPlayer==null && !locked)
		{
			if(other.gameObject.name.Contains("Player"))
				attach (other.gameObject);
		}
	}

	public void attach(GameObject newplayer)
	{
		if(attachedPlayer==null)
		{
			attachedPlayer = newplayer;
			if(attachedPlayer.rigidbody.drag!=999f)
				drag = attachedPlayer.rigidbody.drag;
			attachedPlayer.transform.position = new Vector3(transform.position.x + 0.001f,
			                                           newplayer.transform.position.y,
			                                           transform.position.z);
			attachedPlayer.collider.isTrigger = true;
			attachedPlayer.rigidbody.useGravity = false;
			attachedPlayer.rigidbody.velocity = Vector3.zero;
			attachedPlayer.transform.eulerAngles = new Vector3(0f, 270f, 0f);
			attachedPlayer.rigidbody.drag = 999f;
			attachedPlayer.GetComponent<player_move>().enabled = false;
		}
	}

	public void throwplayer(GameObject newthrower)
	{
		if(throwtimer>0)
			return;
		throwtimer = maxthrowtimer;
		locked = true;
		thrower = newthrower;
		thrower.transform.LookAt (attachedPlayer.transform.position);
		thrower.transform.eulerAngles = new Vector3 (0f,
		                                            thrower.transform.eulerAngles.y,
		                                            0f);
		thrower.rigidbody.drag = 999f;

	}

	public void unattach()
	{
		if(attachedPlayer!=null)
		{
			attachedPlayer.collider.isTrigger = false;
			attachedPlayer.rigidbody.useGravity = true;
			attachedPlayer.GetComponent<player_move>().enabled = true;
			attachedPlayer.rigidbody.drag = drag;
			attachedPlayer = null;
		}
	}

	public bool checkattach(GameObject check)
	{
		if(attachedPlayer==null)
			return false;
		if(check.GetComponent<player_move>().playerNumber==attachedPlayer.GetComponent<player_move>().playerNumber)
		{
			unattach();
			return true;
		}
		return false;
	}
}

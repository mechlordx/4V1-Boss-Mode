using UnityEngine;
using System.Collections;

public class cockpit : MonoBehaviour {

	public GameObject attachedPlayer;
	public float jumpInHeight;
	public float cockpitheight;
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
		if(attachedPlayer==null)
		{
			checkforplayers();
		}
		else if(attachedPlayer!=null && throwtimer<0)
		{
			GameObject.Find ("Ladder").GetComponent<ladder>().locked = false;
			// Check for other players
			checkforplayers();
		}
		else if(throwtimer==30) // Throwdown
		{
			GameObject a = thrower;
			GameObject b = attachedPlayer;
			Vector3 throwerpos = thrower.transform.position;
			Vector3 attachedpos = attachedPlayer.transform.position;
			attachedPlayer.transform.position = throwerpos + new Vector3(0f, .75f, 0f);
			Vector3 force = Vector3.Normalize(throwerpos - attachedpos);
			force += new Vector3(0f,-force.y,0f);
			force += new Vector3(0f, .1f, 0f);

			force = Vector3.zero;

			unattach(force * 10000f);
			attach(thrower);
			thrower = null;
			// Reset game
		}
		else if(throwtimer==1)
			locked = false;
		else if(thrower!=null)
			thrower.rigidbody.velocity = Vector3.zero;
		throwtimer += -1;
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
			attachedPlayer.transform.parent = transform;
			attachedPlayer.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			attachedPlayer.transform.localPosition = Vector3.zero;
			attachedPlayer.rigidbody.drag = 999f;
			attachedPlayer.GetComponent<player_move>().enabled = false;
			reset ();
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
		thrower.collider.isTrigger = true;
		thrower.rigidbody.useGravity = false;
		thrower.rigidbody.velocity = Vector3.zero;
		thrower.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		thrower.transform.parent = transform;
		Vector3 direction = Vector3.Normalize(new Vector3(thrower.transform.localPosition.x, 0f, thrower.transform.localPosition.z));
		thrower.transform.eulerAngles = new Vector3(0f,
		                                            Vector3.Angle(new Vector3(0f, 0f, 1f), direction),
		                                            0f);
		thrower.transform.localPosition = direction * .5f + new Vector3(0f, 0.4f, 0f);
		//Debug.Break();
		
	}
	
	public void unattach(Vector3 force)
	{
		attachedPlayer.collider.isTrigger = false;
		attachedPlayer.rigidbody.useGravity = true;
		attachedPlayer.GetComponent<player_move>().enabled = true;
		attachedPlayer.rigidbody.drag = drag;
		attachedPlayer.transform.parent = null;
		attachedPlayer.rigidbody.AddForce (force);
		attachedPlayer = null;
	}

	bool checkladder(Collider collider)
	{
		return GameObject.Find ("Ladder").GetComponent<ladder> ().checkattach (collider.gameObject);
	}

	void checkforplayers()
	{
		var colliders = Physics.OverlapSphere(transform.position, 1.5f);
		foreach(Collider collider in colliders)
		{
			if(collider.gameObject.tag=="Player")
			{
				if(collider.isTrigger)
				{
					if(checkladder(collider))
					{

					}
					else
						continue;
					// Lol you just got knocked out
				}
				if(attachedPlayer==null)
				{
					checkladder(collider);
					attach(collider.gameObject);
					break;
				}
				else
				{
					checkladder(collider);
					throwplayer(collider.gameObject);
				}
			}
		}
	}

	void reset()
	{
		GameObject.Find ("Ladder").GetComponent<ladder>().unattach();
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		int number = attachedPlayer.GetComponent<player_move> ().playerNumber;
		foreach(GameObject player in players)
		{
			player.GetComponent<player_move>().deactivate(number);
			player.rigidbody.velocity = Vector3.zero;
		}
		transform.parent.gameObject.GetComponent<boss_control> ().enabled = true;
		transform.parent.gameObject.GetComponent<boss_control>().playerNumber = number;
		transform.parent.gameObject.GetComponent<boss_forms> ().chooseform ();
	}
}

using UnityEngine;
using System.Collections;

public class newcockpit : MonoBehaviour {

	public GameObject attachedPlayer;
	public float jumpInHeight;
	public float cockpitheight;
	public bool locked = false;
	public float[] brawlHealth;
	public float startBrawlHealth = 10f;
	public float maxBrawlHealth = 15f;
	public float bossGain = 0.95f;
	public float bossDam = 0.45f;
	public float playerGain = 0.5f;
	public float playerDam = 1f;
	public GameObject[] players;
	int bossNumber = -1;

	// Use this for initialization
	void Awake () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		brawlHealth = new float[4];
		for(int x=0;x<brawlHealth.Length;x++)
			brawlHealth[x] = -1F;
	}
	
	// Update is called once per frame
	void Update () {
		if(attachedPlayer!=null)
		{
			bossNumber = attachedPlayer.GetComponent<player_move>().playerNumber;
			if(brawlHealth[bossNumber]<=0f)
			{
				unattach();
			}
		}
		checkforplayers ();
		if(brawling())
		{
			player_controls controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
			// Apply damages and gains
			for(int x=0;x<2;x++)
			{
				for(int y=0;y<4;y++)
				{
					if(controlsRef.getButton(y,x,1))
						damage(y);
				}
			}

			// If someone won
			if(!brawling())
			{
				unattach();
				for(int x=0;x<brawlHealth.Length;x++)
				{
					if(brawlHealth[x]>0f)
					{
						foreach(GameObject player in players)
						{
							if(player.GetComponent<player_move>().playerNumber==x)
							{
								attach(player);
								break;
							}
						}
					}
				}
			}
		}
		else
		{

		}
	}
	
	public void attach(GameObject newplayer)
	{
		if(attachedPlayer==null)
		{
			GameObject.Find("Boss").GetComponent<boss_control>().resetTurns();
			attachedPlayer = newplayer;
			attachedPlayer.SetActive(true);
			attachedPlayer.transform.position = new Vector3(transform.position.x + 0.001f,
			                                                newplayer.transform.position.y,
			                                                transform.position.z);
			attachedPlayer.collider.isTrigger = true;
			attachedPlayer.rigidbody.useGravity = false;
			attachedPlayer.rigidbody.velocity = Vector3.zero;
			attachedPlayer.transform.parent = transform;
			attachedPlayer.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			attachedPlayer.transform.localPosition = Vector3.zero;
			attachedPlayer.GetComponent<player_move>().enabled = false;
			reset ();
		}
	}
	
	public void unattach()
	{
		Vector3 force = Vector3.zero;
		if(attachedPlayer!=null)
		{
			attachedPlayer.SetActive(true);
			attachedPlayer.collider.isTrigger = false;
			attachedPlayer.rigidbody.useGravity = true;
			attachedPlayer.GetComponent<player_move>().enabled = true;
			attachedPlayer.transform.parent = null;
			attachedPlayer.rigidbody.AddForce (force);
			attachedPlayer = null;
		}
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
				if(collider.GetComponent<player_move>().enabled==false)
					continue;
				if(checkladder(collider))
				{
					GameObject.Find ("Ladder").GetComponent<ladder>().unattach();
				}
				if(attachedPlayer==null)
				{
					attach (collider.gameObject);
				}
				else
				{
					if(brawlHealth[collider.gameObject.GetComponent<player_move>().playerNumber]==-1)
					{
						collider.gameObject.SetActive(false);
						collider.gameObject.transform.position = transform.position;
						brawlHealth[collider.gameObject.GetComponent<player_move>().playerNumber] = startBrawlHealth;
					}
				}
			}
		}
	}
	
	void reset()
	{
		GameObject.Find ("Ladder").GetComponent<ladder>().unattach();
		int number = attachedPlayer.GetComponent<player_move> ().playerNumber;
		foreach(GameObject player in players)
		{
			player.GetComponent<player_move>().deactivate(number);
			player.rigidbody.velocity = Vector3.zero;
			player.SetActive(true);
		}
		transform.parent.gameObject.GetComponent<boss_control> ().enabled = true;
		transform.parent.gameObject.GetComponent<boss_control>().playerNumber = number;
		transform.parent.gameObject.GetComponent<boss_forms> ().chooseform ();
		for(int x=0;x<brawlHealth.Length;x++)
			brawlHealth[x] = -1f;
		brawlHealth [attachedPlayer.GetComponent<player_move> ().playerNumber] = startBrawlHealth;
	}

	bool brawling()
	{
		int numberOfPlayers = 0;
		for(int x=0;x<brawlHealth.Length;x++)
		{
			if(brawlHealth[x]>0f)
				numberOfPlayers += 1;
		}
		if(numberOfPlayers>1)
			return true;
		return false;
	}

	void damage(int thePlayer)
	{
		float dam = 0f;
		float gain = 0f;
		bool[] alive = new bool[4];
		for(int x=0;x<4;x++)
			alive[x] = false;
		for(int x=0;x<4;x++)
		{
			if(brawlHealth[x]>0f)
				alive[x] = true;
		}
		if(thePlayer==bossNumber)
		{
			dam = bossDam;
			gain = bossGain;
		}
		else
		{
			dam = playerDam;
			gain = playerGain;
		}
		for(int x=0;x<brawlHealth.Length;x++)
		{
			if(x==thePlayer && brawlHealth[x]>0f)
			{
				brawlHealth[x] += gain;
				if(brawlHealth[x]>maxBrawlHealth)
					brawlHealth[x] = maxBrawlHealth;
			}
			else if(x!=thePlayer && brawlHealth[thePlayer]>0f)
				brawlHealth[x] += -dam;
		}

		for(int x=0;x<4;x++)
		{
			if(alive[x])
			{
				if(brawlHealth[x]<=0f) // died
				{
					foreach(GameObject player in players)
					{
						if(player.GetComponent<player_move>().playerNumber==x)
						{
							Debug.Log ("fix");
							player.rigidbody.velocity = Vector3.zero;
							player.SetActive(true);
							player.transform.position = new Vector3(0f, -3f, 0f);
						}
					}
				}
			}
		}
	}
}
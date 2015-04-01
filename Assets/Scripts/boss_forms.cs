using UnityEngine;
using System.Collections;

public class boss_forms : MonoBehaviour {

	public GameObject[] pool;
	public int[] currentchoices;
	public int choice = -1;
	public GameObject prefabRef;
	float timer = 0f;
	bool ticking = false;
	player_controls controlsRef;
	
	// Use this for initialization
	void Awake () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls>();
		
		GameObject.Find ("GameController").GetComponent<score> ().begin ();
		for(int x=0;x<9999;x++)
		{
			if(Resources.Load ("BossPrefabs/" + "boss" + x.ToString()))
			{
				Debug.Log ("Found #" + x.ToString());
				addprefab((GameObject) Resources.Load ("BossPrefabs/" + "boss" + x.ToString()));
			}
		}

		/*pool = new MonoBehaviour[things.Length];
		for(int x=0;x<pool.Length;x++)
		{
			pool[x] = (MonoBehaviour) things[x];
		}

		foreach(MonoBehaviour c in pool)
		{
			gameObject.AddComponent(c.name);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		int playerNumber = GetComponent<boss_control> ().playerNumber;
		if(timer>3.5f)
		{
			// Let player choose
			for(int x=0;x<4;x++)
			{
				if(controlsRef.getButton(playerNumber, x, 1))
				{
					if(GameObject.Find ("Player" + (playerNumber+1).ToString())
					   .GetComponent<player_move>().containsPickup(x))
						choose(currentchoices[x]);
				}
			}
		}
		else if(timer>0f)
		{
			if(choice==-1)
			{
				// Choose first option
				choose(1);
			}
		}

		timer += -Time.deltaTime;
		if(ticking)
		{
			if(timer<=0f)
			{
				GameObject.Find("GameController").GetComponent<score>().unpause();
				ticking = false;
				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
				foreach(GameObject player in players)
				{
					player.GetComponent<player_move>().enabled = true;
				}
				Debug.Log ("Player" + playerNumber.ToString());
				GameObject.Find ("Player" + (playerNumber+1).ToString()).GetComponent<player_move>().enabled = false;
			}
		}
	}

	public void chooseform()
	{
		if(prefabRef!=null)
			Destroy(prefabRef, 0f);
		choice = -1;
		timer = 10f;
		GameObject.Find ("GameController").GetComponent<score> ().pause ();
		ticking = true;
		currentchoices = new int[4];
		for(int x=0;x<currentchoices.Length;x++)
		{
			currentchoices[x] = x+3;
			Debug.Log ("Current Choice " + x + " " + currentchoices[x]);
		}
	}

	void choose(int newchoice)
	{
		if(timer>3.5f)
			timer = 3.4f;
		if(pool.Length!=0)
			become(newchoice);
		choice = newchoice;
		Debug.Log (choice);
	}

	void become(int choice)
	{
		prefabRef = (GameObject) GameObject.Instantiate (pool [choice-1], transform.position, Quaternion.identity);
		prefabRef.transform.parent = transform;
		prefabRef.transform.localEulerAngles = Vector3.zero;
		prefabRef.transform.localPosition = Vector3.zero;
	}

	void addprefab(GameObject toadd)
	{
		var oldpool = pool;
		pool = new GameObject[oldpool.Length + 1];
		for(int x=0;x<oldpool.Length;x++)
		{
			pool[x] = oldpool[x];
		}
		pool[oldpool.Length] = toadd;
	}

	/*void disableall()
	{
		MonoBehaviour[] comps = GetComponents<MonoBehaviour> ();
		foreach(MonoBehaviour c in comps)
		{
			if(includes(c))
				c.enabled = false;
		}
	}

	bool includes(MonoBehaviour c)
	{
		for(int x=0;x<pool.Length;x++)
		{
			if(pool[x]==c)
				return true;
		}
		return false;
	}

	void findandenable(MonoBehaviour newchoice)
	{
		MonoBehaviour[] comps = GetComponents<MonoBehaviour> ();
		foreach(MonoBehaviour c in comps)
		{
			if(c==newchoice)
			{
				c.enabled = true;
				return;
			}
		}
		Debug.Log ("Failure");
	}*/
}

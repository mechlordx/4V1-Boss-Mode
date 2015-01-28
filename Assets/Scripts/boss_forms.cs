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
				if(controlsRef.getButton(playerNumber, x))
					choose(x);
			}
		}
		else if(timer>0f)
		{
			if(choice==-1)
			{
				// Choose first option
				choose(Random.Range (0, pool.Length));
			}
		}

		timer += -Time.deltaTime;
		if(ticking)
		{
			if(timer<=0f)
			{
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
		ticking = true;
		currentchoices = new int[4];
		for(int x=0;x<currentchoices.Length;x++)
		{
			currentchoices[x] = Random.Range (0, pool.Length);
		}
	}

	void choose(int choice)
	{
		if(timer>3.5f)
			timer = 3.5f;
		if(pool.Length!=0)
			become(choice);
	}

	void become(int choice)
	{
		prefabRef = (GameObject) GameObject.Instantiate (pool [choice], transform.position, Quaternion.identity);
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

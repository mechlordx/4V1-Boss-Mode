using UnityEngine;
using System.Collections;

public class charge : MonoBehaviour {

	float radius = 30f;
	float maxpull = 6000f;
	float minpull = 300f;
	float timeleft = 10f;
	int myplayernumber;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		timeleft += -Time.deltaTime;
		if(timeleft<0f)
			Destroy (gameObject, 0f);
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		int currentnumber;
		float distance;
		float force;
		Vector3 direction;
		foreach(GameObject player in players)
		{
			currentnumber = player.GetComponent<player_move>().playerNumber;
			if((currentnumber!=
			   GameObject.Find ("Boss").GetComponent<boss_control>().playerNumber) &&
				(currentnumber!=
				 myplayernumber))
			{
				distance = Vector3.Distance(player.transform.position, transform.position);
				if(distance<radius)
				{
					force = (radius - distance)/radius;
					force = (maxpull - minpull) * force;
					force += minpull;

					direction = transform.position - player.transform.position;
					direction = Vector3.Normalize(direction);
					player.rigidbody.AddForce(direction * force);
				}
			}
		}
	}

	public void startfollow(GameObject tofollow)
	{
		myplayernumber = tofollow.GetComponent<player_move> ().playerNumber;
		transform.parent = tofollow.transform;
		transform.localPosition = Vector3.zero;
	}
}

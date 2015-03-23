using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public GameObject playerFollow;
	GameObject theBoss;
	Vector3 bossPosition;
	float dis = 3f;

	// Use this for initialization
	void Awake () {
		theBoss = GameObject.Find ("Boss");
		bossPosition = new Vector3(theBoss.transform.position.x,
		                           0f,
		                           theBoss.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = bossPosition + new Vector3(0f,playerFollow.transform.position.y,0f);
		transform.LookAt (playerFollow.transform.position);
		transform.position += transform.forward * (Vector3.Magnitude(transform.position - playerFollow.transform.position) + dis);
		transform.Rotate (new Vector3 (0f, 180f, 0f));
		transform.position += new Vector3 (0f, 1.5f, 0f);
		transform.Rotate (new Vector3 (20f, 0f, 0f));
	}
}

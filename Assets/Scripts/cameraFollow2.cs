using UnityEngine;
using System.Collections;

public class cameraFollow2 : MonoBehaviour {

	public GameObject player;
	GameObject boss;
	public float angle;
	public float distance;
	public float height;

	// Use this for initialization
	void Awake () {
		if(player==null)
			player = GetComponent<cameraFollow>().playerFollow;
		boss = GameObject.Find ("Boss");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = player.transform.position;
		Vector3 bossPos = boss.transform.position;
		Vector3 dif = playerPos - bossPos;
		dif.y = 0f;
		transform.position = Vector3.Normalize (dif) * distance;
		transform.LookAt (new Vector3 (bossPos.x, 0f, bossPos.z));
		transform.position += new Vector3 (0f, height, 0f);
		transform.eulerAngles = new Vector3(angle, transform.eulerAngles.y, transform.eulerAngles.z);
		//transform.eulerAngles = new Vector3 (angle, camAngle, 0f);
	}
}

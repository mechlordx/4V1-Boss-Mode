using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
	newcockpit CP;

	public GameObject theCockpit;
	public GameObject playerFollow;
	GameObject theBoss;
	Vector3 bossPosition;
	float dis = 3f;

	// Use this for initialization
	void Awake () {
		CP = theCockpit.GetComponent<newcockpit> ();

		theBoss = GameObject.Find ("Boss");
		bossPosition = new Vector3(theBoss.transform.position.x,
		                           0f,
		                           theBoss.transform.position.z);
		if (CP.attachedPlayer == null)
			Debug.Log("Oh no!");
		
		if(playerFollow == null)
			Debug.Log ("WHY!");
	}
	
	// Update is called once per frame
	void Update () {
	

		if(CP.attachedPlayer == null || CP.attachedPlayer != playerFollow){
			if(CP.attachedPlayer == null ||(playerFollow.transform.position.x != CP.attachedPlayer.transform.position.x && playerFollow.transform.position.z != CP.attachedPlayer.transform.position.z)){
				if(transform.parent != null)
					transform.parent = null;

				transform.position = bossPosition + new Vector3(0f,playerFollow.transform.position.y,0f);
				transform.LookAt (playerFollow.transform.position);
				transform.position += transform.forward * (Vector3.Magnitude(transform.position - playerFollow.transform.position) + dis);
				transform.Rotate (new Vector3 (0f, 180f, 0f));
				transform.position += new Vector3 (0f, 1.5f, 0f);
				transform.Rotate (new Vector3 (20f, 0f, 0f));
			}
			else{
				transform.position = new Vector3(0,15,-10);
				transform.rotation = Quaternion.Euler(45, 0, 0);
			}

		}
		/**/
		else{


			if(transform.parent == null){
				transform.parent = playerFollow.transform;
				transform.position = new Vector3(0, 15, -10);
				transform.rotation = Quaternion.Euler(45, 0, 0);
			}


		}
		/**/
	}
}

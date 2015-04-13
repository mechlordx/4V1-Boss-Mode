using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
	newcockpit CP;

	public GameObject theCockpit;
	public GameObject playerFollow;
	public int playerNumber;
	GameObject theBoss;
	Vector3 bossPosition;
	float dis = 3f;
	int mode = 0;
	player_controls controlsRef;
	Vector3 desiredRelPos;
	Vector3 fakeBossPosition;


	// Use this for initialization
	void Awake () {
		CP = theCockpit.GetComponent<newcockpit> ();
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls> ();
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
		//If not the boss follow the player, if is boss become a child to the playerfor ease of following
		if(CP.attachedPlayer == null || CP.attachedPlayer != playerFollow){
			if(CP.attachedPlayer == null ||(playerFollow.transform.position.x != CP.attachedPlayer.transform.position.x && playerFollow.transform.position.z != CP.attachedPlayer.transform.position.z)){
				if(controlsRef.getButton(playerNumber, 3, 1))
				{
					setPositionBasedOn(playerFollow.transform.position + playerFollow.transform.forward*10f);
					desiredRelPos = transform.position - playerFollow.transform.position;
					mode = 1;
				}
				if(controlsRef.getButton(playerNumber, 2, 1))
				{
					if(mode==0)
					{
						mode = 1;
						desiredRelPos = transform.position - playerFollow.transform.position;
					}
					else if(mode==1)
						mode = 0;
				}
				if(transform.parent != null)
					transform.parent = null;
				if(mode==0)
				{
					setPositionBasedOn(bossPosition);
				}
				else if(mode==1)
				{
					transform.position = playerFollow.transform.position + desiredRelPos;
				}
			}
			else{
				transform.position = new Vector3(0,15,-10);
				transform.rotation = Quaternion.Euler(45, 0, 0);
			}

		}
		else{

			if(transform.parent == null){
				transform.parent = playerFollow.transform;
				transform.localPosition = new Vector3(0, 15, -10);
				//transform.rotation = Quaternion.Euler(45, 0, 0);
				//transform.rotation = Quaternion.Euler(playerFollow.transform.rotation.x + 45, playerFollow.transform.rotation.y,playerFollow.transform.rotation.z);
			}

			if(CP.camReset){
				//transform.parent = playerFollow.transform;
				//transform.position = new Vector3(0, 15, -10);
				CP.camReset = false;
			}

			if(transform.rotation.eulerAngles.x <= 44 || transform.rotation.x >= 46)
				transform.localRotation = Quaternion.Euler(45,0,0);
			/*
			if((transform.rotation.eulerAngles.y <= 359 && transform.rotation.y >= 1) || transform.rotation.eulerAngles.y <= -1)
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0,transform.rotation.eulerAngles.z);
			if((transform.rotation.eulerAngles.z <= 359 && transform.rotation.z >= 1) || transform.rotation.eulerAngles.z <= -1)
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,0);
				*/
				
		}
		/**/
	}

	void setPositionBasedOn(Vector3 pos)
	{
		transform.position = pos + new Vector3(0f,playerFollow.transform.position.y,0f);
		transform.LookAt (playerFollow.transform.position);
		transform.position += transform.forward * (Vector3.Magnitude(transform.position - playerFollow.transform.position) + dis);
		transform.Rotate (new Vector3 (0f, 180f, 0f));
		transform.position += new Vector3 (0f, 1.5f, 0f);
		transform.Rotate (new Vector3 (20f, 0f, 0f));
	}
}

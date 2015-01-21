using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public float movespeed = 1f;

	int a;

	GameObject gameControllerRef;

	healthbars HPbars;
	player_controls controlsRef;

	public int playerNum;
	// Use this for initialization
	void Start () {
		HPbars = Camera.main.gameObject.GetComponent<healthbars> ();
		gameControllerRef = GameObject.Find ("GameController");
		controlsRef = gameControllerRef.GetComponent<player_controls> ();

		if(gameObject.tag == "P1"){
			playerNum = 0;
		}

		else if(gameObject.tag == "P2"){
			playerNum = 1;
		}

		else if(gameObject.tag =="P3"){
			playerNum = 2;
		}
		if(gameObject.tag == "P4"){
			playerNum = 3;
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*
			for(int x=0;x<4;x++)
			{
				if(controlsRef.getButton(playerNumber, x, 1))
					a = 1;
				else if(controlsRef.getButton(playerNumber, x))
					a = 2;
				else if(controlsRef.getButton(playerNumber, x, -1))
					a = 3;
				else
					a = 0;
				allButtons[x].GetComponent<button_light>().state(a);
			}
*/
		if(gameObject.tag == "P1"){
			
			if(Input.GetKey("w"))
				gameObject.transform.position+= Vector3.forward*movespeed;
			if(Input.GetKey("a"))
				gameObject.transform.position+= Vector3.left*movespeed;
			if(Input.GetKey("s"))
				gameObject.transform.position+= Vector3.back*movespeed;
			if(Input.GetKey("d"))
				gameObject.transform.position+= Vector3.right*movespeed;

		}

		else if(gameObject.tag == "P2"){
			
			if(Input.GetKey(KeyCode.UpArrow))
				gameObject.transform.position+= Vector3.forward*movespeed;
			   if(Input.GetKey(KeyCode.LeftArrow))
				gameObject.transform.position+= Vector3.left*movespeed;
			if(Input.GetKey(KeyCode.DownArrow))
				gameObject.transform.position+= Vector3.back*movespeed;
			if(Input.GetKey(KeyCode.RightArrow))
				gameObject.transform.position+= Vector3.right*movespeed;
			
		}
	
	}
	void OnCollisionStay (Collision col){
	if( col.transform.gameObject.tag == "Boss"){
			//Deal damage here
			if(Input.GetKeyDown ("q") && gameObject.tag == "P1"){
				HPbars.bossHealth -= 50;
			}
			else if(Input.GetKeyDown(KeyCode.KeypadEnter) && gameObject.tag == "P2"){
				HPbars.bossHealth -= 50;
			}
		}
	}
}

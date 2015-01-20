using UnityEngine;
using System.Collections;

public class button_test : MonoBehaviour {

	public int playerNumber = 0;
	public GameObject[] allButtons;
	public GameObject joystick;
	GameObject gameControllerRef;
	player_controls controlsRef;
	float joyx;
	float joyy;
	float joyscale = .5f;
	// Use this for initialization
	void Awake () {
		joyx = joystick.transform.position.x;
		joyy = joystick.transform.position.y;
		gameControllerRef = GameObject.Find ("GameController");
		controlsRef = gameControllerRef.GetComponent<player_controls> ();
	}
	
	// Update is called once per frame
	void Update () {
		int a;
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
		joystick.transform.position = new Vector3(joyx + controlsRef.getAnyAxis(playerNumber, true) * joyscale,
		                                          joyy + controlsRef.getAnyAxis(playerNumber, false) * joyscale,
		                                          joystick.transform.position.z);
	}
}

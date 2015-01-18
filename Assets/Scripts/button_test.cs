using UnityEngine;
using System.Collections;

public class button_test : MonoBehaviour {

	public int playerNumber = 0;
	public GameObject[] allButtons;
	GameObject gameControllerRef;
	player_controls controlsRef;
	public Material[] mymats;

	// Use this for initialization
	void Awake () {
		gameControllerRef = GameObject.Find ("GameController");
		controlsRef = gameControllerRef.GetComponent<player_controls> ();
		for(int x=0;x<allButtons.Length;x++)
			allButtons[x].GetComponent<button_light>().mats = mymats;
	}
	
	// Update is called once per frame
	void Update () {
		int a;
		for(int x=0;x<4;x++)
		{
			if(controlsRef.getDirection(playerNumber, x, 1))
				a = 1;
			else if(controlsRef.getDirection(playerNumber, x))
				a = 2;
			else if(controlsRef.getDirection(playerNumber, x, -1))
				a = 3;
			else
				a = 0;
			allButtons[x].GetComponent<button_light>().state(a);
		}

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
			allButtons[x+4].GetComponent<button_light>().state(a);
		}
		/*
		for(int x=0;x<4;x++)
		{
			if(controlsRef.getButton(playerNumber, x, 1))
				allButtons[x+4].GetComponent<button_light>().down();
			else if(controlsRef.getButton(playerNumber, x, -1))
				allButtons[x+4].GetComponent<button_light>().up();
		}
		if(controlsRef.getAxisAsButton(playerNumber, true, true, 1))
		   allButtons[0+4+4].GetComponent<button_light>().down();
		else if(controlsRef.getAxisAsButton(playerNumber, true, true, -1))
		   allButtons[0+4+4].GetComponent<button_light>().up();

		if(controlsRef.getAxisAsButton(playerNumber, true, false, 1))
			allButtons[2+4+4].GetComponent<button_light>().down();
		else if(controlsRef.getAxisAsButton(playerNumber, true, false, -1))
			allButtons[2+4+4].GetComponent<button_light>().up();

		if(controlsRef.getAxisAsButton(playerNumber, false, true, 1))
			allButtons[1+4+4].GetComponent<button_light>().down();
		else if(controlsRef.getAxisAsButton(playerNumber, false, true, -1))
			allButtons[1+4+4].GetComponent<button_light>().up();

		if(controlsRef.getAxisAsButton(playerNumber, false, false, 1))
			allButtons[3+4+4].GetComponent<button_light>().down();
		else if(controlsRef.getAxisAsButton(playerNumber, false, false, -1))
			allButtons[3+4+4].GetComponent<button_light>().up();
*/
	}
}

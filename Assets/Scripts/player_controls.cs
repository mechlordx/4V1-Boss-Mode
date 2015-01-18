using UnityEngine;
using System.Collections;

public class player_controls : MonoBehaviour {
	
	public int maxPlayerCount = 5;
	public int buttonCount = 4;

	public string playerName = "Player";
	public string buttonName = "_Button";
	public string horizontalAxisName = "_X-Axis";
	public string verticalAxisName = "_Y-Axis";
	string[] directions = {"Up", "Right", "Down", "Left"};

	int buttonsPerPlayer;
	
	public string[] easyNames;

	bool enforceActivePlayers = false;
	bool[] activePlayers;

	int[] lastPressed;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
		activePlayers = new bool[maxPlayerCount];
		for(int x=0;x<activePlayers.Length;x++)
			activePlayers[x] = true;
		buttonsPerPlayer = directions.Length + buttonCount + 4;
		easyNames = new string[buttonsPerPlayer * maxPlayerCount];

		int currentButton;
		bool currentBool;
		string currentName;
		int floor;
		for(int currentPlayer=0;currentPlayer<maxPlayerCount;currentPlayer++)
		{
			floor = buttonsPerPlayer * currentPlayer;
			for(int x=0;x<directions.Length;x++)
			{
				currentButton = floor + x;
				currentName = playerName + (currentPlayer+1).ToString() + buttonName + directions[x];
				easyNames[currentButton] = currentName;
			}
			floor += directions.Length;
			for(int x=0;x<buttonCount;x++)
			{
				currentButton = floor + x;
				currentName = playerName + (currentPlayer+1).ToString() + buttonName + (x+1).ToString();
				easyNames[currentButton] = currentName;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// DPad, then Buttons, then Horizontal-Pos->Neg, then Vertical-Pos->Neg
	}

	public bool getButton(int thePlayer, int theButton, int thestate = 0)
	{
		string name = easyNames[(buttonsPerPlayer * thePlayer) + theButton + directions.Length];
		if(thestate==1)
			return Input.GetButtonDown(name);
		else if(thestate==0)
			return Input.GetButton(name);
		else if(thestate==-1)
			return Input.GetButtonUp(name);
		else
		{
			Debug.Log ("Direction check fail, state not possible");
			return false;
		}
	}

	public bool getDirection(int thePlayer, int theButton, int thestate = 0)
	{
		string name = easyNames[(buttonsPerPlayer * thePlayer) + theButton];
		if(thestate==1)
			return Input.GetButtonDown(name);
		else if(thestate==0)
			return Input.GetButton(name);
		else if(thestate==-1)
			return Input.GetButtonUp(name);
		else
		{
			Debug.Log ("Direction check fail, state not possible");
			return false;
		}
	}

	public float getAxis(int thePlayer, bool horizontal)
	{
		if(horizontal)
			return Input.GetAxis(playerName + (thePlayer+1).ToString() + horizontalAxisName);
		else
			return Input.GetAxis(playerName + (thePlayer+1).ToString() + verticalAxisName);
	}

	public float getAxisRaw(int thePlayer, bool horizontal)
	{
		if(horizontal)
			return Input.GetAxisRaw(playerName + (thePlayer+1).ToString() + horizontalAxisName);
		else
			return Input.GetAxisRaw(playerName + (thePlayer+1).ToString() + verticalAxisName);
	}

	/*public bool getAxisAsButton(int thePlayer, bool horizontal, bool positive, int thestate = 0)
	{
		int a = 0;
		if(!horizontal)
			a += 2;
		if(!positive)
			a += 1;
		if(thestate==1)
			return onPress [(buttonsPerPlayer * thePlayer) + buttonCount + directions.Length + a];
		else if(thestate==0)
			return isPressed [(buttonsPerPlayer * thePlayer) + buttonCount + directions.Length + a];
		else if(thestate==-1)
			return onRelease [(buttonsPerPlayer * thePlayer) + buttonCount + directions.Length + a];
		else
		{
			Debug.Log ("Direction check fail, state not possible");
			return false;
		}
	}*/

	/*void updateAxis(int thePlayer, int currentFloor)
	{
		bool[] posneg = {false, false};
		float a;
		a = Input.GetAxisRaw (playerName + (thePlayer + 1).ToString () + horizontalAxisName);
		if(a==1f)
			posneg[0] = true;
		else if(a==-1f)
			posneg[1] = true;
		updateButton (currentFloor, posneg[0]);
		updateButton (currentFloor + 1, posneg[1]);

		posneg [0] = false;
		posneg [1] = false;

		a = Input.GetAxisRaw (playerName + (thePlayer + 1).ToString () + verticalAxisName);
		if(a==1f)
			posneg[0] = true;
		else if(a==-1f)
			posneg[1] = true;
		updateButton (currentFloor + 2, posneg[0]);
		updateButton (currentFloor + 3, posneg[1]);
	}*/
}

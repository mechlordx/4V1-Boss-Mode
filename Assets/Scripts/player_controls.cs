﻿using UnityEngine;
using System.Collections;

public class player_controls : MonoBehaviour {
	
	public int maxPlayerCount = 4;
	public int buttonCount = 4;

	public bool keepDebugSettings = false;

	public string playerName = "Player";
	public string buttonName = "_Button";
	public string horizontalAxisName = "_X-Axis";
	public string verticalAxisName = "_Y-Axis";
	string[] directions = {"Up", "Right", "Down", "Left"};

	int buttonsPerPlayer;
	int axesPerPlayer = 2;
	public string[] buttonNames;
	public string[] axisNames;

	bool enforceActivePlayers = false;
	bool[] activePlayers;

	int[] lastPressed;
	
	bool waitingforaxis = false;
	bool waitingforbutton = false;
	int waitingarrayindex;

	// Use this for initialization
	void Awake () {
		Input.ResetInputAxes();
		DontDestroyOnLoad (gameObject);
		activePlayers = new bool[maxPlayerCount];
		for(int x=0;x<activePlayers.Length;x++)
			activePlayers[x] = true;

		buttonsPerPlayer = directions.Length + buttonCount;
		if(!keepDebugSettings)
		{
			buttonNames = new string[buttonsPerPlayer * maxPlayerCount];
			axisNames = new string[2 * maxPlayerCount];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(waitingforaxis)
		{
			for(int joystick=1;joystick<5;joystick++)
			{
				if(Input.GetKey(KeyCode.Escape))
				{
					waitingforaxis = false;
					break;
				}
				for(int button=0;button<4;button++)
				{
					name = "Joystick " + joystick.ToString() + " Axis " + button.ToString();
					if(Mathf.Abs (Input.GetAxis(name)) >= .3f)
					{
						axisNames[waitingarrayindex] = name;
						waitingforaxis = false;
						break;
					}
				}
				if(!waitingforaxis)
					break;
			}
		}
		else if(waitingforbutton)
		{
			for(int joystick=1;joystick<6;joystick++)
			{
				if(Input.GetKey(KeyCode.Escape))
				{
					waitingforbutton = false;
					break;
				}
				for(int button=0;button<20;button++)
				{
					name = "joystick " + joystick.ToString() + " button " + button.ToString();
					if(Input.GetKeyDown(name))
					{
						buttonNames[waitingarrayindex] = name;
						waitingforbutton = false;
						break;
					}
				}
				if(!waitingforbutton)
					break;
			}
		}
	}

	public bool getButton(int thePlayer, int theButton, int thestate = 0)
	{
		string name = buttonNames[(buttonsPerPlayer * thePlayer) + theButton + directions.Length];
		if(name=="")
			return false;
		if(thestate==1)
			return Input.GetKeyDown(name);
		else if(thestate==0)
			return Input.GetKey(name);
		else if(thestate==-1)
			return Input.GetKeyUp(name);
		else
		{
			Debug.Log ("Direction check fail, state not possible");
			return false;
		}
	}

	public bool getDirection(int thePlayer, int theButton, int thestate = 0)
	{
		string name = buttonNames[(buttonsPerPlayer * thePlayer) + theButton];
		if(name=="")
			return false;
		if(thestate==1)
			return Input.GetKeyDown(name);
		else if(thestate==0)
			return Input.GetKey(name);
		else if(thestate==-1)
			return Input.GetKeyUp(name);
		else
		{
			Debug.Log ("Direction check fail, state not possible");
			return false;
		}
	}

	public float getAxis(int thePlayer, bool horizontal)
	{
		if(horizontal)
		{
			if(axisNames[thePlayer*2]=="")
				return 0f;
			return Input.GetAxisRaw(axisNames[thePlayer*2]);
		}
		else
		{
			if(axisNames[thePlayer*2 + 1]=="")
				return 0f;
			return Input.GetAxisRaw(axisNames[thePlayer*2 + 1]);
		}
	}

	public float getAxisRaw(int thePlayer, bool horizontal)
	{
		if(horizontal)
		{
			if(axisNames[thePlayer*2]=="")
				return 0f;
			return Input.GetAxisRaw(axisNames[thePlayer*2]);
		}
		else
		{
			if(axisNames[thePlayer*2 + 1]=="")
				return 0f;
			return Input.GetAxisRaw(axisNames[thePlayer*2 + 1]);
		}
	}

	public float getPadAxis(int thePlayer, bool horizontal)
	{
		if(horizontal)
		{
			if(Input.GetKey(buttonNames[(buttonsPerPlayer * thePlayer) + 1]))
				return 1f;
			   else if(Input.GetKey(buttonNames[(buttonsPerPlayer * thePlayer) + 3]))
				return -1f;
			else
				return 0f;
		}
		else
		{
			if(Input.GetKey(buttonNames[(buttonsPerPlayer * thePlayer) + 0]))
				return 1f;
			   else if(Input.GetKey(buttonNames[(buttonsPerPlayer * thePlayer) + 2]))
				return -1f;
			else
				return 0f;
		}
	}

	public float getAnyAxis(int thePlayer, bool horizontal)
	{
		float toreturn = 0f;

		toreturn = getAxis (thePlayer, horizontal);
		if(toreturn == 0f)
			toreturn = getPadAxis (thePlayer, horizontal);

		return toreturn;
	}

	public void setAxis(int thePlayer, bool horizontal)
	{
		if(waitingforaxis || waitingforbutton)
			return;
		Input.ResetInputAxes();
		int a = thePlayer*axesPerPlayer;
		if(!horizontal)
			a += 1;
		waitingarrayindex = a;
		waitingforaxis = true;
	}

	public void setButton(int thePlayer, int thebutton)
	{
		if(waitingforaxis || waitingforbutton)
			return;
		waitingarrayindex = thePlayer * buttonsPerPlayer + 4 + thebutton;
		waitingforbutton = true;
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
		a = Input.GetAxisRawRaw (playerName + (thePlayer + 1).ToString () + horizontalAxisName);
		if(a==1f)
			posneg[0] = true;
		else if(a==-1f)
			posneg[1] = true;
		updateButton (currentFloor, posneg[0]);
		updateButton (currentFloor + 1, posneg[1]);

		posneg [0] = false;
		posneg [1] = false;

		a = Input.GetAxisRawRaw (playerName + (thePlayer + 1).ToString () + verticalAxisName);
		if(a==1f)
			posneg[0] = true;
		else if(a==-1f)
			posneg[1] = true;
		updateButton (currentFloor + 2, posneg[0]);
		updateButton (currentFloor + 3, posneg[1]);
	}*/
}

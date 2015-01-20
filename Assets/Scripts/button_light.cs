using UnityEngine;
using System.Collections;

public class button_light : MonoBehaviour {

	int maxtimer = 7;
	public int pressedtimer = 0;
	public int unpressedtimer = 0;
	public bool pressed = false;
	public Material[] mats;

	// Use this for initialization
	void Start () {
		mats = new Material[4];
		mats [0] = (Material) Resources.Load ("White");
		
		mats [1] = (Material) Resources.Load ("Red");
		
		mats [2] = (Material) Resources.Load ("Blue");
		
		mats [3] = (Material) Resources.Load ("Green");
	}
	
	// Update is called once per frame
	void Update () {
	
		if(pressedtimer>0)
		{
			renderer.material = mats[1];
		}
		else if(pressed)
		{
			renderer.material = mats[2];
		}
		else if(unpressedtimer>0)
		{
			renderer.material = mats[3];
		}
		else
			renderer.material = mats[0];
		unpressedtimer += -1;
		pressedtimer += -1;
	}

	public void state(int newstate)
	{
		if(newstate==1)
		{
			pressedtimer = maxtimer;
			pressed = true;
			unpressedtimer = 0;
		}
		else if(newstate==3)
		{
			unpressedtimer = maxtimer;
			pressed = false;
			pressedtimer = 0;
		}
	}
}

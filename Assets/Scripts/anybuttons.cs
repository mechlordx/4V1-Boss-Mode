using UnityEngine;
using System.Collections;

public class anybuttons : MonoBehaviour {

	public int joystickcount = 6;
	int buttoncount = 20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string name;
		//Debug.Log(Input.GetKey("joystick 1 button 1"));
		for(int joystick=1;joystick<joystickcount;joystick++)
		{
			for(int button=0;button<buttoncount;button++)
			{
				name = "joystick " + joystick.ToString() + " button " + button.ToString();
				if(Input.GetKeyDown(name))
					Debug.Log (name);
			}
		}
	}
}

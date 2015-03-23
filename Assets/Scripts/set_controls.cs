using UnityEngine;
using System.Collections;

public class set_controls : MonoBehaviour {

	public int currentcontrol = -1;
	public int player = 0;
	public GameObject textObject;
	player_controls controlsref;
	// Use this for initialization
	void Awake () {
		controlsref = GameObject.Find ("GameController").GetComponent<player_controls> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentcontrol!=-1)
		{
			if(!controlsref.isWaiting())
			{
				currentcontrol += 1;
				if(currentcontrol>=0 && currentcontrol < 4)
				{
					controlsref.setButton(player, currentcontrol);
					display ("Player " + (player+1).ToString() + ": " + "\n" + "Press button" + (currentcontrol+1).ToString());
				}
				else if(currentcontrol==4)
				{
					controlsref.setAxis(player, true);
					display ("Player " + (player+1).ToString() + ": " + "\n" + "Tilt horizontal joystick");
				}
				else if(currentcontrol==5)
				{
					controlsref.setAxis(player, false);
					display ("Player " + (player+1).ToString() + ": " + "\n" + "Tilt vertical joystick");
				}
				else if(currentcontrol>5 && currentcontrol<10)
				{
					string dir = GameObject.Find ("GameController").GetComponent<player_controls>().directions[currentcontrol-6];
					controlsref.setButton(player, (currentcontrol - 4 - 6));
					display ("Player " + (player+1).ToString() + ": " + "\n" + "Press " + dir);
				}
				else if(currentcontrol==10)
				{
					currentcontrol = -1;
					string[] buttons = controlsref.buttonNames;
					string[] axes = controlsref.axisNames;
					usave_file usaveref = GameObject.Find("GameController").GetComponent<usave_file>();
					usaveref.sarrayResize(0);
					usaveref.insertBefore(0, axes, true);
					usaveref.insertBefore(0, buttons, true);
					usaveref.saveFile();
					GetComponent<controls_menu_script>().wait = false;
				}
			}
		}
		else
			display ("");
	}

	void display(string newtext)
	{
		if(textObject!=null)
		{
			textObject.GetComponent<TextMesh>().text = newtext;
		}
	}

	public void startSet(int playernumber)
	{
		player = playernumber;
		currentcontrol = 0;
		controlsref.setButton(player, currentcontrol);
		display ("Player " + (player+1).ToString() + ": " + "\n" + "Press button" + (currentcontrol+1).ToString());

	}
}
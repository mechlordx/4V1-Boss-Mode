using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerReady : MonoBehaviour {

	public int playerNumber = 0;
	public bool ready = false;
	public bool readyUp = false;
	//public GameObject[] icons;
	public GameObject icon;
	public GameObject text;
	float deselectedcolor = 0.6f;
	float selectedcolor = 1f;
	bool selector = false;
	player_controls controlsRef;
	game_menu_script menuRef;

	public Image uiIcon;
	public Sprite playerNeut;
	public Sprite playerSelect;

	// Use this for initialization
	void Awake () {
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(ready)
		{
			if(readyUp)
			{
				if(controlsRef.getButton(playerNumber, 1, 1))
					readyUp = false;
			}
			else if(controlsRef.getButton(playerNumber, 2, 1))
				ready = false;
			else if(controlsRef.getButton(playerNumber, 0, 1))
				readyUp = true;
		}
		else
		{
			if(controlsRef.getButton(playerNumber, 2, 1))
				ready = true;
		}

		// Diplay icons
		if(ready)
		{
			uiIcon.GetComponent<Image>().sprite = playerSelect;

			var newcolor = icon.GetComponent<Renderer>().material.color;
			newcolor.r = selectedcolor;
			newcolor.g = selectedcolor;
			newcolor.b = selectedcolor;
			icon.GetComponent<Renderer>().material.color = newcolor;

			if(readyUp)
				settext ("Unready: B");
			else
				settext ("Drop-Out: Y" + '\n' + "Ready: A");
		}
		else
		{
			uiIcon.GetComponent<Image>().sprite = playerNeut;
			var newcolor = icon.GetComponent<Renderer>().material.color;
			newcolor.r = deselectedcolor;
			newcolor.g = deselectedcolor;
			newcolor.b = deselectedcolor;
			icon.GetComponent<Renderer>().material.color = newcolor;

			settext ("Drop-In: Y");
		}
	}

	void settext(string newtext)
	{
		text.GetComponent<TextMesh> ().text = newtext;
	}
}

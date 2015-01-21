using UnityEngine;
using System.Collections;

public class main_menu_script : MonoBehaviour {
	
	public bool debug = true;
	public string[] debugloadlevels;
	public bool wait = false;
	public int initialbutton = 0;
	public int selectedbutton;
	public GameObject[] buttons;
	public int[] selectmatrix;
	public bool linear = true;
	public float deselectedcolor;
	int maxbutton;
	/* The soundeffect script can be placed on the button or menu object.
	 * The priority is the button's soundeffect over the menu object's.
	 * Sound1 is used for changing button selection, Sound2 for enter/a-selecting.
	 */
	void Start () {
		maxbutton = buttons.Length;
		selectedbutton = initialbutton;
		// Unlight all buttons
		Color originalcolor;
		foreach(GameObject button in buttons)
		{
			originalcolor = button.renderer.material.color;
			originalcolor.r = deselectedcolor;
			originalcolor.g = deselectedcolor;
			originalcolor.b = deselectedcolor;
			button.renderer.material.color = originalcolor;
		}
		// Light up selected button
		originalcolor = buttons[selectedbutton].renderer.material.color;
		originalcolor.r = 1f;
		originalcolor.g = 1f;
		originalcolor.b = 1f;
		buttons[selectedbutton].renderer.material.color = originalcolor;
		
	}
	
	void enter()
	{
		wait = true;
		if(buttons[selectedbutton].GetComponent<soundeffect>())
			GetComponent<soundeffect>().play(2);
		else if(GetComponent<soundeffect>())
			GetComponent<soundeffect>().play(2);
		if(selectedbutton==0)
		{
			GameObject.Find ("GameController").GetComponent<loader>().loadScene(1);
		}
		else if(selectedbutton==1)
		{
		}
		else if(selectedbutton==2)
		{
		}
		else if(selectedbutton==3)
		{
		}
		else if(selectedbutton==4)
		{
		}
		else if(selectedbutton==5)
		{
		}
		else if(selectedbutton==6)
		{
		}
		else if(selectedbutton==7)
		{
		}
		else if(selectedbutton==8)
		{
		}
		else if(selectedbutton==9)
		{
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!wait)
		{
			if(linear)
			{
				bool change = false;
				int from = 0;
				if((Input.GetKeyDown(KeyCode.DownArrow))||(Input.GetKeyDown(KeyCode.S)))
				{
					from = selectedbutton;
					change = true;
					selectedbutton += 1;
					if(selectedbutton==maxbutton)
						selectedbutton = 0;
				}
				if((Input.GetKeyDown(KeyCode.UpArrow))||(Input.GetKeyDown(KeyCode.W)))
				{
					from = selectedbutton;
					change = true;
					selectedbutton += -1;
					if(selectedbutton==-1)
						selectedbutton += maxbutton;
				}
				if(change)
				{
					Color newcolor;
					newcolor = buttons[from].renderer.material.color;
					newcolor.r = deselectedcolor;
					newcolor.g = deselectedcolor;
					newcolor.b = deselectedcolor;
					buttons[from].renderer.material.color = newcolor;
					newcolor = buttons[selectedbutton].renderer.material.color;
					newcolor.r = 1f;
					newcolor.g = 1f;
					newcolor.b = 1f;
					buttons[selectedbutton].renderer.material.color = newcolor;
					if(buttons[selectedbutton].GetComponent<soundeffect>())
						GetComponent<soundeffect>().play(1);
					else if(GetComponent<soundeffect>())
						GetComponent<soundeffect>().play(1);
				}
			}
			else
			{
				int button = 4;
				if((Input.GetKeyDown (KeyCode.UpArrow))||(Input.GetKeyDown (KeyCode.W)))
					button = 0;
				else if((Input.GetKeyDown(KeyCode.RightArrow))||(Input.GetKeyDown (KeyCode.D)))
					button = 1;
				else if((Input.GetKeyDown(KeyCode.DownArrow))||(Input.GetKeyDown (KeyCode.S)))
					button = 2;
				else if((Input.GetKeyDown(KeyCode.LeftArrow))||(Input.GetKeyDown (KeyCode.A)))
					button = 3;
				
				int from = selectedbutton;
				int to = selectedbutton;
				if(button==4)
					to = selectedbutton;
				else
					to = selectmatrix[selectedbutton * 4 + button];
				if((to==selectedbutton)||(to==-1))
				{
				}
				else
				{
					Color newcolor;
					newcolor = buttons[from].renderer.material.color;
					newcolor.r = deselectedcolor;
					newcolor.g = deselectedcolor;
					newcolor.b = deselectedcolor;
					buttons[from].renderer.material.color = newcolor;
					newcolor = buttons[to].renderer.material.color;
					newcolor.r = 1f;
					newcolor.g = 1f;
					newcolor.b = 1f;
					buttons[to].renderer.material.color = newcolor;
					selectedbutton = to;
					if(buttons[selectedbutton].GetComponent<soundeffect>())
						GetComponent<soundeffect>().play(1);
					else if(GetComponent<soundeffect>())
						GetComponent<soundeffect>().play(1);
				}
			}
			if((Input.GetKeyDown(KeyCode.KeypadEnter))||(Input.GetKeyDown(KeyCode.Space))||(Input.GetKeyDown(KeyCode.Return)))
			{
				enter ();
			}
		}
	}
}

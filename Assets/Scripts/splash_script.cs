using UnityEngine;
using System.Collections;

public class splash_script : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		usave_file usaveref = GameObject.Find ("GameController").GetComponent<usave_file> ();
		if(usaveref.ifSlot (usaveref.slot))
		{
			usaveref.sarrayResize(32+8);
			usaveref.loadFile();
			string[] buttons = usaveref.sarrayGet(0, 32);
			string[] axes = usaveref.sarrayGet (32, 8);
			GameObject.Find("GameController").GetComponent<player_controls>().buttonNames = buttons;
			GameObject.Find("GameController").GetComponent<player_controls>().axisNames = axes;
		}
		GameObject.Find ("GameController").GetComponent<loader> ().loadScene (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class cheatCode : MonoBehaviour {

	public string code = "a";
	public bool activated = false;
	string currentCode = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.Escape))
		{
			currentCode = "";
			activated = false;
		}
		currentCode += Input.inputString;
		if(currentCode==code && !activated)
		{
			GetComponent<loader>().loadScene(1);
			currentCode = "no";
			activated = true;
		}
	}
}

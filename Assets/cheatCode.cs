using UnityEngine;
using System.Collections;

public class cheatCode : MonoBehaviour {

	public string code = "a";
	string currentCode = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.Escape))
			currentCode = "";
		currentCode += Input.inputString;
		if(currentCode==code)
		{
			GetComponent<loader>().loadScene(1);
			currentCode = "no";
		}
	}
}

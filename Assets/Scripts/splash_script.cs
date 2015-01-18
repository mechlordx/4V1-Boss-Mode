using UnityEngine;
using System.Collections;

public class splash_script : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject.Find ("GameController").GetComponent<loader> ().loadScene (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

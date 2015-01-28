using UnityEngine;
using System.Collections;

public class printallcomponents : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		MonoBehaviour[] comps = GetComponents<MonoBehaviour> ();
		foreach(MonoBehaviour c in comps)
		{
			Debug.Log (c.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

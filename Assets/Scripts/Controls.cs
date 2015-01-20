using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public float movespeed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.name == "P1"){
			if(Input.GetKey("w"))
				gameObject.transform.position+= Vector3.forward*movespeed;
			if(Input.GetKey("a"))
				gameObject.transform.position+= Vector3.left*movespeed;
			if(Input.GetKey("s"))
				gameObject.transform.position+= Vector3.back*movespeed;
			if(Input.GetKey("d"))
				gameObject.transform.position+= Vector3.right*movespeed;
		}
	
	}
}

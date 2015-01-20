using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public float movespeed = 1f;
	public healthbars HPbars;

	// Use this for initialization
	void Start () {
		HPbars = Camera.main.GetComponent<healthbars> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.tag == "P1"){
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

	void OnCollisionStay(Collision col){
		print ("ow");
		if(col.gameObject.tag.Equals("Boss") == true){
			if(Input.GetKeyDown ("q")){
				HPbars.bossHealth -= 100;
			}
		}
	}
}

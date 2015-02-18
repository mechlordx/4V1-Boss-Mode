﻿using UnityEngine;
using System.Collections;

public class zapbullet : MonoBehaviour {

	public float force = 300f;
	public float speed = 40f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var things = Physics.OverlapSphere (transform.position, transform.localScale.x / 2f);
		foreach(Collider thing in things)
		{
			if(thing.gameObject.tag=="Player")
			{
				boom(thing.gameObject);
				break;
			}
			if(thing.gameObject.tag == "wall"){
				destroy();
				break;
			}
		}
		transform.position += transform.forward * Time.deltaTime * speed;
	}
	
	void boom(GameObject thing)
	{
		thing.rigidbody.AddForce(transform.forward * force);
		if(GameObject.Find ("electriccharge(Clone)"))
			Destroy (GameObject.Find ("electriccharge(Clone)"), 0f);
		GameObject a = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/electriccharge"), transform.position, Quaternion.identity);
		a.GetComponent<charge> ().startfollow (thing);
		Destroy (gameObject, 0f);
	}
	void destroy (){
		Destroy(gameObject);
	}
}

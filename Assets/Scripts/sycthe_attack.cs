using UnityEngine;
using System.Collections;

public class sycthe_attack : MonoBehaviour {

	public float force = 15000f;
	public float speed = 55f; 
	// Use this for initialization
	void Awake () {
		Destroy (gameObject, 10f);
	}
	
	// Update is called once per frame
	void Update () {

		transform.localScale = transform.localScale * 1.0005f;
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
		Destroy (gameObject, 0f);
	}
	void destroy (){
		Destroy(gameObject);
	}
}

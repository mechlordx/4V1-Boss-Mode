using UnityEngine;
using System.Collections;

public class StunBullet : MonoBehaviour {

	public bool Stun= false;
	public float speed = 40f; 
	
	
	// Use this for initialization
	void Awake () {
		Destroy (gameObject, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		var things = Physics.OverlapSphere (transform.position, transform.localScale.x / 2f);
		foreach(Collider thing in things)
		{
			if(thing.gameObject.tag=="Player")
			{
				stun(thing.gameObject);
				break;
			}
			if(thing.gameObject.tag == "wall"){
				destroy();
				break;
			}
		}
		transform.position += transform.forward * Time.deltaTime * speed;
	}
	
	void stun(GameObject thing)
	{
		//thing.rigidbody.AddForce(transform.forward * force);
		Stun = true;
		Destroy (gameObject, 0f);
	}
	void destroy (){
		Destroy(gameObject);
	}
}

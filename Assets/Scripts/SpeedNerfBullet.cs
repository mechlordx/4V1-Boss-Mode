using UnityEngine;
using System.Collections;

public class SpeedNerfBullet : MonoBehaviour {

	public bool Speed_Decrease = true;
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
				slow(thing.gameObject);
				break;
			}
		}
		transform.position += transform.forward * Time.deltaTime * speed;
	}
	
	void slow(GameObject thing)
	{
		//thing.rigidbody.AddForce(transform.forward * force);
		Speed_Decrease = true;
		Destroy (gameObject, 0f);
	}
}

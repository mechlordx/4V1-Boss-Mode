using UnityEngine;
using System.Collections;

public class SwapbeamScript : MonoBehaviour {

		public float force = 0.0000001f;
		public float speed = 55f; 
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
					boom(thing.gameObject);
					break;
				}
			}
			transform.position += transform.forward * Time.deltaTime * speed;
		}
		
		void boom(GameObject thing)
		{
			
			thing.rigidbody.AddForce(transform.forward * force);
			//Destroy (gameObject, 0f);
		}
}

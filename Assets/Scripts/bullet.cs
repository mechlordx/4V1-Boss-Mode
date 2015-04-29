using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public float force = 15000f;
	public float speed = 55f; 
	public Quaternion angle;
	// Use this for initialization
	void Awake () {

		Destroy (gameObject, 10f);
		if(GameObject.Find ("Boss"))
		{
			boss_control bossRef = GameObject.Find ("Boss").GetComponent<boss_control> ();
			force = force * bossRef.forceFactor;
			speed = speed * bossRef.projectileFactor;
			//angle = Quaternion.Euler(0f,GameObject.Find ("Boss").transform.rotation.y,0f);
			//Debug.Log ("angle of boss: " + GameObject.Find ("Boss").transform.rotation.y + " angle of bullet: " + angle.y);
			//transform.rotation = angle;
			transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
		}
	}

	public void redo()
	{
		boss_control bossRef = GameObject.Find ("Boss").GetComponent<boss_control> ();
		force = force * bossRef.forceFactor;
		speed = speed * bossRef.projectileFactor;
	}

	// Update is called once per frame
	void Update () {
		//transform.rotation= angle;
		transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
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
		thing.GetComponent<Rigidbody>().AddForce(transform.forward * force);
		Destroy (gameObject, 0f);
	}
	void destroy (){
		Destroy(gameObject);
	}
}

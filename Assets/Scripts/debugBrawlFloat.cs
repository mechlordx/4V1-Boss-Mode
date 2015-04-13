using UnityEngine;
using System.Collections;

public class debugBrawlFloat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(0f, Time.deltaTime * 7f, 0f);
		if(transform.position.y>50f)
			Destroy(gameObject, 0f);
	}
}

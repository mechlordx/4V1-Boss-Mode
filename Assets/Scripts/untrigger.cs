using UnityEngine;
using System.Collections;

public class untrigger : MonoBehaviour {

	public GameObject target;
	public float timer = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(target!=null)
		{
			timer += -Time.deltaTime;
			if(timer<0f)
			{
				target.collider.isTrigger = false;
				Destroy (gameObject, 0f);
			}
		}
		else
			Destroy (gameObject, 0f);
	}
}

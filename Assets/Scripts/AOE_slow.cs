using UnityEngine;
using System.Collections;

public class AOE_slow : MonoBehaviour {
	float timer = 10f;
	float timerReset = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(renderer.material.color.a == 1f)
		{
			timer -= Time.deltaTime;
		}

		if(timer >= 0)
		{
			Destroy(gameObject);
		}
	}
}

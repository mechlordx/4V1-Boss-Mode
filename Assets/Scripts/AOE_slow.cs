using UnityEngine;
using System.Collections;

public class AOE_slow : MonoBehaviour {
	float timer = 10f;
	float timerReset = 10f;

	public bool isOn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(renderer.material.color.a == 1f)
		{
			timer -= Time.deltaTime;
		}
		else
			timer = 10f;

		if(timer <= 0)
		{
			Destroy(gameObject);
			print("asdf");
			timer = 90001;
		}
	}
}

using UnityEngine;
using System.Collections;

public class AOE_slow : MonoBehaviour {
	float timer = 10f;
	float timerReset = 10f;

	Boss4 B4;

	public bool isOn = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		if(timer <= 0)
		{
			Destroy(gameObject);

		}

	}
}

using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	public Vector3 toRotate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (toRotate, Space.World);
	}
}

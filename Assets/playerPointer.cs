using UnityEngine;
using System.Collections;

public class playerPointer : MonoBehaviour {

	public GameObject otherPlayer;
	float far = 10f;
	float close = 5f;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.zero;
		float dis = Vector3.Distance (otherPlayer.transform.position, transform.position);
		float newAlpha = 0f;

		if(dis<=close)
			newAlpha = 1f;
		else if(dis<far)
			newAlpha = 1f - ((dis - close) / (far - close));
		Debug.Log (newAlpha);
		Renderer[] children = GetComponentsInChildren<Renderer>();
		foreach(Renderer child in children)
		{
			Color oldcolor = child.material.color;
			oldcolor.a = newAlpha;
			child.material.color = oldcolor;
		}
		transform.localPosition = Vector3.zero;
		transform.LookAt(new Vector3(otherPlayer.transform.position.x,
		                             transform.position.y,
		                             otherPlayer.transform.position.z));
		transform.position += transform.forward * 0.6f;
	}
}

using UnityEngine;
using System.Collections;

public class readyMatrix : MonoBehaviour {

	public bool[] readyMat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int playerCount()
	{
		int a = 0;
		for(int x=0;x<readyMat.Length;x++)
		{
			if(readyMat[x])
				a+=1;
		}
		return a;
	}
}

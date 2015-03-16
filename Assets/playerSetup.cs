using UnityEngine;
using System.Collections;

public class playerSetup : MonoBehaviour {

	public GameObject readyBanner;
	int readyPlayers = 0;
	bool allReady = false;
	player_controls controlsRef;
	bool[] readyMatrix;

	// Use this for initialization
	void Awake () {
		controlsRef = GameObject.Find ("Game Controller").GetComponent<player_controls> ();
	}
	
	// Update is called once per frame
	void Update () {
		allReady = true;
		readyPlayers = 0;
		readyMatrix = new bool[4];
		for(int x=0;x<readyMatrix.Length;x++)
			readyMatrix[x] = false;
		GameObject[] possiblePlayers = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in possiblePlayers)
		{
			if(player.GetComponent<playerReady>().readyUp)
			{
				readyPlayers += 1;
				readyMatrix[player.GetComponent<playerReady>().playerNumber] = true;
			}
			else if(player.GetComponent<playerReady>().ready)
				allReady = false;
		}

		if(allReady && readyPlayers > 1)
		{
			readyBanner.GetComponent<Renderer>().enabled = true;
			for(int x=0;x<readyMatrix.Length;x++)
			{
				if(readyMatrix[x])
				{
					if(controlsRef.getButton(x, 0, 1))
						GameObject.Find ("Game Controller").GetComponent<loader>().loadScene(3);
				}
			}
		}
		else
			readyBanner.GetComponent<Renderer>().enabled = false;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerSetup : MonoBehaviour {

	public GameObject readyBanner;
	int readyPlayers = 0;
	bool allReady = false;
	player_controls controlsRef;
	bool[] readyMatrix;

	public Image readyImage;
	public Sprite neutral;
	public Sprite ready;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
		controlsRef = GameObject.Find ("GameController").GetComponent<player_controls> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!Application.isLoadingLevel && Application.loadedLevelName=="game_menu")
		{
			GameObject.Find ("menu object").GetComponent<game_menu_script>().playerSelector = readyMatrix;
			Destroy (gameObject);
		}
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
			if(readyBanner!=null)
				readyBanner.GetComponent<Renderer>().enabled = true;

			if(readyImage.sprite == neutral)
				readyImage.sprite = ready;

			for(int x=0;x<readyMatrix.Length;x++)
			{
				if(readyMatrix[x])
				{
					if(controlsRef.getButton(x, 0, 1))
					{
						GameObject.Find ("GameController").GetComponent<readyMatrix>().readyMat = readyMatrix;
						GameObject.Find ("GameController").GetComponent<loader>().loadScene(1);
					}
				}
			}
		}
		else if(readyBanner!=null){
			readyBanner.GetComponent<Renderer>().enabled = false;
			if(readyImage.sprite == ready)
				readyImage.sprite = neutral;
		}
	}
}

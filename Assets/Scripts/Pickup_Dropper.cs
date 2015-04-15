using UnityEngine;
using System.Collections;

public class Pickup_Dropper : MonoBehaviour {
	Pickup_Controller PickCon;
	player_move PM;
	score sPage;
	
	Vector3 Pos;
	
	float iterator = 9001f;

	public int maxPickups = 4;
	int PickupType;
	public int PickupsOnGround = 0;
	// Use this for initialization
	void Start () {
		sPage = GameObject.Find("GameController").GetComponent<score>();
	}
	
	// Update is called once per frame
	void Update () {
		if(sPage.timer <= iterator && PickupsOnGround <= maxPickups)
			{
				Pos = new Vector3(Random.Range(-14,14), 0.5f, Random.Range(-14,14));

				foreach(GameObject players in GameObject.FindGameObjectsWithTag("Player"))
				{
					if((Mathf.Abs(Vector3.Distance(Pos,players.transform.position))<=5))
				 	  return;
				}
				if(Mathf.Abs(Vector3.Distance(Pos,GameObject.Find ("Boss").transform.position))<=5)
					return;
				
				PickupType = Random.Range(1,5);
				GameObject temp = (GameObject)Instantiate(Resources.Load ("Prefabs/Pickup " + PickupType));
			PickupsOnGround++;
				temp.GetComponent<Pickup_Controller>().PickupType = PickupType;
				temp.transform.position = Pos;

				iterator = sPage.timer - 15f;
			}
	}
}

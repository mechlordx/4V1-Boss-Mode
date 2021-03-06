﻿using UnityEngine;
using System.Collections;

public class ladder : MonoBehaviour {

	public GameObject attachedPlayer;
	public GameObject LadderLook;
	public float climbspeed;
	public float heightlimit;
	public bool locked = false;
	GameObject thrower;
	GameObject attachedCamera;
	int throwtimer = 0;
	int maxthrowtimer = 60;
	float drag;
	Vector3 InitPos;
	// Use this for initialization
	void Start () {
		LadderLook = GameObject.FindGameObjectWithTag("ladderlook");
	}
	
	// Update is called once per frame
	void Update () {
		if(attachedPlayer!=null && throwtimer<0)
		{
			InitPos = attachedPlayer.transform.position;
			//attachedPlayer.transform.position += new Vector3(0f, climbspeed*Time.deltaTime, 0f);
			attachedPlayer.transform.position= Vector3.MoveTowards(attachedPlayer.transform.position, LadderLook.transform.position, climbspeed*Time.deltaTime);
			attachedPlayer.transform.LookAt(new Vector3(0,attachedPlayer.transform.position.y,0));
			Mathf.Abs(Vector3.Distance(attachedPlayer.transform.position, InitPos));
			if(attachedPlayer.transform.position == LadderLook.transform.position)
			{
				Debug.Log("asdklfjsd;lkf");
				attachedPlayer.transform.position = GameObject.Find ("Cockpit").transform.position;
				unattach();
				//locked = true;
			}
			else if(attachedPlayer.transform.position.y<-5f)
				unattach();
		}
		else if(throwtimer==30) // Throwdown
		{
			GameObject a = thrower;
			GameObject b = attachedPlayer;
			Vector3 throwerpos = thrower.transform.position;
			Vector3 attachedpos = attachedPlayer.transform.position;
			thrower.transform.position = new Vector3(attachedpos.x,
			                                         throwerpos.y,
			                                         attachedpos.z);
			attachedPlayer.transform.position = new Vector3(throwerpos.x,
			                                                attachedpos.y,
			                                                throwerpos.z);
			attachedPlayer.transform.eulerAngles = thrower.transform.eulerAngles;

			Vector3 force = Vector3.Normalize(throwerpos - attachedpos);
			force += new Vector3(0f,-force.y,0f);
			force += new Vector3(0f, .1f, 0f);
			attachedPlayer.GetComponent<Rigidbody>().AddForce(force * 10000f);

			unattach();
			attach(thrower);
			thrower = null;
		}
		else if(throwtimer==1)
			locked = false;
		else if(thrower!=null)
			thrower.GetComponent<Rigidbody>().velocity = Vector3.zero;
		throwtimer += -1;
	}

	void OnTriggerEnter(Collider other)
	{
		if(attachedPlayer==null && !locked)
		{
			if(other.gameObject.name.Contains("Player"))
				attach (other.gameObject);
		}
	}

	public void attach(GameObject newplayer)
	{
		if(attachedPlayer==null)
		{
			newplayer.GetComponent<player_move>().animationModel.GetComponent<Animator>().SetTrigger("Rising");
			attachedPlayer = newplayer;
			if(attachedPlayer.GetComponent<Rigidbody>().drag!=999f)
				drag = attachedPlayer.GetComponent<Rigidbody>().drag;
			attachedPlayer.transform.position = new Vector3(transform.position.x + 0.001f,
			                                           newplayer.transform.position.y,
			                                           transform.position.z);
			attachedPlayer.GetComponent<Collider>().isTrigger = true;
			attachedPlayer.GetComponent<Rigidbody>().useGravity = false;
			attachedPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
			attachedPlayer.transform.eulerAngles = new Vector3(0f, 270f, 0f);
			attachedPlayer.GetComponent<Rigidbody>().drag = 999f;
			attachedPlayer.GetComponent<player_move>().enabled = false;
		}
	}

	public void throwplayer(GameObject newthrower)
	{
		if(throwtimer>0)
			return;
		throwtimer = maxthrowtimer;
		//locked = true;
		thrower = newthrower;
		thrower.transform.LookAt (attachedPlayer.transform.position);
		thrower.transform.eulerAngles = new Vector3 (0f,
		                                            thrower.transform.eulerAngles.y,
		                                            0f);
		thrower.GetComponent<Rigidbody>().drag = 999f;

	}

	public void unattach()
	{
		if(attachedPlayer!=null)
		{
			attachedPlayer.GetComponent<Collider>().isTrigger = false;
			attachedPlayer.GetComponent<Rigidbody>().useGravity = true;
			attachedPlayer.GetComponent<player_move>().enabled = true;
			attachedPlayer.GetComponent<Rigidbody>().drag = drag;
			attachedPlayer = null;
		}
	}

	public bool checkattach(GameObject check)
	{
		if(attachedPlayer==null)
			return false;
		if(check.GetComponent<player_move>().playerNumber==attachedPlayer.GetComponent<player_move>().playerNumber)
		{
			unattach();
			return true;
		}
		return false;
	}
}

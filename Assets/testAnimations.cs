using UnityEngine;
using System.Collections;

public class testAnimations : MonoBehaviour {

	public bool running = false;
	public bool punching = false;
	public bool dashing = false;
	public bool bossing = false;
	public bool rising = false;
	public bool spawn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Animator animator = GetComponent<Animator> ();
		animator.SetBool("Running", running);
		animator.SetBool("Bossing", bossing);

		if(animator.GetFloat("PunchingTrigger")==1f)
			animator.SetBool("Punching", false);
		if(animator.GetFloat("DashingTrigger")==1f)
			animator.SetBool("Dashing", false);

		if (punching)
		{
			animator.SetTrigger ("Punch");
			animator.SetBool("Punching", true);
		}
		if (dashing)
		{
			animator.SetTrigger ("Dash");
			animator.SetBool("Dashing", true);
		}
		if (rising)
			animator.SetTrigger ("Rising");
		if (spawn)
			animator.SetTrigger ("Spawn");

		punching = false;
		dashing = false;
		rising = false;
		spawn = false;
	}
}

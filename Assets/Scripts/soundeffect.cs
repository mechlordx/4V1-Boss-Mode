using UnityEngine;
using System.Collections;

public class soundeffect : MonoBehaviour {

	public AudioClip[] sound1;
	public AudioClip[] sound2;
	public AudioClip[] sound3;
	public AudioClip[] sound4;
	public AudioClip[] sound5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void play(int toplay)
	{
		if((toplay==1)&&(sound1.Length>0))
			audio.PlayOneShot (sound1 [Random.Range (0, sound1.Length)]);
		else if((toplay==2)&&(sound2.Length>0))
			audio.PlayOneShot (sound2 [Random.Range (0, sound2.Length)]);
		else if((toplay==3)&&(sound3.Length>0))
			audio.PlayOneShot (sound3 [Random.Range (0, sound3.Length)]);
		else if((toplay==4)&&(sound4.Length>0))
			audio.PlayOneShot (sound4 [Random.Range (0, sound4.Length)]);
		else if((toplay==5)&&(sound5.Length>0))
			audio.PlayOneShot (sound5 [Random.Range (0, sound5.Length)]);
	}
}

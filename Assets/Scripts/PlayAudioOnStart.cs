using UnityEngine;
using System.Collections;

public class PlayAudioOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		AudioSource audioSource = GetComponent<AudioSource>();
		if(audioSource) audioSource.PlayDelayed(1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

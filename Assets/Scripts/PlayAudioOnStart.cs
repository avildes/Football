using UnityEngine;
using System.Collections;

public class PlayAudioOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		AudioSource audioSource = GetComponent<AudioSource>();
		if(audioSource) if(!Application.isLoadingLevel)audioSource.PlayDelayed(.5f);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

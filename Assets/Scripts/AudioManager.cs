using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AudioManager : MonoBehaviour 
{
	private AudioSource musicChannel;

	private AudioSource audioChannel;

	private static AudioManager instance;

	// Use this for initialization
	void Start () 
	{
		if(instance == null)
		{
			musicChannel = GetComponent<AudioSource>();
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

	}

	public void PlayMusic()
	{
		musicChannel.Play();
	}

	public void Toggle () 
	{
		if(AudioListener.volume == 0) Unmute();
		else 						  Mute();
	}

	public void Mute () 
	{
		AudioListener.volume = 0;
	}

	public void Unmute()
	{
		AudioListener.volume = 1;
	}
}

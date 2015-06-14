using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Music{Test, Jump, Door};

public enum Sfx{Coin};

public class AudioManager : MonoBehaviour 
{
	public static AudioManager instance;

	public AudioClip testMusic;

	public AudioClip coinSfx;

	private AudioSource musicChannel;

	private AudioSource[] sfxChannels;

	private const string audioPath = "common/audio/...";

	[SerializeField] private Dictionary<string, AudioClip> musicClips;
	private Dictionary<string, AudioClip> sfxClips;


	// Use this for initialization
	void Start () 
	{
		if(instance == null)
		{
			//Create audio channels
			musicChannel = gameObject.AddComponent<AudioSource>();
			sfxChannels = new AudioSource[3];
			for(int i = 0; i < 3; i++) sfxChannels[i] = gameObject.AddComponent<AudioSource>();

			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	
	}

	private void LoadMusics()
	{
	}

	private void OnMusicLoaded()
	{

	}

	private void LoadSfxs()
	{

	}

	private void OnSfxsLoaded()
	{

	}


	private void LoadAudio()
	{
		//Load audio files from the audioPath and save reference on a hash for later use.
	}

	public void PlaySfx(Sfx sfxId)
	{
		if (sfxId == Sfx.Coin)
			sfxChannels[0].clip = coinSfx;
		sfxChannels[0].Play ();
	}


	public void PlayMusic(Music id)
	{
		if (id == Music.Test) musicChannel.clip = testMusic;

		musicChannel.Play ();
	}

	public void ToggleAudio()
	{
		if (IsMuted ()) Unmute();
		else 			Mute();
	}

	public void Mute()
	{		
		AudioListener.volume = 0f;
	}

	public void Unmute()
	{
		AudioListener.volume = 1f;
	}

	public bool IsMuted()
	{
		return (AudioListener.volume == 0f);
	}

}

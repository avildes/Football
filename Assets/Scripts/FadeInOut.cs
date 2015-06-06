using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class FadeInOut : MonoBehaviour
{

	public static FadeInOut instance;

	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

	[SerializeField] private Image image;

	private enum FadeState{Idle, ToClear, ToBlack}; 

	private FadeState state;

	private GameObject fadeToBlackCallback;
	private string fadeToBlackCallbackFunction;
	
	void Awake ()
	{
		if(instance == null)
		{
			image.enabled = true;
			state = FadeState.ToClear;
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

	}

	void Update ()
	{
		if(state == FadeState.ToClear) 			StartScene();
		else if (state == FadeState.ToBlack) 	EndScene();

		//Else do nothing
	}

	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	private void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(image.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			image.color = Color.clear;
			image.enabled = false;
			
			// The scene is no longer starting.
			state = FadeState.Idle;
		}
	}

	private void EndScene ()
	{
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if (image.color.a >= 0.95f) 
		{	// ... reload the level.
//			Application.LoadLevel (0);
			fadeToBlackCallback.SendMessage(fadeToBlackCallbackFunction);
			state = FadeState.Idle;
		}
	}

	public void StartFadeToBlack(GameObject fadeToBlackCallback, string fadeToBlackCallbackFunction)
	{
		// Make sure the texture is enabled.
		image.enabled = true;

		this.fadeToBlackCallback = fadeToBlackCallback;
		this.fadeToBlackCallbackFunction = fadeToBlackCallbackFunction;

		state = FadeState.ToBlack;
	}
}
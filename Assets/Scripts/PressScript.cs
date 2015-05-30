using UnityEngine;
using System.Collections;

public class PressScript : MonoBehaviour
{
	private enum State{ListeningForInput, WaitingFadeOut};
	private State state;

	[SerializeField] private string sceneName;
	
	void Start ()
	{
		state = State.ListeningForInput;
	}
	
	void Update ()
	{
		if (state == State.ListeningForInput) 
		{
			if (Input.touchCount == 1 || Input.GetKeyDown (KeyCode.Space)) 
			{
				FadeInOut.instance.StartFadeToBlack (gameObject, "OnFadeOutComplete");
				state = State.WaitingFadeOut;
			}
			
			if (Input.GetKeyDown (KeyCode.Escape)) 
			{
				FadeInOut.instance.StartFadeToBlack (gameObject, "OnFadeOutComplete");
				state = State.WaitingFadeOut;
			}
		}
	}
	
	
	private void OnFadeOutComplete()
	{
//		AnalyticsManager.Instance.LogSceneTransition(Application.loadedLevelName, sceneName);
		Application.LoadLevel(sceneName);
	}
	

}

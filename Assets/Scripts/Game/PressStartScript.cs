using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class PressStartScript : MonoBehaviour
{
	public float timer;

	private enum State{ListeningForInput, WaitingFadeOut};
	private State state;

	void Start ()
    {
		timer = 0;

		AnalyticsManager.instance.LogScene("Splash");
		state = State.ListeningForInput;

        //StartCoroutine(Flash());
	}
	
	void Update ()
    {
		timer += Time.deltaTime;

		if (state == State.ListeningForInput) 
		{
			if (Input.touchCount == 1 || Input.GetKeyDown (KeyCode.Space)) 
			{
				FadeInOut.instance.StartFadeToBlack (gameObject, "OnFadeOutComplete");
				state = State.WaitingFadeOut;
				//			SaveTimer();
				//			AnalyticsManager.Instance.LogSceneTransition("Splash", "Game");
				//            Application.LoadLevel("game");
			}

			if (Input.GetKeyDown (KeyCode.Escape)) {
				SaveTimer ();
				AnalyticsManager.instance.LogSceneTransition ("Splash", "Quit");
				Application.Quit ();
			}
		}
	}

	private void OnFadeOutComplete()
	{
		SaveTimer();
		AnalyticsManager.instance.LogSceneTransition("Splash", "Game");
		Application.LoadLevel("game");
	}

	void SaveTimer()
	{
		AnalyticsManager.instance.LogTimeSpent("Time Spent on Splash Screen", (int) timer);
	}

    IEnumerator Flash()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(.5f);
        

        StartCoroutine(Flash());
    }
}

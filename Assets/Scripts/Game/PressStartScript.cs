using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class PressStartScript : MonoBehaviour
{
	public float timer;

	void Start ()
    {
		timer = 0;

		AnalyticsManager.Instance.LogScene("Splash");
        StartCoroutine(Flash());
	}
	
	void Update ()
    {
		timer += Time.deltaTime;

	    if(Input.touchCount == 1 || Input.GetKeyDown(KeyCode.Space))
        {
			SaveTimer();
			AnalyticsManager.Instance.LogSceneTransition("Splash", "Game");
            Application.LoadLevel("game");
        }

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SaveTimer();
			AnalyticsManager.Instance.LogSceneTransition("Splash", "Quit");
			Application.Quit();
		}
	}

	void SaveTimer()
	{
		AnalyticsManager.Instance.LogTimeSpent("Time Spent on Splash Screen", (int) timer);

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

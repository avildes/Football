using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour
{
	private string sceneToGo;
	private float timeElapsed;
	
	void Start ()
	{
		if(Application.isMobilePlatform)  AnalyticsManager.Instance.LogScene(Application.loadedLevelName);
	}

	void Update()
	{
		timeElapsed += Time.deltaTime;
	}

	public void To(string sceneName)
	{
		this.sceneToGo = sceneName;
		FadeInOut.instance.StartFadeToBlack (gameObject, "OnFadeOutComplete");
	}

	void SaveTimer()
	{
		if(Application.isMobilePlatform) AnalyticsManager.Instance.LogTimeSpent("Time Spent on " + Application.loadedLevelName + " Screen", (int) timeElapsed);
	}
	
	
	private void OnFadeOutComplete()
	{
		if(Application.isMobilePlatform) AnalyticsManager.Instance.LogSceneTransition (Application.loadedLevelName, sceneToGo);
		Application.LoadLevel(sceneToGo);
	}
	

}

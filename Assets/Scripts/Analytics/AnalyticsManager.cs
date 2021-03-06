﻿using UnityEngine;
using System.Collections;

public class AnalyticsManager : MonoBehaviour
{
    /// <summary>
    /// Google Analytics Variable
    /// </summary>
    public GoogleAnalyticsV3 googleAnalytics;

    public static AnalyticsManager instance;

    void Awake()
    {

		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this); 
		} 
		else
		{
			Destroy(gameObject);
		}
        
    }

	void Start ()
    {
		if(instance == null) instance = this;
	}

    public void LogScene(string sceneName)
    {
        googleAnalytics.LogScreen(sceneName);
    }

	public void LogSceneTransition(string oldScene, string newScene)
	{
		EventHitBuilder eventLog = new EventHitBuilder();
		eventLog.SetEventCategory("Conversion");
		eventLog.SetEventAction("Scene Transition");
		eventLog.SetEventLabel("Transition from: " + oldScene + " to: " + newScene);
		googleAnalytics.LogEvent(eventLog);
	}

	public void LogTimeSpent(string eventName, int timeSpent)
	{
        EventHitBuilder eventLog = new EventHitBuilder();
        eventLog.SetEventCategory(eventName);
		eventLog.SetEventAction("TimeSpent");
        eventLog.SetEventValue(timeSpent);
        googleAnalytics.LogEvent(eventLog);
    }

    public void LogHiScore(int newScore)
    {
        EventHitBuilder eventLog = new EventHitBuilder();
        eventLog.SetEventCategory("EndGame");
        eventLog.SetEventAction("New Record");
        eventLog.SetEventValue(newScore);
        googleAnalytics.LogEvent(eventLog);
    }
}

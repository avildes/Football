using UnityEngine;
using System.Collections;

public class AnalyticsManager : MonoBehaviour
{
    /// <summary>
    /// Google Analytics Variable
    /// </summary>
    public GoogleAnalyticsV3 googleAnalytics;

    public static AnalyticsManager Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

	void Start ()
    {
		if(Instance == null) Instance = this;   
	}

    public void LogScene(string sceneName)
    {
        googleAnalytics.LogScreen(sceneName);
    }

    public void LogScore(int score)
    {
        EventHitBuilder eventLog = new EventHitBuilder();
        eventLog.SetEventCategory("EndGame");
        eventLog.SetEventAction("Score");
        eventLog.SetEventValue(score);
        googleAnalytics.LogEvent(eventLog);
    }

    public void LogHiScore(int newScore)
    {
        EventHitBuilder eventLog = new EventHitBuilder();
        eventLog.SetEventCategory("EndGame");
        eventLog.SetEventAction("New HiScore");
        eventLog.SetEventValue(newScore);
        googleAnalytics.LogEvent(eventLog);
    }
}

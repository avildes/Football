using UnityEngine;
using System.Collections;

public class WaitAndLoadNewLevel : MonoBehaviour
{
	public string level;

	public float seconds;

	IEnumerator WaitAndLoad ()
	{
		yield return new WaitForSeconds(seconds);
		Application.LoadLevel(level);
	}

	void Start ()
	{
		StartCoroutine(WaitAndLoad());
	}
}

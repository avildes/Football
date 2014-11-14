using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
	public string level;

	void OnTouchDown ()
	{
		Application.LoadLevel(level);
	}
}

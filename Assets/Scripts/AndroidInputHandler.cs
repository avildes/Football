using UnityEngine;
using System.Collections;

public class AndroidInputHandler : MonoBehaviour 
{
	[SerializeField] private bool workOnPC;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (workOnPC || Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
			{
				Application.Quit();
				return;
			}
		}
	}
}

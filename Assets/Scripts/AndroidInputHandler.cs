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
			if (Input.GetKey(KeyCode.Home)  || Input.GetKey(KeyCode.Menu))
			{
				Application.Quit();
				return;
			}
			else if(Input.GetKey(KeyCode.Escape))
			{
				if(Application.loadedLevelName == "Game") 		    Application.LoadLevel("Splash");
				else if(Application.loadedLevelName == "Splash") 	Application.Quit();
				else if(Application.loadedLevelName == "Credits") 	Application.LoadLevel("Splash");
			}
		}
	}
}

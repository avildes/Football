using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BabelManager : MonoBehaviour 
{
	public static BabelManager instance;

	private Dictionary<string, string> texts;
	
	[SerializeField] private bool overrideSystLang;
	[SerializeField] private SystemLanguage language;

	private bool initialized;

	// Use this for initialization
	void Start () 
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(instance);
			LoadTexts();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void LoadTexts()
	{
		if(!overrideSystLang) language = Application.systemLanguage;

		texts = new Dictionary<string, string>();


		//TODO: Load the correct language JSON file

		if(language == SystemLanguage.Portuguese)
		{
			texts.Add("credits", "Credits");
			texts.Add("creditsGD", "- Game Design -\nPietro Amaral");
			texts.Add("creditsArt", "- Art -\nArthur \"Pirata\"" );
			texts.Add("creditsDev1", "- Programming -\nAntonio Vildes");
			texts.Add("creditsDev2", "- Additional Programming -\nSilvio Carrera");
			texts.Add("creditsMusic", "- Music/Sfx -\nLuiz Manghi");
		}

		initialized = true;
	}
	
	public string GetText(string stringKey)
	{
		return texts[stringKey];
	}

	public bool IsInitialized()
	{
		return initialized;
	}
}

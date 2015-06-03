using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

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
			TextAsset mJson = Resources.Load("strings_pt") as TextAsset;
			var strings = JSON.Parse(mJson.text);


			texts.Add("credits", strings["credits"].Value);
			texts.Add("creditsGD", strings["creditsGD"].Value);
			texts.Add("creditsArt", strings["creditsArt"].Value);
			texts.Add("creditsDev1", strings["creditsDev1"].Value);
			texts.Add("creditsDev2", strings["creditsDev2"].Value);
			texts.Add("creditsMusic", strings["creditsMusic"].Value);
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

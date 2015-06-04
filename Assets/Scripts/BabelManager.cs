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




		if(language == SystemLanguage.Portuguese)
		{
			TextAsset mJson = Resources.Load("strings_pt") as TextAsset;
			JSONNode nodes = JSON.Parse(mJson.text);


			//TODO: Mount dictionary dinamicaly
//			for(int i = 0; i < nodes.Count; i++)
//			{
//
//				texts.Add(nodes[i], nodes[i].Value);
//			}
			texts.Add("credits", nodes["credits"].Value);
			texts.Add("creditsGD", nodes["creditsGD"].Value);
			texts.Add("creditsArt", nodes["creditsArt"].Value);
			texts.Add("creditsDev", nodes["creditsDev"].Value);
			texts.Add("creditsMusic", nodes["creditsMusic"].Value);
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

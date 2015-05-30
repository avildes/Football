using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BabelManager : MonoBehaviour 
{
	public static BabelManager instance;

	private Dictionary<string, string> texts;
	
	[SerializeField] private bool overrideSystLang;
	[SerializeField] private SystemLanguage language;

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
			texts.Add("credits", "Creditos");
			texts.Add("creditsGD", "Game Design \nPietro Amaral");
			texts.Add("creditsDev1", "Programming \nAntonio Vieldes");
			texts.Add("creditsArt", "Art \nArtur \"Pirata\"" );
			texts.Add("creditsDev2", "Additional Programming \nSilvio Carrera");
		}
	
	}
	
	public string GetText(string stringKey)
	{
		return texts[stringKey];
	}
}

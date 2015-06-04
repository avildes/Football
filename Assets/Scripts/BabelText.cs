using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BabelText : MonoBehaviour
{
	private Text textfield;
	[SerializeField] private string stringKey;
	[SerializeField] private bool showInEdit;

	private bool initialized;

	// Use this for initialization
	void Start () 
	{
		textfield = gameObject.GetComponent<Text>();

		if(BabelManager.instance)
		{
			initialized = BabelManager.instance.IsInitialized();
			if(stringKey.Length > 0) if(initialized) LoadText();
		}
	}

	void Update()
	{
		if(!initialized)
		{
			initialized = BabelManager.instance.IsInitialized();
			if(initialized)  LoadText();
		}
	}

	private void LoadText()
	{
		textfield.text = BabelManager.instance.GetText(stringKey);
	}
}

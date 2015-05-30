using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BabelText : MonoBehaviour
{
	private Text textfield;
	[SerializeField] private string stringKey;
	[SerializeField] private bool showInEdit;

	// Use this for initialization
	void Start () 
	{
		textfield = gameObject.GetComponent<Text>();
		if(stringKey.Length > 0) textfield.text = BabelManager.instance.GetText(stringKey);
	}


}

using UnityEngine;
using System.Collections;

public class TeamSelectionButton : MonoBehaviour 
{
	public GameObject teamController;

	public string button;

	void OnTouchDown ()
	{
		teamController.SendMessage("ChangeTeam", button, SendMessageOptions.DontRequireReceiver);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamSelection : MonoBehaviour
{
	public GameObject homeTeam;

	public GameObject teamTeam;
	
    private Texture2D targetTexture;

	private Texture2D sourceTexture;

    void ChangeTeam(string value)
    {
        switch(value)
		{
		case "":
			break;
		}

    }

	private void PaintBaseTexture(Texture2D source)
	{
		targetTexture.SetPixels32(sourceTexture.GetPixels32());
		targetTexture.Apply();
	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamSelection : MonoBehaviour
{
	public int qtdTeams;

	public Texture2D baseTextureHome;
	public Texture2D baseTextureAway;

	private int selectedHome;
	private int selectedAway;

	void Start()
	{
		selectedHome = GetHomeTeam();
		selectedAway = GetAwayTeam();

		PaintSelected();
	}

	/// <summary>
	/// Paints the selected teams.
	/// </summary>
	private void PaintSelected()
	{
		string textureName = "Teams/Away/Away"+selectedAway;
		Texture2D awaySrc = Resources.Load<Texture2D>(textureName);

		if(awaySrc != null)	PaintBaseTexture(awaySrc, baseTextureAway);
		else Debug.Log("Null: " + textureName);

		
		textureName = "Teams/Home/Home"+selectedHome;
		Texture2D homeSrc = Resources.Load<Texture2D>(textureName);

		if(homeSrc != null)	PaintBaseTexture(homeSrc, baseTextureHome);
		else Debug.Log("Null: " + textureName);
	}

	/// <summary>
	/// Receives the input from the buttons and changes the team accordingly.
	/// </summary>
	/// <param name="value">Value.</param>
    public void ChangeTeam(string value)
    {
        switch(value)
		{
			case "HomeRight":
				selectedHome = mod (selectedHome+1, qtdTeams);
				break;
			case "HomeLeft":
				selectedHome = mod (selectedHome-1, qtdTeams);
				break;
			case "AwayRight":
				selectedAway = mod (selectedAway+1, qtdTeams);
				break;
			case "AwayLeft":
				selectedAway = mod (selectedAway-1, qtdTeams);
				break;
		}

		SetHomeTeam(selectedHome);
		SetAwayTeam(selectedAway);

		PaintSelected();
    }

	/// <summary>
	/// Paints the base texture.
	/// </summary>
	/// <param name="source">Source.</param>
	/// <param name="targetTexture">Target texture.</param>
	private void PaintBaseTexture(Texture2D source, Texture2D targetTexture)
	{
		targetTexture.SetPixels32(source.GetPixels32());
		targetTexture.Apply();
	}

	#region Util
	/// <summary>
	/// Returns the mod between a and b.
	/// </summary>
	/// <param name="a">The alpha component.</param>
	/// <param name="b">The blue component.</param>
	private int mod(int a, int b)
	{
		int mod = a % b;
		mod = mod < 0 ? mod + b : mod;
		return mod;
	}
	#endregion

	#region PlayerPrefs
	private int GetHomeTeam()
	{
		if (PlayerPrefs.HasKey("HomeTeam"))
		{
			return PlayerPrefs.GetInt("HomeTeam");	
		}
		return 0;
	}
	
	private int GetAwayTeam()
	{
		if (PlayerPrefs.HasKey("AwayTeam"))
		{
			return PlayerPrefs.GetInt("AwayTeam");	
		}
		return 0;
	}
	
	private void SetHomeTeam(int _team)
	{
		PlayerPrefs.SetInt("HomeTeam", _team);
	}
	
	private void SetAwayTeam(int _team)
	{
		PlayerPrefs.SetInt("AwayTeam", _team);
	}
	#endregion

}
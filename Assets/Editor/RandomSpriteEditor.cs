using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RandomSprite))]
public class RandomSpriteEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		RandomSprite myScript = (RandomSprite)target;
		if(GUILayout.Button("Randomize"))
		{
			myScript.Randomize();
		}
	}
}
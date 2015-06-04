using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSprite : MonoBehaviour 
{
	[SerializeField] private Sprite[] spritePool;
	[SerializeField] private SpriteRenderer[] targets;
	[SerializeField] private bool dontRepeat;

	private int lastRandomValue = -1;

	// Use this for initialization
	void Start () 
	{
		
	}
	

	public void Randomize () 
	{
		List<int> randomValues = new List<int>();
		for(int i = 0; i < spritePool.Length; i++)
		{
			if(dontRepeat && i == lastRandomValue) continue;

			randomValues.Add(i);
		}

		int newRandomValue = Random.Range(0, randomValues.Count);
		lastRandomValue = newRandomValue;
//		print (lastRandomValue);

		for(int i = 0; i < targets.Length; i++) targets[i].sprite = spritePool[lastRandomValue];
	}
}

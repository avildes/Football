using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomPrefab : MonoBehaviour 
{
	[SerializeField] private GameObject[] prefabPool;
	[SerializeField] private GameObject[] targets;
	[SerializeField] private bool dontRepeat;
	
	private int lastRandomValue = -1;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	
	public void Randomize () 
	{
		List<int> randomValues = new List<int>();
		for(int i = 0; i < prefabPool.Length; i++)
		{
			if(dontRepeat && i == lastRandomValue) continue;
			
			randomValues.Add(i);
		}
		
		int newRandomValue = Random.Range(0, randomValues.Count);
		lastRandomValue = newRandomValue;
		//		print (lastRandomValue);
		
		for(int i = 0; i < targets.Length; i++) targets[i] = prefabPool[lastRandomValue];
	}

	public void To(int id)
	{
		if(id > 0 && id < prefabPool.Length) 
		{
			lastRandomValue = id;
			for(int i = 0; i < targets.Length; i++) targets[i] = prefabPool[lastRandomValue];
		}
	}
}

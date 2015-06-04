using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomAnimator : MonoBehaviour 
{
	[SerializeField] private AnimatorOverrideController[] animatorPool;
	[SerializeField] private Animator[] targets;
	[SerializeField] private bool dontRepeat;
	[SerializeField] private bool onStart;
	
	private int lastRandomValue = -1;
	
	// Use this for initialization
	void Start () 
	{
		if(onStart) Randomize();
	}
	
	
	public void Randomize () 
	{
		List<int> randomValues = new List<int>();
		for(int i = 0; i < animatorPool.Length; i++)
		{
			if(dontRepeat && i == lastRandomValue) continue;
			
			randomValues.Add(i);
		}
		
		int newRandomValue = Random.Range(0, randomValues.Count);
		lastRandomValue = newRandomValue;
		print (lastRandomValue);
		
		for(int i = 0; i < targets.Length; i++) targets[i].runtimeAnimatorController = animatorPool[lastRandomValue];
	}
}

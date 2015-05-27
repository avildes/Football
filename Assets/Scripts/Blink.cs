using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour 
{

	[SerializeField] private MonoBehaviour targetComponent;

	[SerializeField] private float frequency;
	private float countFrequency;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		countFrequency += Time.deltaTime;

		if(countFrequency >= frequency)
		{
			countFrequency = 0;
			ToggleActive();
		}
	
	}

	private void ToggleActive()
	{
		targetComponent.enabled = !targetComponent.enabled;
	}
}

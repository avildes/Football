using UnityEngine;
using System.Collections;

public class BackgroundControl : MonoBehaviour
{

	public float speed = 2f;
	public float verticalDirection = -1f;

	void Start ()
	{
	
	}

	void FixedUpdate ()
	{
		Transform lastBg = GetLastBgImg();
		Transform firstBg = GetLastBgImg();

		if(lastBg.position.y < -7)
		{
			Vector3 newPos = firstBg.position;
			newPos.y += 3.15f;
			lastBg.position = newPos;
		}

		MoveChildren();
		//transform.Translate(new Vector3(0, speed * verticalDirection * Time.deltaTime, 0));
	}

	void MoveChildren()
	{
		for (int i = 0; i < transform.childCount; i++) 
		{
			transform.GetChild(i).Translate(new Vector3(0, speed * verticalDirection * Time.deltaTime, 0));
		}
	}

	Transform GetFirstBgImg()
	{
		Transform retorno = null;
		
		for (int i = 0; i < transform.childCount; i++) 
		{
			if((retorno == null) || transform.GetChild(i).position.y > retorno.position.y)
			{
				retorno = transform.GetChild(i);
			}
		}
		return retorno;
	}

	Transform GetLastBgImg()
	{
		Transform retorno = null;

		for (int i = 0; i < transform.childCount; i++) 
		{
			if((retorno == null) || transform.GetChild(i).position.y < retorno.position.y)
			{
				retorno = transform.GetChild(i);
			}
		}
		return retorno;
	}
}

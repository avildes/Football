using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundControl : MonoBehaviour
{
	public float speed;
	public float verticalDirection;

    private List<Transform> imgs;
    private Transform first;
    private Transform last;

    private int lastPosition;

    public bool gameOver = false;

    void Update()
    {
        speed = BalanceClass.Instance.bgSpeed;
        verticalDirection = BalanceClass.Instance.bgVerticalDirection;
    }

	void Start ()
	{
        imgs = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            imgs.Add(transform.GetChild(i));
        }

        lastPosition = imgs.Count - 1;
        first = imgs[0];
        last = imgs[lastPosition];
	}

	void FixedUpdate ()
	{
		if(last.position.y < -8)
        {
            Vector3 newPos = first.position;
            newPos.y += 6.30f;
			last.position = newPos;

            first = last;
            last = GetAntecedent();
        }
        
        if(!gameOver)
        {
            MoveChildren();
        }
	}

    private Transform GetAntecedent()
    {
        int pos = (lastPosition - 1) % imgs.Count;
        pos = pos < 0 ? pos + imgs.Count : pos;
        lastPosition = pos;
        return imgs[pos];
    }

	void MoveChildren()
	{
		transform.Translate(new Vector3(0, speed * verticalDirection * Time.deltaTime, 0));
		/*
		for (int i = 0; i < transform.childCount; i++) 
		{
			transform.GetChild(i).Translate(new Vector3(0, speed * verticalDirection * Time.deltaTime, 0));
		}
		*/
	}

    void SetGameOver(bool value)
    {
        gameOver = value;
		if (value) GetComponent<AudioSource>().Stop();
    }

    void SetSpeed(float value)
    {
        speed = value;
    }
}

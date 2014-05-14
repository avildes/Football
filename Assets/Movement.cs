using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private GameObject player;
	private Animator playerAnimator;

	private int horizontal, vertical;



	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerAnimator = player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float temp;

		temp = Input.GetAxis ("Horizontal");
		if (temp > 0) {
						horizontal = 1;
				} else if (temp == 0) {
						horizontal = 0;
				} else {
			horizontal = -1;
				}

		playerAnimator.SetInteger ("Horizontal", horizontal);

		temp = Input.GetAxis ("Vertical");
		if (temp > 0) {
			vertical = 1;
		} else if (temp == 0) {
			vertical = 0;
		} else {
			vertical = -1;
		}
		
		playerAnimator.SetInteger ("Vertical", vertical);
		



	}
}

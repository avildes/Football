using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private GameObject player;
	private Animator playerAnimator;

	private int horizontal, vertical;

	public Vector2 speed = new Vector2(1, 1);
	
	private Vector2 movement;


	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerAnimator = player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// Animacao

		if (inputX > 0) {
			horizontal = 1;
		} else if (inputX == 0) {
			horizontal = 0;
		} else {
			horizontal = -1;
		}

		playerAnimator.SetInteger ("Horizontal", horizontal);

		if (inputY > 0) {
			vertical = 1;
		} else if (inputY == 0) {
			vertical = 0;
		} else {
			vertical = -1;
		}
		
		playerAnimator.SetInteger ("Vertical", vertical);
		


		// movement

		Vector2 input = new Vector2 (inputX, inputY);
		//input = input.normalized;
		movement = new Vector2(
			speed.x * input.x,
			speed.y * input.y);
		



	}
	
	void FixedUpdate()
	{
		rigidbody2D.velocity = movement;
	}

}

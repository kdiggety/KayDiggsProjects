using UnityEngine;
using System.Collections;

public class Player : Character 
{
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		// these are false unless one of keys is pressed
		isLeft = false;
		isRight = false;
		isUp = false;
		isDown = false;
		
		movingDir = moving.None;
		
		// keyboard input
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{ 
			isLeft = true;
			facingDir = facing.Left;
		}
		if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (isLeft == false)) 
		{ 
			isRight = true; 
			facingDir = facing.Right;
		}
		
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{
			//isLeft = true;
			//facingDir = facing.Left;
			isUp = true; 
		}
		
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && (isUp == false)) 
		{
			//isRight = true;
			//facingDir = facing.Right;             x
			isDown = true;
		}
		
		UpdateMovement();
	}

	void OnCollisionEnter(Collision other)
	{
		print ("Player.OnCollisionEnter - Collision=" + other);
	}
	
	void OnTriggerEnter(Collider other)
	{
		print ("Player.OnTriggerEnter - Collider=" + other);
	}
}

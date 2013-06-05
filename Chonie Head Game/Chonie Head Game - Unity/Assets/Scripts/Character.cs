using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	[HideInInspector] public enum facing { Right, Left }
	[HideInInspector] public facing facingDir;
	
	[HideInInspector] public enum moving { Right, Left, None }
	[HideInInspector] public moving movingDir;
	
	[HideInInspector] public bool isLeft;
	[HideInInspector] public bool isRight;
	[HideInInspector] public bool isUp;
	[HideInInspector] public bool isDown;
	
	[HideInInspector] public bool blockedRight;
	[HideInInspector] public bool blockedLeft;
	[HideInInspector] public bool blockedUp;
	[HideInInspector] public bool blockedDown;
	
	protected Transform thisTransform;
	
	public float runVel = 4f;
	private float moveVel;
	private float walkVel = 3f; // walk while carrying ball
	private Vector3 vel2;
	private Vector3 vel;
	private float maxVelY = 0f;
			
	private RaycastHit hitInfo;
	private float halfMyX = 50.0f;
	private float halfMyY = 50.0f;
	[HideInInspector] public float rayDistUp = 50.0f;
	
	private float absVel2X;
	private float absVel2Y;
			
	public virtual void Awake()
	{
		thisTransform = transform;
	}
	
	// Use this for initialization
	public virtual void Start () 
	{
		moveVel = runVel;
		maxVelY = runVel;
		vel.y = 0;
	}
	
	// Update is called once per frame
	public virtual void UpdateMovement() 
	{
		vel.x = 0;
		vel.y = 0;
		
		// pressed right button
		if(isRight == true)
		{
			vel.x = moveVel;
		}
		
		// pressed left button
		if(isLeft == true)
		{			
			vel.x = -moveVel;
		}
		
		// pressed up button
		if(isUp == true)
		{
			vel.y = moveVel;
		}
		
		// pressed down button
		if(isDown == true)
		{
			vel.y = -moveVel;
		}
		
		UpdateRaycasts();
		
		// apply movement 
		vel2 = vel * Time.deltaTime;
		thisTransform.position += new Vector3(vel2.x,vel2.y,0f);
		
		// screen boundary
		if(thisTransform.position.x > 260.0f)
		{
			thisTransform.position = new Vector3(260.0f,thisTransform.position.y, -20f);
		}
		if(thisTransform.position.x < -260.0f)
		{
			thisTransform.position = new Vector3(-260.0f,thisTransform.position.y, -20f);
		}
		if(thisTransform.position.y > 230.0f)
		{
			thisTransform.position = new Vector3(thisTransform.position.x,230.0f, -20f);
		}
		if(thisTransform.position.y < -230.0f)
		{
			thisTransform.position = new Vector3(thisTransform.position.x,-230.0f, -20f);
		}
	}
	
	// ============================== RAYCASTS ============================== 
	
	void UpdateRaycasts()
	{
		blockedRight = false;
		blockedLeft = false;
		blockedUp = false;
		blockedDown = false;
		
		absVel2X = Mathf.Abs(vel2.x);
		absVel2Y = Mathf.Abs(vel2.y);
		
		//print ("Character.UpdateRaycasts - x=" + thisTransform.position.x + ", y=" + thisTransform.position.y + ", absVel2X=" + absVel2X + ", absVel2Y=" + absVel2Y);
		
		/*if (Physics.Raycast(new Vector3(thisTransform.position.x-0.25f,thisTransform.position.y,0f), -Vector3.up, out hitInfo, 0.6f+absVel2Y) 
			|| Physics.Raycast(new Vector3(thisTransform.position.x+0.25f,thisTransform.position.y,0f), -Vector3.up, out hitInfo, 0.6f+absVel2Y))
		{			
			// not while jumping so he can pass up thru platforms
			if(vel.y <= 0)
			{
				grounded = true;
				vel.y = 0f; // stop falling			
				thisTransform.position = new Vector3(thisTransform.position.x,hitInfo.point.y+halfMyY,0f);
			}
		}*/
		
		// blocked up
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,0f), Vector3.up, out hitInfo, halfMyY+absVel2Y))
		{
			//print ("Character.UpdateRaycasts - BlockedUp");
			BlockedUp();
		}
		
		// blocked down
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,0f), -Vector3.up, out hitInfo, halfMyY+absVel2Y))
		{
			//print ("Character.UpdateRaycasts - BlockedDown");
			BlockedDown();
		}
		
		// blocked on right
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,0f), Vector3.right, out hitInfo, halfMyX+absVel2X))
		{
			//print ("Character.UpdateRaycasts - BlockedRight");
			BlockedRight();
		}
		
		// blocked on left
		if(Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,0f), -Vector3.right, out hitInfo, halfMyX+absVel2X))
		{
			//print ("Character.UpdateRaycasts - BlockedLeft");
			BlockedLeft();
		}
		
		//print ("Character.UpdateRaycasts - hitInfo.collider=" + hitInfo.collider);
	}
	
	void BlockedUp()
	{
		if(vel.y > 0)
		{
			blockedUp = true;
			vel.y = 0f;
		}
	}

	void BlockedDown()
	{
		if(vel.y < 0)
		{
			blockedDown = true;
			vel.y = 0f;
		}
	}
	
	void BlockedRight()
	{
		//if(facingDir == facing.Right || movingDir == moving.Right)
		if(vel.x > 0)
		{
			blockedRight = true;
			vel.x = 0f;
			//thisTransform.position = new Vector3(hitInfo.point.x-(halfMyX-0.01f),thisTransform.position.y, thisTransform.position); // .01 less than collision width.
		}
	}
	
	void BlockedLeft()
	{
		//if(facingDir == facing.Left || movingDir == moving.Left)
		if(vel.x < 0)
		{
			blockedLeft = true;
			vel.x = 0f;
			//thisTransform.position = new Vector3(hitInfo.point.x+(halfMyX-0.01f),thisTransform.position.y, thisTransform.position); // .01 less than collision width.
		}
	}
}

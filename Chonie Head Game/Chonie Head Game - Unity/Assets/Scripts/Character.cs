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
	private float halfMyX = 0.325f; //0.25f;
	private float halfMyY = 0.5f;//0.375f;
	[HideInInspector] public float rayDistUp = 0.375f;
	
	private float absVel2X;
	private float absVel2Y;
	
	// layer masks
	protected int groundMask = 1 << 8; // Ground
		
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
		//if(xa.gameOver == true || alive == false) return;
		
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
		
		//UpdateRaycasts();
		
		// apply movement 
		vel2 = vel * Time.deltaTime;
		thisTransform.position += new Vector3(vel2.x,vel2.y,0f);
		
		// screen boundary
		if(thisTransform.position.x > 260.0f)
		{
			thisTransform.position = new Vector3(260.0f,thisTransform.position.y, -11.0f);
		}
		if(thisTransform.position.x < -260.0f)
		{
			thisTransform.position = new Vector3(-260.0f,thisTransform.position.y, -11.0f);
		}
		if(thisTransform.position.y > 260.0f)
		{
			thisTransform.position = new Vector3(thisTransform.position.x,260.0f, -11.0f);
		}
		if(thisTransform.position.y < -260.0f)
		{
			thisTransform.position = new Vector3(thisTransform.position.x,-260.0f, -11.0f);
		}
	}
	
	// ============================== RAYCASTS ============================== 
	
	/*void UpdateRaycasts()
	{
		blockedRight = false;
		blockedLeft = false;
		blockedUp = false;
		blockedDown = false;
		
		absVel2X = Mathf.Abs(vel2.x);
		absVel2Y = Mathf.Abs(vel2.y);
		
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.25f,thisTransform.position.y,0f), -Vector3.up, out hitInfo, 0.6f+absVel2Y, groundMask) 
			|| Physics.Raycast(new Vector3(thisTransform.position.x+0.25f,thisTransform.position.y,0f), -Vector3.up, out hitInfo, 0.6f+absVel2Y, groundMask))
		{			
			// not while jumping so he can pass up thru platforms
			if(vel.y <= 0)
			{
				grounded = true;
				vel.y = 0f; // stop falling			
				thisTransform.position = new Vector3(thisTransform.position.x,hitInfo.point.y+halfMyY,0f);
			}
		}
		
		// blocked up
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.2f,thisTransform.position.y,0f), Vector3.up, out hitInfo, rayDistUp+absVel2Y, groundMask)
			|| Physics.Raycast(new Vector3(thisTransform.position.x+0.2f,thisTransform.position.y,0f), Vector3.up, out hitInfo, rayDistUp+absVel2Y, groundMask))
		{
			BlockedUp();
		}
		
		// blocked on right
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,0f), Vector3.right, out hitInfo, halfMyX+absVel2X, groundMask))
		{
			BlockedRight();
		}
		
		// blocked on left
		if(Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,0f), -Vector3.right, out hitInfo, halfMyX+absVel2X, groundMask))
		{
			BlockedLeft();
		}
	}*/
	
	void BlockedUp()
	{
		if(vel.y > 0)
		{
			vel.y = 0f;
			blockedUp = true;
		}
	}

	void BlockedDown()
	{
		if(vel.y > 0)
		{
			vel.y = 0f;
			blockedDown = true;
		}
	}
	
	void BlockedRight()
	{
		if(facingDir == facing.Right || movingDir == moving.Right)
		{
			blockedRight = true;
			vel.x = 0f;
			thisTransform.position = new Vector3(hitInfo.point.x-(halfMyX-0.01f),thisTransform.position.y, 0f); // .01 less than collision width.
		}
	}
	
	void BlockedLeft()
	{
		if(facingDir == facing.Left || movingDir == moving.Left)
		{
			blockedLeft = true;
			vel.x = 0f;
			thisTransform.position = new Vector3(hitInfo.point.x+(halfMyX-0.01f),thisTransform.position.y, 0f); // .01 less than collision width.
		}
	}
}

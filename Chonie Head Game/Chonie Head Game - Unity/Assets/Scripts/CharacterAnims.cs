using UnityEngine;
using System.Collections;

public class CharacterAnims : MonoBehaviour 
{
	public enum anim { None, WalkLeft, WalkRight, RopeLeft, RopeRight, Climb, ClimbStop, StandLeft, StandRight, HangLeft, HangRight, FallLeft, FallRight , ShootLeft, ShootRight }
	
	public Transform spriteParent;
	public OTAnimatingSprite playerSprite;
	
	private anim currentAnim;
	private Character character;
	
	// Use this for initialization
	void Start () 
	{
		character = GetComponent<Character>();
	}
	
	void Update() 
	{		
		// run left
		if (((character.isLeft == true) || (character.isUp == true)) && (currentAnim != anim.WalkLeft))
		{
			currentAnim = anim.WalkLeft;
			playerSprite.Play("run");
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		
		// stand left
		if((character.isLeft == false) && (character.isUp == false) && (character.isDown == false) && (currentAnim != anim.StandLeft) && (character.facingDir == Character.facing.Left))
		{
			currentAnim = anim.StandLeft;
			playerSprite.Play("stand"); // stand left
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		
		// run right
		if(((character.isRight == true) || (character.isDown == true)) && (currentAnim != anim.WalkRight))
		{
			currentAnim = anim.WalkRight;
			playerSprite.Play("run");
			spriteParent.localScale = new Vector3(1,1,1);
		}
		
		// stand right
		if((character.isRight == false) && (character.isUp == false) && (character.isDown == false) && (currentAnim != anim.StandRight) && (character.facingDir == Character.facing.Right))
		{
			currentAnim = anim.StandRight;
			playerSprite.Play("stand"); // stand right
			spriteParent.localScale = new Vector3(1,1,1);
		}
	}
}

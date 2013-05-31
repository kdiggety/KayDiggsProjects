using UnityEngine;
using System.Collections;

public class CharacterAnims : MonoBehaviour 
{
	public enum anim { None, WalkLeft, WalkRight, RopeLeft, RopeRight, Climb, ClimbStop, StandLeft, StandRight, HangLeft, HangRight, FallLeft, FallRight , ShootLeft, ShootRight }
	
	public Transform spriteParent;
	public OTAnimatingSprite playerSprite;
	//public tk2dAnimatedSprite playerSprite;
	
	private anim currentAnim;
	private Character character;
	
	// Use this for initialization
	void Start () 
	{
		character = GetComponent<Character>();
	}
	
	void Update() 
	{
		if(xa.gameOver == true) return;
		
		// run left
		if(character.isLeft && character.grounded == true && currentAnim != anim.WalkLeft)
		{
			currentAnim = anim.WalkLeft;
			playerSprite.Play("run");
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		if(!character.isLeft && character.grounded == true && currentAnim != anim.StandLeft && character.facingDir == Character.facing.Left)
		{
			currentAnim = anim.StandLeft;
			playerSprite.Play("stand"); // stand left
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		
		// run right
		if(character.isRight && character.grounded && currentAnim != anim.WalkRight)
		{
			currentAnim = anim.WalkRight;
			playerSprite.Play("run");
			spriteParent.localScale = new Vector3(1,1,1);
		}
		if(!character.isRight && character.grounded && currentAnim != anim.StandRight && character.facingDir == Character.facing.Right)
		{
			currentAnim = anim.StandRight;
			playerSprite.Play("stand"); // stand left
			spriteParent.localScale = new Vector3(1,1,1);
		}
		
		// falling
		if(character.grounded == false && currentAnim != anim.FallLeft && character.facingDir == Character.facing.Left)
		{
			currentAnim = anim.FallLeft;
			playerSprite.Play("jump"); // fall left
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		if(character.grounded == false && currentAnim != anim.FallRight && character.facingDir == Character.facing.Right)
		{
			currentAnim = anim.FallRight;
			playerSprite.Play("jump"); // fall right
			spriteParent.localScale = new Vector3(1,1,1);
		}
	}
}

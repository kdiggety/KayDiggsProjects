// ------------------------------------------------------------------------
// Orthello 2D Framework Example Source Code
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Example 3
// Using 'collidable' animating sprites and handle collisions
// - asteroid 'full' animation
// - gun : 2 single frameset (idle/shoot) animation
// ------------------------------------------------------------------------
// Asteroid behaviour class
// ------------------------------------------------------------------------
using UnityEngine;
using System.Collections;


public class Monster : MonoBehaviour {
void OnCollisionEnter(Collision other)
	{
		print ("Monster.OnCollisionEnter - Collision.collider=" + ((other.collider.transform.parent != null) ? other.collider.transform.parent.name : other.collider.name));
	}
	
	void OnTriggerEnter(Collider other)
	{
		print ("Monster.OnTriggerEnter - Collider.collider=" + ((other.collider.transform.parent != null) ? other.collider.transform.parent.name : other.collider.name));
		OT.DestroyObject(this);
	}
}

using UnityEngine;
using System.Collections;

public class xa : MonoBehaviour 
{
	public static AudioManager audioManager;
	
	public static Player player;
	
	public static bool gameOver = false;
	
	// set to false if game is configured for CTF
	public static bool letsPlayKeepaway = false; 
	
	// layers
	public const int Team1Goal = 9;
	public const int Team2Goal = 10;
	
	void Start()
	{
		// cache these so they can be accessed by other scripts
		audioManager = gameObject.GetComponent<AudioManager>();
		player = GameObject.Find("Player").GetComponent<Player>();
	}
}

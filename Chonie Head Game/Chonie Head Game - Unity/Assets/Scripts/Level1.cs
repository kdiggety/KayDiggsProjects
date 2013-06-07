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
// Main Example 3 Demo class
// ------------------------------------------------------------------------
using UnityEngine;
using System.Collections;


public class Level1 : MonoBehaviour {
	
	OTTileMap plane;              // room plane
	
    // sprite prototypes that will be used when creating objects
    bool initialized = false;           // initialization notifier

    int dp = 100;
	
	// This method will create an asteroid at a random position on screen and with
    // relative min/max (0-1) size. An OTObject can be provided to act as a base to 
    // determine the new size.
    OTAnimatingSprite RandomBlock(Rect r, float min, float max, OTObject o)
    {		
        // Determine random 1-3 asteroid type
        int t = 1 + (int)Mathf.Floor(Random.value * 3);
        // Determine random size modifier (min-max)
        float s = min + Random.value * (max - min);
		OTSprite sprite = null;
        // Create a new asteroid
        switch (t)
        {
            case 1:
				sprite = OT.CreateSprite("Monsters");
				sprite.frameIndex = 1;
                break;
            case 2: 
				sprite = OT.CreateSprite("Monsters");
				sprite.frameIndex = 2;
                break;
            case 3: 
				sprite = OT.CreateSprite("Monsters");
				sprite.frameIndex = 3;
                break;
        }
		// big blocks start invisible and will be faded.
		if (o == null)
			sprite.alpha = 0;
        if (sprite != null)
        {
            // Set sprite's size
	        if (o != null)
	            sprite.size = o.size * s;
	        else
	            sprite.size = sprite.size * s;
            // Set sprite's random position
            sprite.position = new Vector2(r.xMin + Random.value * r.width, r.yMin + Random.value * r.height);
            // Set sprote's random rotation
            sprite.rotation = Random.value * 360;
            // Set sprite's name
            //sprite.depth = dp++;
            //if (dp > 750) dp = 100;
			sprite.depth = -10;
        }
		
		// fade in the (big) asteroid
		if (o == null)
			new OTTween(sprite,0.75f,OTEasing.Linear).Wait(Random.value * 1).Tween("alpha",0,1);
		
        return sprite as OTAnimatingSprite;
    }
	
    // Create objects for this application
    void CreateObjectPools()
    {
		OT.PreFabricate("Monsters",250);
    }

    // application initialization
    void Initialize()
    {
		// Get reference to gun animation sprite
        plane = OT.ObjectByName("TileMap") as OTTileMap;
		
        CreateObjectPools();
        // set our initialization notifier - we only want to initialize once
        initialized = true;
    }	
    
	// Update is called once per frame
	void Update () {
		
        // only go one if Orthello is initialized
        if (!OT.isValid) return;

        // We call the application initialization function from Update() once 
        // because we want to be sure that all Orthello objects have been started.
        if (!initialized)
        {
            Initialize();
            return;
        }
		
		//If we have less than 15 objects within Orthello we will create a random asteroid
        if (OT.objectCount <= 15)
           RandomBlock(plane.rect, 0.6f, 1.2f, null);  
	}
}


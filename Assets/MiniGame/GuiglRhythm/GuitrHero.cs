using UnityEngine;
using System.Collections;

public class GuitrHero : MiniGame {

	void Start () {
       /* 
        Stage: 
	      3 lines for notes
	      perpendicular line for "strum time"
	      sprites for dots vs lines
          	
	   */
	    // Instantiate background
		// setup dynamic components 
		//put partyer in default position
	}
	
	void Update () {
	    // set buttons to state based on input
		// 
	}

	public override void tick(InputSet intput) {
	}

	public override void control(ControlCommand cmd) {
	}
}


/*
 * Static background
 * asset package for things that move around
 */

/* Need:
 * Button object with:
     collider
     slot for character sprite
	 script to toggle collider & image

   
 */
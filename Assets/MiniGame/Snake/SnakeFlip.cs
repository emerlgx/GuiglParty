using UnityEngine;
using System.Collections;

public class SnakeFlip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// reflect the snake head's orientation depending on the type of wall
		if (coll.gameObject.tag == "horizontal") {
			//flip
		}

		if (coll.gameObject.tag == "vertical") {
			//flop
		}

	}
}

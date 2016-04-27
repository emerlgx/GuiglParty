using UnityEngine;
using System.Collections;

public class SnakeFlip : MonoBehaviour {
	public GameObject snakeGame;

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "beet") {
			Destroy (coll.gameObject);
			snakeGame.SendMessage ("eatBeet");
		} else {
			this.transform.Rotate (Vector3.forward * 180);
		}
		// reflect the snake head's orientation depending on the type of wall
		/*if (coll.gameObject.tag == "horizontal") {
			this.transform.rotation = new Quaternion (this.transform.localRotation.x*-1,
				this.transform.localRotation.y,
				this.transform.localRotation.z,
				this.transform.localRotation.w);

		}

		if (coll.gameObject.tag == "vertical") {
			this.transform.rotation = new Quaternion (this.transform.localRotation.x,
				this.transform.localRotation.y*-1,
				this.transform.localRotation.z,
				this.transform.localRotation.w);
		}*/

	}
}

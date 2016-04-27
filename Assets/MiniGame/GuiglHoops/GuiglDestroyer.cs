using UnityEngine;
using System.Collections;

public class GuiglDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			Destroy (coll.gameObject);
		}
	}
}

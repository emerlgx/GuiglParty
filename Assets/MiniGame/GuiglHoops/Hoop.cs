using UnityEngine;
using System.Collections;

public class Hoop : MonoBehaviour {
	public GuiglHoops gh;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			Destroy (coll.gameObject);
			gh.dunk();
		}
	}
}

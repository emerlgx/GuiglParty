using UnityEngine;
using System.Collections;

public class JumperController : MonoBehaviour {
	public GameObject Move2LeftGame;

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "floor") {
			Move2LeftGame.SendMessage("landed");
		}
	}
}
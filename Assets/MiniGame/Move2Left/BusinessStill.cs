using UnityEngine;
using System.Collections;

public class BusinessStill : MonoBehaviour {
	public GameObject Move2LeftGame;

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") {
			Move2LeftGame.SendMessage("respawn");
		}
	}
}
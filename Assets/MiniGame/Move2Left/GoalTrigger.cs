using UnityEngine;
using System.Collections;

public class GoalTrigger : MonoBehaviour {
	public Move2Left M2LGame;

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Player") {
			Debug.Log ("Found a player!");
			M2LGame.SendMessage ("goal");
		}
	}
}
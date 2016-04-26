using UnityEngine;
using System.Collections;

public class GoalTrigger : MonoBehaviour {
	public Move2Left M2LGame;

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") {
			M2LGame.SendMessage ("Goal");
		}
	}
}

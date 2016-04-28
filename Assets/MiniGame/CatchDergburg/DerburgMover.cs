using UnityEngine;
using System.Collections;

public class DerburgMover : MonoBehaviour {
	private Transform[] chasers; 
	public float speed;
	private Rigidbody2D rigidForm;

	void Awake() {
		chasers = new Transform[4];
		rigidForm = GetComponent<Rigidbody2D>();
	}
		
	void Update () {
		Vector3 away = Vector3.zero;
		for (int i = 0; i < 4; i++) {
			Vector3 diff = transform.position - chasers [i].position;
			away += diff.normalized * Mathf.Pow(diff.magnitude, -2); 
		}
		Vector2 here = new Vector2(transform.position.x, transform.position.y);
		Vector2 there = here + new Vector2(away.x, away.y);
		rigidForm.velocity = speed * Vector2.Lerp(here, there, Time.deltaTime);
	}

	public void setChasers(Transform[] gos){
		chasers = gos;
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "floor") {
			rigidForm.velocity = new Vector2(rigidForm.velocity.x, -rigidForm.velocity.y);
		} else if (coll.gameObject.tag == "wall") {
			rigidForm.velocity = new Vector2(-rigidForm.velocity.x, rigidForm.velocity.y);
		}
	}
}

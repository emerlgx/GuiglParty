using UnityEngine;
using System.Collections;

public class ChaserMover : MonoBehaviour {
	private Rigidbody2D rigidForm;
	static public float maxAngularVelocity = 180;
	static public float rotateSpeed        = 60;
	static public float maxSpeed           = 5;
	static public float acceleration       = 40;

	void Awake(){
		rigidForm = GetComponent<Rigidbody2D>();
	}

	public void move(InputSet input){
		Debug.Log("input null: "+(input == null));
		if (input.left && rigidForm.angularVelocity <= maxAngularVelocity) {
			rigidForm.velocity = Vector2.zero;
			transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
		}

		if (input.right && rigidForm.angularVelocity <= maxAngularVelocity) {
			rigidForm.velocity = Vector2.zero;
			transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
		}

		if (!input.left && !input.right) {
			rigidForm.angularVelocity = 0.0f;
		}


		if (input.middle) {
			if (rigidForm.velocity.magnitude <= maxSpeed) {
				Vector2 forceVec = transform.InverseTransformVector(Vector2.up * acceleration * Time.deltaTime);
				rigidForm.AddForce (forceVec);
			} else {
				rigidForm.velocity = rigidForm.velocity.normalized * maxSpeed;
			}
		} else {
			Vector2 deccelerate = transform.InverseTransformVector(Vector2.down * 2f * acceleration * Time.deltaTime);
			rigidForm.AddForce(deccelerate);
		}

		// move the partyer forward
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "chaser") {
			rigidForm.velocity = -rigidForm.velocity;
		} else if (coll.gameObject.tag == "floor") {
			rigidForm.velocity = new Vector2(rigidForm.velocity.x, -rigidForm.velocity.y);
		} else if (coll.gameObject.tag == "wall") {
			rigidForm.velocity = new Vector2(-rigidForm.velocity.x, rigidForm.velocity.y);
		}
	}
}

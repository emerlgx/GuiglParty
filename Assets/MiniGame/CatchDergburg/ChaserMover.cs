using UnityEngine;
using System.Collections;

public class ChaserMover : MonoBehaviour {
	private Rigidbody2D rigidForm;
	static public float maxAngularVelocity = 30;
	static public float rotateSpeed        = 30;
	static public float maxSpeed           = 10;
	static public float acceleration       = 5;

	void Awake(){
		rigidForm = GetComponent<Rigidbody2D>();
	}

	public void move(InputSet input){
		if (input.left && rigidForm.angularVelocity <= maxAngularVelocity) {
			transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
		}

		if (input.right && rigidForm.angularVelocity <= maxAngularVelocity) {
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
			Vector2 deccelerate = transform.InverseTransformVector(Vector2.down * 0.5f * acceleration * Time.deltaTime);
			rigidForm.AddForce(deccelerate);
		}

		// move the partyer forward
	}
}

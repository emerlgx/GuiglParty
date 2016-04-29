using UnityEngine;
using System.Collections;

public class ChaserMover : MonoBehaviour {
	public Partyer partyer;
	private Rigidbody2D rigidForm;
	static public float maxAngularVelocity = 180;
	static public float rotateSpeed        = 180;
	static public float maxSpeed           = 5;
	static public float acceleration       = 1;
	private Vector3 direction;
	private float veloScale = 0;

	void Awake(){
		rigidForm = GetComponent<Rigidbody2D>();
		direction = new Vector3(0,1,0);
	}

	public void move(InputSet input){
		Debug.Log("input null: "+(input == null));
		if (input.left && rigidForm.angularVelocity <= maxAngularVelocity) {
			//rigidForm.AddRelativeForce = Vector2.zero;
			transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
			direction = Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime) * direction;
		}

		if (input.right && rigidForm.angularVelocity <= maxAngularVelocity) {
			//rigidForm.velocity = Vector2.zero;
			transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
			direction = Quaternion.Euler(0, 0, -rotateSpeed * Time.deltaTime) * direction;
		}

		//if (!input.left && !input.right) {
		//	rigidForm.angularVelocity = 0.0f;
		//}


		if (input.middle) {
			if (veloScale <= maxSpeed) {
				veloScale += acceleration;
			} else {
				veloScale = maxSpeed;
			}
		} else {
			if (veloScale > 0) {
				veloScale -= 0.5f * acceleration;
			} else {
				veloScale = 0;
			}
		}

		transform.position += veloScale * Time.deltaTime * direction;
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

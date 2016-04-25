using UnityEngine;
using System.Collections;

public class Snake : MiniGame {
	InputSet inputSet;

	public Rigidbody2D SnakeHead;
	public float maxSpeed = 20;		// we will not add force above this speed
	public float rotateSpeed = 30;	// how many degrees a partyer will rotate in one second

	void Start () {
		
	}
	
	void FixedUpdate () {

		// check the controls
		if (inputSet.left) {
			SnakeHead.transform.Rotate(-1.0f * Vector3.forward * rotateSpeed * Time.deltaTime);
			Vector2 velocity = SnakeHead.velocity;
			SnakeHead.velocity = SnakeHead.transform.InverseTransformVector(Vector2.up/8) * velocity.magnitude;
			Debug.Log ("turned left!");
		}

		if (inputSet.right) {
			SnakeHead.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
			Vector2 velocity = SnakeHead.velocity;
			SnakeHead.velocity = SnakeHead.transform.InverseTransformVector(Vector2.up/8) * velocity.magnitude;
			Debug.Log ("Turned right!");
		}
	
		// move the partyer forward
		if(SnakeHead.velocity.magnitude <= maxSpeed){
			Vector2 forceVec = SnakeHead.transform.InverseTransformVector(Vector2.up/2);
			SnakeHead.AddForce(forceVec);
		}
	}

	public override void tick (InputSet input) {
		inputSet = input;
	}

	public override void control (ControlCommand command) {

	}
}

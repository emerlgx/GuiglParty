using UnityEngine;
using System.Collections;

public class Snake : MiniGame {
	InputSet inputSet;

	public Rigidbody2D SnakeHead;	// the part of the snake that the player controls
	public GameObject BeetPrefab;	// used to make beets for the snake to eat

	public float maxSpeed = 10;		// we will not add force above this speed
	public float acceleration = 5;
	public float maxAngularVelocity = 30; 
	public float rotateSpeed = 30; // how many degrees a partyer will rotate in one second

	void Start () {
		SnakeHead.gameObject.GetComponent<SnakeFlip> ().snakeGame = this.gameObject;	// tell the snakehead where to provide information
		makeBeet();												// place a starting beet
	}

	void FixedUpdate () {

		// check the controls
		if (inputSet.left && SnakeHead.angularVelocity <= maxAngularVelocity) {
			SnakeHead.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
			//Vector2 velocity = SnakeHead.velocity;
			//SnakeHead.velocity = SnakeHead.transform.InverseTransformVector(Vector2.up/8) * velocity.magnitude;
			Debug.Log ("turned left!");
		}

		if (inputSet.right && SnakeHead.angularVelocity <= maxAngularVelocity) {
			SnakeHead.transform.Rotate(-1.0f * Vector3.forward * rotateSpeed * Time.deltaTime);
			//Vector2 velocity = SnakeHead.velocity;
			//SnakeHead.velocity = SnakeHead.transform.InverseTransformVector(Vector2.up/8) * velocity.magnitude;
			Debug.Log ("Turned right!");
		}

		if (!inputSet.left && !inputSet.right) {
			SnakeHead.angularVelocity = 0.0f;
		}

		// move the partyer forward
		if (SnakeHead.velocity.magnitude <= maxSpeed) {
			Vector2 forceVec = SnakeHead.transform.InverseTransformVector (Vector2.up * acceleration * Time.deltaTime);
			SnakeHead.AddForce (forceVec);
		} else {
			SnakeHead.velocity = SnakeHead.velocity.normalized * maxSpeed;
		}
	}

	public override void tick (InputSet input) {
		inputSet = input;
	}

	public override void control (ControlCommand command) {
		// start and stop the game based on commands
	}

	public void makeBeet() {
		float x = Random.Range (-1.3F, 1.3F);
		float y = Random.Range (-1.3F, 1.3F);
		GameObject tempBeet = Instantiate(BeetPrefab,new Vector3(x,y,0),Quaternion.identity) as GameObject;
		tempBeet.transform.SetParent (this.transform, false);
	}

	public void eatBeet() {
		// give the player points for eating the beet
		partyer.givePoints(1);
		makeBeet ();
	}
}
using UnityEngine;
using System.Collections;

public class Snake : MiniGame {
	InputSet inputSet;

	public Rigidbody2D SnakeHead;

	void Start () {
		SnakeHead.AddForce (new Vector2 (10, 10));
	}
	
	void Update () {
	
	}

	public override void tick (InputSet input) {
		inputSet = input;
	}

	public override void control (ControlCommand command) {

	}
}

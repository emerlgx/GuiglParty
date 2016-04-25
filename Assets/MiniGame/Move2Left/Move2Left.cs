using UnityEngine;
using System.Collections;

public class Move2Left : MiniGame {
	public Rigidbody2D jumper;
	public JumperController jc;

	public Transform respawnPoint;

	public float moveSpeed = 5;
	public float jumpPower = 5;

	InputSet inputs;
	bool isJumping = true;

	void FixedUpdate() {
		if (inputs.left) {
			Debug.Log("Going Left!");
			Vector3 tempPos = jumper.transform.position;
			tempPos.x -= moveSpeed * Time.deltaTime;
			jumper.transform.position = tempPos;
		}

		if (inputs.right) {
			Debug.Log("Going Right!");
			Vector3 tempPos = jumper.transform.position;
			tempPos.x += moveSpeed * Time.deltaTime;
			jumper.transform.position = tempPos;
		}

		if (inputs.middle && !isJumping) {
			Debug.Log("Going Up!");
			jumper.AddForce (new Vector2 (0, jumpPower));
			isJumping = true;
		}


	}

	public override void tick (InputSet input) {
		inputs = input;
	}

	public override void control (ControlCommand command) {
		// do the mario!
	}

	public void setPartyer (Partyer p) {
		MiniGame:setPartyer (p);
	}

	private void respawn() {
		jumper.transform.position = respawnPoint.position;
		isJumping = true;
	}

	private void landed () {
		isJumping = false;
	}
}

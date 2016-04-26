using UnityEngine;
using System.Collections;

public class Move2Left : MiniGame {
	public Rigidbody2D jumper;
	public JumperController jc;
	public GoalTrigger gt;

	public Transform respawnPoint;

	public float moveSpeed = 5;
	public float jumpPower = 100;

	float jumpTimer = 1.0f;
	float jumpTimerMax = 1.0f;

	InputSet inputs;
	bool isJumping = true;

	void Awake () {
		inputs = new InputSet (false, false, false);
	}

	void FixedUpdate() {
		if (inputs.left) {
			//Debug.Log("Going Left!");
			Vector3 tempPos = jumper.transform.position;
			tempPos.x -= moveSpeed * Time.deltaTime;
			jumper.transform.position = tempPos;
		}

		if (inputs.right) {
			//Debug.Log("Going Right!");
			Vector3 tempPos = jumper.transform.position;
			tempPos.x += moveSpeed * Time.deltaTime;
			jumper.transform.position = tempPos;
		}

		if (inputs.middle && (!isJumping || jumpTimer <= 0.0f)) {
			Debug.Log("Going Up!");
			jumper.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);

			isJumping = true;
			jumpTimer = jumpTimerMax;
		}

		jumpTimer -= Time.deltaTime;

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

	public void landed () {
		Debug.Log ("Landed!");
		isJumping = false;
	}
}

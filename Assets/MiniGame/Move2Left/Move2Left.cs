﻿using UnityEngine;
using System.Collections;

public class Move2Left : MiniGame {
	public Rigidbody2D jumper;
	public SpriteRenderer jumpSprite;
	public JumperController jc;
	public GoalTrigger gt;
	public GameObject fallingBusinessPrefab;
	public GameObject stillBusinessPrefab;

	public Transform respawnPoint;

	public float moveSpeed = 5;
	public float jumpPower = 100;

	public int pointsToGive = 10;

	float jumpTimer = 1.0f;
	float jumpTimerMax = 1.0f;

	InputSet inputs;
	bool isJumping = true;

	void Awake () {
		inputs = new InputSet (false, false, false);

		// make the business
		makeFallingBusiness();
		makeFallingBusiness();
		makeFallingBusiness();
		makeStillBusiness ();


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
			jumper.AddForce(Vector2.up * 1.3f * moveSpeed, ForceMode2D.Impulse);

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

	public override void setPartyer (Partyer p) {
		partyer = p;
		// change the sprite to the current player
		jumpSprite.sprite = partyer.face;
		Debug.Log (partyer.name);
		Debug.Log("Set Sprite = " + (jumper.GetComponent<SpriteRenderer>().sprite==p.face));
	}

	private void respawn() {
		jumper.transform.position = respawnPoint.position;
		isJumping = true;
		jumpTimer = jumpTimerMax;
	}

	public void landed () {
		isJumping = false;
	}

	public void goal () {
		partyer.givePoints(1);
		respawn ();
		Debug.Log ("Got a point!");
	}

	public void makeFallingBusiness () {
		float temp = Random.Range(-2.0f,1.1f);
		GameObject tempObj = Instantiate (fallingBusinessPrefab, new Vector3 (temp, 0, 0), Quaternion.identity) as GameObject;
		tempObj.GetComponent<BusinessFalling> ().Move2LeftGame = this.gameObject;
		tempObj.transform.SetParent (this.transform,false);
	}

	public void makeStillBusiness () {
		float temp = Random.Range(-1.9f,1.1f);
		GameObject tempObj = Instantiate (stillBusinessPrefab, new Vector3 (temp, -1.6f, 0), Quaternion.identity) as GameObject;
		tempObj.GetComponent<BusinessStill> ().Move2LeftGame = this.gameObject;
		tempObj.transform.SetParent (this.transform,false);
	}
}

using UnityEngine;
using System.Collections;

public class GuiglCannon : MiniGame {
	public GameObject cannonballPrefab;
	public GameObject cannon;


	public float cannonSpeed = 10.0f;
	public float cannonReloadTime = 1.0f;
	float cannonReloadTimeMax = 1.0f;


	public int pointsToGive = 20;
	InputSet inputs;

	void Awake () {
		inputs = new InputSet (false, false, false);
		cannonReloadTimeMax = cannonReloadTime;
		cannonReloadTime = 0.0f;

	}

	void FixedUpdate() {
		if (inputs.left) {
			
		}

		if (inputs.right) {
			
		}

		if (inputs.middle && cannonReloadTime <= 0.0f) {
			shootCannon();
		}

		cannonReloadTime -= Time.deltaTime;

	}

	public override void tick (InputSet input) {
		inputs = input;
	}

	public override void control (ControlCommand command) {
		// beep boop
	}

	public override void setPartyer (Partyer p) {
		partyer = p;
	}

	void shootCannon () {


		cannonReloadTime = cannonReloadTimeMax;
	}

	void hitTarget() {
		partyer.givePoints (pointsToGive);
	}

}
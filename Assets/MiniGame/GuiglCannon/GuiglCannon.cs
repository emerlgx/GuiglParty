using UnityEngine;
using System.Collections;

public class GuiglCannon : MiniGame {
	public GameObject cannonballPrefab;
	public GameObject targetPrefab;

	public GameObject cannon;
	public GameObject spawnPointHolder;

	public float cannonSpeed = 10.0f;
	public float cannonReloadTime = 1.0f;
	float cannonReloadTimeMax = 1.0f;

	public float maxAngle = 85.0f;	// how far up the cannon can aim
	public float minAngle = 5.0f;	// how far down the cannon can aim
	public float turnSpeed = 30.0f;	// how many degrees the cannon can turn per second
	float cannonAngle;


	public int pointsToGive = 20;
	InputSet inputs;

	void Awake () {
		inputs = new InputSet (false, false, false);
		cannonReloadTimeMax = cannonReloadTime;
		cannonReloadTime = 0.0f;

		cannonAngle = cannon.transform.localRotation.z;
		makeTarget ();
	}

	void FixedUpdate() {
		if (inputs.left && cannon.transform.localEulerAngles.z < maxAngle) {
			cannon.transform.Rotate (new Vector3(0,0,turnSpeed * Time.deltaTime));
		}

		if (inputs.right && cannon.transform.localEulerAngles.z > minAngle) {
			cannon.transform.Rotate (new Vector3(0,0,-1.0f*turnSpeed * Time.deltaTime));
		}

		if (inputs.middle && cannonReloadTime <= 0.0f) {
			shootCannon();
			cannonReloadTime = cannonReloadTimeMax;
		}

		/*if (cannon.transform.localRotation.z < minAngle) {
			cannon.transform.localEulerAngles = new Vector3 (cannon.transform.localRotation.x, cannon.transform.localRotation.y, minAngle);
		}

		if (cannon.transform.localRotation.z > maxAngle) {
			cannon.transform.localEulerAngles = new Vector3 (cannon.transform.localRotation.x, cannon.transform.localRotation.y, maxAngle);
		}*/

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
		GameObject shot = Instantiate (cannonballPrefab, cannon.transform.position, Quaternion.identity) as GameObject;
		shot.GetComponent<SpriteRenderer> ().sprite = partyer.face;
		shot.GetComponent<TrailRenderer> ().material.color = partyer.darkCol;

		shot.transform.rotation = cannon.transform.rotation;
		shot.GetComponent<Rigidbody2D> ().AddRelativeForce(Vector2.right*cannonSpeed);
	}

	void makeTarget() {
		// select one of the positions to spawn a target at
		GameObject spawnpoint;
			spawnpoint = spawnPointHolder.transform.GetChild (Random.Range(0,spawnPointHolder.transform.childCount)).gameObject;

		// instantiate and setup the target
		GameObject target = Instantiate (targetPrefab, spawnpoint.transform.position, Quaternion.identity) as GameObject;
		target.transform.SetParent(spawnpoint.transform, false);
		target.transform.localPosition = Vector3.zero;
		target.GetComponent<HitTarget> ().CannonGame = this.gameObject;

	}

	void hitTarget() {
		makeTarget ();
		partyer.givePoints (pointsToGive);
	}
}
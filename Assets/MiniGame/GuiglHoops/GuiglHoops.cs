using UnityEngine;
using System.Collections;

public class GuiglHoops : MiniGame {
	public Bump leftBump;
	public Bump rightBump;
	public GameObject leftSpawn;
	public GameObject rightSpawn;
	public GameObject guiglBallPrefab;
	public float launchSpeed = 100.0f;
	public float launchSpeedModifier = 100.0f;

	InputSet inputs;
	ArrayList guigls = new ArrayList();

	bool holdLeft = false;
	bool holdRight = false;
	bool holdMiddle = false;
	void Awake() {
		inputs = new InputSet(false,false,false);
	}

	void Update() {
		if (inputs.left) {
			if (!holdLeft) {
				leftBump.makeBump ();
				holdLeft = true;
			}

		} else {
			holdLeft = false;
		}

		if (inputs.right) {
			if (!holdRight) {
				rightBump.makeBump ();
				holdRight = true;
			}

		} else {
			holdRight = false;
		}

		if (inputs.middle) {
			if (!holdMiddle) {
				float decider = Random.Range (0.0f, 1.0f);
				if (decider > 0.5f) {
					spawnLeft ();
				} else {
					spawnRight ();
				}
				holdMiddle = true;
			}
		} else {
			holdMiddle = false;
		}
	}

	public override void tick (InputSet input) {
		inputs = input;
	}

	public override void control (ControlCommand command) {
		// stuff occurs here
	}

	public override void setPartyer (Partyer p) {
		// updates the partyer
		partyer = p;
	}

	public void spawnLeft() {
		GameObject ball = Instantiate (guiglBallPrefab, leftSpawn.transform.position, Quaternion.identity) as GameObject;
		float modifier = Random.Range (-1.0f * launchSpeedModifier, launchSpeedModifier);
		ball.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * (launchSpeed+modifier));
		guigls.Add (ball);
		Debug.Log ("made a guigl!");
	}

	public void spawnRight() {
		GameObject ball = Instantiate (guiglBallPrefab, rightSpawn.transform.position, Quaternion.identity) as GameObject;
		float modifier = Random.Range (-1.0f * launchSpeedModifier, launchSpeedModifier);
		ball.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * (launchSpeed+modifier));
		guigls.Add (ball);
		Debug.Log ("made a guigl!");
	}

	public void dunk() {
		partyer.givePoints (1);
	}
}

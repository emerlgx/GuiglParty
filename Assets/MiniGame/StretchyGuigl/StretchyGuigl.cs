using UnityEngine;
using System.Collections;

public class StretchyGuigl : MiniGame {
	InputSet inputs;

	public GameObject stretcher;
	public float maxTorque = 100.0f;
	public float torque = 50.0f;
	public float minWidth = 0.25;
	public float maxWidth = 5.0f;

	bool isStretching = false;

	bool leftDown = false;
	bool rightDown = false;
	bool middleDown = false;


	void Start () {
		inputs = new InputSet (false, false, false);
	}

	void FixedUpdate () {

		if (inputs.left) {
			leftDown = true;
			if (stretcher.GetComponent<Rigidbody2D> ().angularVelocity > -1.0f * maxTorque) {
				stretcher.GetComponent<Rigidbody2D> ().AddTorque (torque * Time.deltaTime);
			}
		} else {
			leftDown = false;
		}

		if (inputs.right) {
			rightDown = true;

		} else {
			rightDown = false;
		}

		if (inputs.middle) {
			middleDown = true;

		} else {
			middleDown = false;
		}

	}
		
	public override void tick(InputSet input) {
		inputs = input;
	}

	public override void control (ControlCommand command) {
		// beep boop
	}

	public override void setPartyer (Partyer p) {
		partyer = p;
		stretcher.GetComponent<SpriteRenderer> ().sprite = p.face;

	}

}

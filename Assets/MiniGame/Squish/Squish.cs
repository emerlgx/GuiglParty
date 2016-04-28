using UnityEngine;
using System.Collections;

public class Squish : MiniGame{
	InputSet inputs;

	public GameObject SpawnPoint;
	public GameObject guiglPrefab;
	public GameObject squisherObj;
	public GameObject background;

	public float guiglForce = 100.0f;

	public int pointsToGive = 5;

	bool side = false;
	bool letGoLeft = true;
	bool letGoRight = true;
	bool letGoMiddle = true;

	void Awake(){
		background = transform.FindChild ("Background").gameObject;
	}

	void Start () {
		inputs = new InputSet (false, false, false);
		squisherObj.GetComponent<Squisher> ().squishGame = this.gameObject;
	}

	void FixedUpdate() {
		if (inputs.left) {
			if (letGoLeft && !side) {
				makeGuigl ();
				side = !side;
			}
			letGoLeft = false;
		} else {
			letGoLeft = true;
		}

		if (inputs.right) {
			if (letGoRight && side) {
				makeGuigl ();
				side = !side;
			}
			letGoRight = false;
		} else {
			letGoRight = true;
		}

		if (inputs.middle) {
			if (letGoMiddle) {
				squisherObj.GetComponent<Squisher> ().Squish ();
				letGoMiddle = false;
			} else {
				letGoMiddle = true;
			}
		}


	}

	public override void tick (InputSet input) {
		inputs = input;
	}

	public override void control (ControlCommand command) {
		// noot noot
	}

	public override void setPartyer (Partyer p) {
		// updates the partyer
		partyer = p;
		background.GetComponent<SpriteRenderer>().sprite = partyer.face;
	}

	public void makeGuigl() {
		GameObject temp = Instantiate (guiglPrefab, SpawnPoint.transform.position, Quaternion.identity) as GameObject;
		temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1.0f* guiglForce, guiglForce), 0.0f));
		temp.GetComponent<SpriteRenderer> ().sprite = partyer.face;
		temp.transform.SetParent (this.transform);
	}

	public void squished() {
		partyer.givePoints (pointsToGive);
	}
}

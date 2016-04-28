using UnityEngine;
using System.Collections;

public class GuiglHoops : MiniGame {
	public Bump leftBump;
	public Bump rightBump;
	public GameObject leftSpawn;
	public GameObject rightSpawn;
	public GameObject guiglBallPrefab;
	public int pointsToGive = 10;
	public float launchSpeed = 100.0f;
	public float launchSpeedModifier = 100.0f;

	private SpriteRenderer background;
	float r, g, b, dr, dg, db;

	InputSet inputs;
	ArrayList guigls = new ArrayList();

	bool holdLeft = false;
	bool holdRight = false;
	bool holdMiddle = false;
	void Awake() {
		inputs = new InputSet(false,false,false);
		r  = Random.Range(0f, 1f);
		g  = Random.Range(0f, 1f);
		b  = Random.Range(0f, 1f);
		dr = Random.Range(0.5f, 3f);
		dg = Random.Range(0.5f, 3f);
		db = Random.Range(0.5f, 3f);


		background = transform.FindChild("badJapaneseFlag").GetComponent<SpriteRenderer>();
	}

	void Update() {
		background.color = new Color(
			0.5f + 0.5f * Mathf.Sin(r * Mathf.PI),
			0.5f + 0.5f * Mathf.Sin(g * Mathf.PI),
			0.5f + 0.5f * Mathf.Sin(b * Mathf.PI), 
			1);
		r += dr * Time.deltaTime;
		g += dg * Time.deltaTime;
		b += db * Time.deltaTime;

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
		foreach (GameObject ball in guigls) {
			if (ball) {
				ball.GetComponent<SpriteRenderer> ().sprite = partyer.face;
			}
		}
	}

	public void spawnLeft() {
		GameObject ball = Instantiate (guiglBallPrefab, leftSpawn.transform.position, Quaternion.identity) as GameObject;
		float modifier = Random.Range (-1.0f * launchSpeedModifier, launchSpeedModifier);
		ball.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * (launchSpeed+modifier));
		ball.GetComponent<SpriteRenderer> ().sprite = partyer.face;
		guigls.Add (ball);
	}

	public void spawnRight() {
		GameObject ball = Instantiate (guiglBallPrefab, rightSpawn.transform.position, Quaternion.identity) as GameObject;
		float modifier = Random.Range (-1.0f * launchSpeedModifier, launchSpeedModifier);
		ball.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * (launchSpeed+modifier));
		ball.GetComponent<SpriteRenderer> ().sprite = partyer.face;
		guigls.Add (ball);
	}

	public void dunk() {
		partyer.givePoints (pointsToGive);
	}
}

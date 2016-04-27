using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CircleCollider2D))]
//[RequireComponent(typeof(SpriteRenderer))]
public class Button : MonoBehaviour {
	private Partyer partyer;
	private CircleCollider2D detector;
	private SpriteRenderer partyFace;
	private bool noteHere;

	// Use this for initialization
	void Awake () {
		detector = GetComponent<CircleCollider2D>();
		//Debug.Log ("15 detector is null: "+ (detector == null));
		partyFace = GetComponent<SpriteRenderer>();
//		Debug.Log ("detector is null: "+ (detector == null));
//		Debug.Log ("partyFace is null: "+ (detector == null));
	}

	void Start() {
		float ratio = 0.17f;
		transform.localScale = new Vector3(ratio, ratio, ratio);
		detector.radius  = 1; //probably want a slight +- buffer
		detector.enabled = true;

		//partyFace.sprite  = partyer.face;
		partyFace.enabled = true;
//		Debug.Log ("start: detector is null: "+ (detector == null));
//		Debug.Log ("start: partyFace is null: "+ (detector == null));
		noteHere = false;
	}

	void OnTriggerEnter2D(Collider2D note) {
		noteHere = true;
	}

	void OnTriggerExit2D(Collider2D note) {
		noteHere = false;
	}

	/* note, on -> +1
	 * note, off -> -1
	 * no note, on -> -1
	 * no note, off -> 0
	 */
	public void updatePlayerScore() {
		int points;
		if (noteHere && isActive ()) {
			points = 30;
		} else if (noteHere && !isActive ()) {
			points = -1;
		} else if (!noteHere && isActive ()) {
			points = -3;
		} else {
			points = 0;
		}
		partyer.givePoints(points);
	}
		
	public void setActive(bool isActive) {
		//detector.enabled  = isActive;
		partyFace.enabled = isActive;
	}

	public bool isActive() {
		return partyFace.enabled;
	}

	public void newPartyer(Partyer p) {
		partyer = p;
//		Debug.Log ("newPartyer: partyFace is null: "+ (detector == null));
//		Debug.Log ("newPartyer: p is null: "+ (p == null));
		partyFace.sprite = p.face;
	}

	public void setPosn(Vector3 v) {
		transform.position = v;
	}
}

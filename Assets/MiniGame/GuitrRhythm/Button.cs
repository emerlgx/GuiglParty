using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CircleCollider2D))]
//[RequireComponent(typeof(SpriteRenderer))]
public class Button : MonoBehaviour {
	private Partyer partyer;
	private CircleCollider2D detector;
	private SpriteRenderer partyFace;

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
		detector.enabled = false;

		//partyFace.sprite  = partyer.face;
		partyFace.enabled = true;
//		Debug.Log ("start: detector is null: "+ (detector == null));
//		Debug.Log ("start: partyFace is null: "+ (detector == null));
	}
		
	public void setActive(bool isActive) {
		detector.enabled  = isActive;
		partyFace.enabled = isActive;
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

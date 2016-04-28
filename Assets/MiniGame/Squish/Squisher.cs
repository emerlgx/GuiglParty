using UnityEngine;
using System.Collections;

public class Squisher : MonoBehaviour {
	public GameObject squishGame;
	public GameObject explosionPrefab;

	public float startPos = -4;
	public float endPos = 5;
	public float speed;

	bool isSquishing = false;
	bool isReceding = false;

	void FixedUpdate() {
		if (isSquishing) {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
			if (transform.localPosition.x >= endPos) {
				isSquishing = false;
				isReceding = true;
			}
		} else if (isReceding) {
			transform.Translate (Vector3.down * speed * Time.deltaTime);
			if (transform.localPosition.x <= startPos) {
				isSquishing = false;
				isReceding = false;
			}
		}
	}

	public void Squish () {
		if (!isReceding) {
			isSquishing = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Instantiate (explosionPrefab, coll.transform.position, Quaternion.identity);
			Destroy (coll.gameObject);
			squishGame.SendMessage ("squished");
		}
	}
}
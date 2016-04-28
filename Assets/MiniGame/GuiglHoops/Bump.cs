using UnityEngine;
using System.Collections;

public class Bump : MonoBehaviour {
	public float height = 2;
	public float speed = 5;

	bool isBumping = false;
	float startHeight;

	void Start() {
		startHeight = this.transform.localPosition.y;
	}


	void FixedUpdate () {
		if (isBumping && transform.localPosition.y <= height + startHeight) {
			//newPos.y += Time.deltaTime * speed;
			//transform.position = newPos;
			this.GetComponent<Rigidbody2D> ().velocity = Vector2.up * speed * Time.deltaTime;
		}


		if (transform.localPosition.y >= height + startHeight) {
			this.GetComponent<Rigidbody2D> ().velocity = Vector2.down * speed * Time.deltaTime;
			isBumping = false;
		}
		if (transform.localPosition.y <= startHeight) {
			Vector3 newPos = transform.localPosition;
			newPos.y = startHeight;
			transform.localPosition = newPos;
			isBumping = false;
		}
	}

	public void makeBump() {
		if(!isBumping) {
			isBumping = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class Bump : MonoBehaviour {
	public float height = 2;
	public float speed = 5;

	bool isBumping = false;
	float startHeight;

	void Start() {
		startHeight = this.transform.position.y;
	}


	void FixedUpdate () {
		if (isBumping) {
			Vector3 newPos = transform.position;
			newPos.y += Time.deltaTime * speed;
			transform.position = newPos;
			if (transform.position.y >= height + startHeight) {
				newPos.y = height + startHeight;
				transform.position = newPos;
				isBumping = false;
			}
		} else if(transform.position.y > startHeight) {
			Vector3 newPos = transform.position;
			newPos.y -= Time.deltaTime * speed;
			transform.position = newPos;
		}
	}

	public void makeBump() {
		if(!isBumping) {
			isBumping = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class BusinessFalling : MonoBehaviour {
	public GameObject Move2LeftGame;
	public float startHeight = 5.0f;
	public float distance = 10.0f;
	public float speed = 3.0f;

	void Start() {
		transform.position = new Vector3(transform.position.x,startHeight,transform.position.z);

	}

	void FixedUpdate() {
		transform.Translate(new Vector3(0,-Time.deltaTime * speed,0));

		if(transform.position.y < startHeight - distance) {
			transform.position = new Vector3(transform.position.x,startHeight,transform.position.z);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") {
			Move2LeftGame.SendMessage("respawn");
		}
	}
}
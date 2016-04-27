using UnityEngine;
using System.Collections;

public class HitTarget : MonoBehaviour {
	public GameObject CannonGame;

	public float explosionForce = 100.0f;
	public GameObject explosion;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			CannonGame.SendMessage ("hitTarget");
			float xForce = Random.Range (-1.0f * explosionForce, explosionForce);
			float yForce = Random.Range (-1.0f * explosionForce, explosionForce);

			coll.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(xForce, yForce));

			// show an explosion!
			Instantiate(explosion,transform.position,Quaternion.identity);

			Destroy (this.gameObject);
		}
	}
}

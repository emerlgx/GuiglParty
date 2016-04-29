using UnityEngine;
using System.Collections;

public class DerburgMover : MonoBehaviour {
	private Transform[] chasers; 
	static Vector2[]    bounds;
	private Rigidbody2D rigidForm;

	public float speed;
	public float wallDist = 3.8f;
	public float wallFear = 1000f;

	void Awake() {
		bounds = new Vector2[2]{ 
			new Vector2(-wallDist, -wallDist),
			new Vector2(wallDist, wallDist)
		};
		for (int i = 0; i < 2; i++) {
			bounds [i].x += transform.position.x;
			bounds [i].y += transform.position.y;
		}
		chasers = new Transform[4];
		rigidForm = GetComponent<Rigidbody2D>();
	}
		
	void Update () {
		Vector3 away = Vector3.zero;
		for (int i = 0; i < 4; i++) {
			Vector3 diff = transform.position - chasers [i].position;
			away += diff.normalized * Mathf.Pow(diff.magnitude, -2); 
			//Vector3 cornerDiff = transform.position - corners [i].position;
			//away += cornerFear * cornerDiff.normalized * 
		}
		away = away.normalized;

		float maxDist = 0;
		for (int i = 0; i < 2; i++) {
			float xDiff = transform.position.x - bounds[i].x;
			float yDiff = transform.position.y - bounds[i].y;
			if (xDiff > maxDist) {
				maxDist = xDiff;
			}
			if (yDiff > maxDist) {
				maxDist = yDiff;
			}
		}
		float ratio = maxDist / wallDist;

		Vector3 randomDir = new Vector3(Random.value, Random.value, 0).normalized;
		away = away * (1f - ratio) + randomDir * ratio;
		transform.position += speed * Time.deltaTime * away;
	}

	public void setChasers(Transform[] gos){
		chasers = gos;
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "floor") {
			rigidForm.velocity = new Vector2(rigidForm.velocity.x, -rigidForm.velocity.y);
		} else if (coll.gameObject.tag == "wall") {
			rigidForm.velocity = new Vector2(-rigidForm.velocity.x, rigidForm.velocity.y);
		} else if (coll.gameObject.tag == "chaser") {
			coll.gameObject.GetComponent<ChaserMover> ().partyer.givePoints(100000);
			placeDerberg();
		}
	}

	public void placeDerberg(){
		transform.localPosition = new Vector3 (
			Random.Range (-wallDist, wallDist),
			Random.Range (-wallDist, wallDist),
			0
		);
	}

}

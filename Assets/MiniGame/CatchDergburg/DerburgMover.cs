using UnityEngine;
using System.Collections;

public class DerburgMover : MonoBehaviour {
	private Transform[] chasers; 
	public float speed;

	void Awake() {
		chasers = new Transform[4];
	}
		
	void Update () {
		Vector3 away = transform.position;
		for (int i = 0; i < 4; i++) {
			Vector3 diff = transform.position - chasers [i].position;
			away += diff.normalized * Mathf.Sqrt (diff.magnitude); 
		}
		transform.position = speed * Vector3.Lerp(transform.position, away, Time.deltaTime);

	}

	public void setChasers(Transform[] gos){
		chasers = gos;
	}
}

using UnityEngine;
using System.Collections;

public class DerburgMover : MonoBehaviour {
	private Transform[] chasers; 
	public float speed;

	void Awake() {
		chasers = new Transform[4];
	}
		
	void Update () {
		Vector3 direction = new Vector3(0, 0, 0);
		for (int i = 0; i < 4; i++) {
			Vector3 diff = transform.position - chasers [i].position;
			direction += diff.normalized * Mathf.Sqrt (diff.magnitude); 

		}

	}

	public void setChasers(GameObject[] gos){
		for (int i = 0; i < 4; i++){
			chasers[i] = gos[i].transform;
		}
	}
}

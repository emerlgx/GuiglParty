using UnityEngine;
using System.Collections;

public class AutoDestruct : MonoBehaviour {
	public float timer = 10.0f;

	// Use this for initialization
	void Start () {
		Invoke ("die", timer);
	}

	void die() {
		Destroy (this.gameObject);
	}
}

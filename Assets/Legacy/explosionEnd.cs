using UnityEngine;
using System.Collections;

public class explosionEnd : MonoBehaviour {
	public float delay = 1f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

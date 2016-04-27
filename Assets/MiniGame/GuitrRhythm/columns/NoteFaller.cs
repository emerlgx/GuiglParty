using UnityEngine;
using System.Collections;

public class NoteFaller : MonoBehaviour {
	static public float fallspeed; 
	private Vector3 fallvelocity;

	public int noteLength;
	// Use this for initialization
	void Start () {
		fallvelocity = new Vector2(0, -fallspeed);
		GetComponent<Rigidbody2D>().velocity = fallvelocity;
	}
}

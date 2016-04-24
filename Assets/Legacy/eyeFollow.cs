using UnityEngine;
using System.Collections;

public class eyeFollow : MonoBehaviour {

	public Transform player;
	public float eyeScale;

	private Vector2 center;

	// Use this for initialization
	void Start () {
		center = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 diff = player.position - transform.position;
		diff = diff / diff.magnitude;
		transform.localPosition = center + diff * eyeScale;

	
	}
}

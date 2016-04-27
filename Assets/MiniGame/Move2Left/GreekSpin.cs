using UnityEngine;
using System.Collections;

public class GreekSpin : MonoBehaviour {
	public float speed = 5;

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0,0,speed*Time.deltaTime));
	}
}

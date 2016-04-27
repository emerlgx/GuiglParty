using UnityEngine;
using System.Collections;

public class HoopSwinger : MonoBehaviour {
	public float startpos = 0;	// what part of the shuffle cycle the gourds begin in. in radians
	public float distance = 2;
	public float speed = 0.25f;

	void Update () {
		transform.Rotate (new Vector3 (0, 0, distance * Mathf.Sin (startpos * Mathf.PI)));
		startpos = (startpos + Time.deltaTime*speed) % (2);
	}
}

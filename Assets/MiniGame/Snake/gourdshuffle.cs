using UnityEngine;
using System.Collections;

public class gourdshuffle : MonoBehaviour {
	public float startpos = 0;	// what part of the shuffle cycle the gourds begin in. in radians
	public float distance = 2;
	public float speed = 0.25f;
	public float offset = 0;
	// Update is called once per frame
	void Update () {
		Vector3 tempPos = this.transform.position;
		tempPos.y = offset + distance * Mathf.Sin (startpos * Mathf.PI);
		this.transform.position = tempPos;
		startpos = (startpos + Time.deltaTime*speed) % (2);
	}
}

using UnityEngine;
using System.Collections;

public class swivel : MonoBehaviour {
	public float swivelRate;
	private float angle = 0;
	public float swivelSize;	

	void Update () {
		transform.eulerAngles = new Vector3(0,0, swivelSize * Mathf.Sin(angle));
		angle += Time.deltaTime * swivelRate;
	}
}

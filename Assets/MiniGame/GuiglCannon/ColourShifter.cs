using UnityEngine;
using System.Collections;

public class ColourShifter : MonoBehaviour {
	SpriteRenderer img;

	public float changeRate = 0.1f;
	float startVal = 0.0f;

	void Start () {
		img = this.GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		img.color = new Color (img.color.r, img.color.g, img.color.b, 0.5f + 0.5f * Mathf.Sin(startVal * Mathf.PI));
		startVal = (startVal + Time.deltaTime*changeRate) % 2;



	}
}

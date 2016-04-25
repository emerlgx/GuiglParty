using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Button : MonoBehaviour {
	public Partyer partyer;
	private CircleCollider2D collider;
	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		collider = GetComponent<CircleCollider2D>();
		collider.radius  = Constants.guiglWidth / 2; //probably want a slight +- buffer
		collider.enabled = false;

		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = partyer.face;
		renderer.enabled = false;
	}

	void setActive(bool isActive) {
		collider.enabled = isActive;
		renderer.enabled = isActive;
	}

	void NewPartyer(Partyer p) {
	    
	}
}

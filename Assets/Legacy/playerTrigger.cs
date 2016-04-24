using UnityEngine;
using System.Collections;

public class playerTrigger : MonoBehaviour {


	public GameObject parent;
	private Rigidbody2D parentRigidBody2d;
	private CircleCollider2D parentCollider2d;

	public PhysicsMaterial2D hardBounce;
	public PhysicsMaterial2D softBounce;

	// Use this for initialization
	void Start () {
		parentRigidBody2d = parent.GetComponent<Rigidbody2D> ();
		parentCollider2d = parent.GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void flickerCollider (CircleCollider2D collider) {
		collider.enabled = false;
		collider.enabled = true;
	}

	void onTriggerEnter(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			if (coll.attachedRigidbody.velocity.magnitude >= parentRigidBody2d.velocity.magnitude) {
				parentCollider2d.sharedMaterial = hardBounce;
				flickerCollider(parentCollider2d);
			} else {
				parentCollider2d.sharedMaterial = softBounce;
				flickerCollider(parentCollider2d);
			}
		}
	}

	void onTriggerExit(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			parentCollider2d.sharedMaterial = null;
			flickerCollider(parentCollider2d);
		}
	}
}

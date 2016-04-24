using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {

	public float dampTime = 0.15f;
	public float verticalOffset;
	private Vector3 velocity = Vector3.zero;
	public Transform generator;

	public float pitchScale;
	public float pitchSmoothTime;
	public float pitchBase = 1;
	float currentVelocity = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (generator.GetComponent<Generator>().topPosition) {
			Transform target = generator.GetComponent<Generator>().topPosition;
			Vector3 targetRelPosn = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(
				new Vector3(targetRelPosn.x, verticalOffset, targetRelPosn.z)
			);
			Vector3 destination = new Vector3(transform.position.x, target.position.y, transform.position.z);//transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

			//audio shnitz
			float curPitch = GetComponent<AudioSource>().pitch;
			GetComponent<AudioSource>().pitch = Mathf.SmoothDamp(
				curPitch, pitchBase + pitchScale * target.GetComponent<Rigidbody2D>().velocity.y,
				ref currentVelocity, pitchSmoothTime);
		}
	}
}

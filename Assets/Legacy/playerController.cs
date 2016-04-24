using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float verticalReleaseForce;
	public float releaseForce;
	public float verticalInitialReleaseForce;
	public float initialReleaseForce;
	public float maxReleaseForce;
	public float maxVerticalReleaseForce;
	public float yVelocity;

	public float rotationScalar = .5f;

	public float releaseForceIncreaseRate;
	public float verticalReleaseForceIncreaseRate;
	public KeyCode left;
	public KeyCode right;

	public SpriteRenderer leftArrow;
	public SpriteRenderer rightArrow;

	private Quaternion leftArrowRotation;
	private Quaternion rightArrowRotation;
	private Vector3 leftArrowPosition;
	private Vector3 rightArrowPosition;

	private Rigidbody2D rigidbody2d;
	private Transform transform2d;
	private TrailRenderer trailRenderer;

	public float breakthroughSpeed = 10;

	enum State{Idle, ChargeRight, ChargeLeft, LaunchRight, LaunchLeft} 
	private State currentState;

	// Use this for initialization
	void Start () {
		rightArrow.color = new Color(1f,1f,1f,0f);
		leftArrow.color  = new Color(1f,1f,1f,0f);

		rightArrowRotation = rightArrow.transform.rotation;
		leftArrowRotation = leftArrow.transform.rotation;
		rightArrowPosition = rightArrow.transform.position - transform.position;
		leftArrowPosition = leftArrow.transform.position - transform.position;

		currentState = State.Idle;
		rigidbody2d = GetComponent<Rigidbody2D>();
		transform2d = GetComponent<Transform>();	

		trailRenderer = GetComponent<TrailRenderer> ();
		trailRenderer.sortingOrder = 1241232;
	}

	void resetCharge () {
		releaseForce = 0;
		verticalReleaseForce = 0;
		rightArrow.color  = new Color(1f,1f,1f,0f);
		leftArrow.color  = new Color(1f,1f,1f,0f);
	}
	void Update () {

		if (rigidbody2d.velocity.y > breakthroughSpeed) {
			gameObject.layer = LayerMask.NameToLayer("UpBall");
		}
		else if (rigidbody2d.velocity.y <= 10) {
			gameObject.layer = LayerMask.NameToLayer("DownBall");
		}

		//start charging
		if (currentState != State.ChargeRight && Input.GetKeyDown (right)) {
			resetCharge();
			currentState = State.ChargeRight;
			releaseForce = initialReleaseForce;
			verticalReleaseForce = verticalInitialReleaseForce;
			rightArrow.color = new Color(1f,1f,1f,1f);
		} else if (currentState != State.ChargeLeft && Input.GetKeyDown (left)) {
			resetCharge();
			currentState = State.ChargeLeft;
			releaseForce = initialReleaseForce;
			verticalReleaseForce = verticalInitialReleaseForce;
			leftArrow.color = new Color(1f,1f,1f,1f);
		} 

		//charging
		else if (currentState == State.ChargeRight && Input.GetKey (right)) {
			releaseForce += releaseForceIncreaseRate * Time.deltaTime;
			verticalReleaseForce += verticalReleaseForceIncreaseRate * Time.deltaTime;
			rigidbody2d.angularVelocity = -Mathf.Min(releaseForce, maxReleaseForce)*rotationScalar;
			//rightArrow.color = new Color(1f,1f,1f, Mathf.Min(releaseForce, maxReleaseForce)/maxReleaseForce);
			
		} else if (currentState == State.ChargeLeft && Input.GetKey (left)) {
			releaseForce += releaseForceIncreaseRate * Time.deltaTime;
			verticalReleaseForce += verticalReleaseForceIncreaseRate * Time.deltaTime;
			rigidbody2d.angularVelocity = Mathf.Min(releaseForce, maxReleaseForce)*rotationScalar;
			//leftArrow.color= new Color(1f,1f,1f, Mathf.Min(releaseForce, maxReleaseForce)/maxReleaseForce);
		}

		//release
		else if (currentState == State.ChargeRight && Input.GetKeyUp (right)) {
			currentState = State.LaunchRight;
			float currentRelease = Mathf.Min(releaseForce, maxReleaseForce);
			float currentVerticalRelease = Mathf.Min (verticalReleaseForce, maxVerticalReleaseForce);
			rigidbody2d.AddForce (Vector2.right * currentRelease);
			rigidbody2d.AddForce (Vector2.up * currentVerticalRelease);
			resetCharge();

		} else if (currentState == State.ChargeLeft && Input.GetKeyUp (left)) {
			currentState = State.LaunchLeft;
			float currentRelease = Mathf.Min(releaseForce, maxReleaseForce);
			float currentVerticalRelease = Mathf.Min (verticalReleaseForce, maxVerticalReleaseForce);
			rigidbody2d.AddForce (Vector2.right * currentRelease * -1);
			rigidbody2d.AddForce (Vector2.up * currentVerticalRelease);
			resetCharge();
		}



		
		rightArrow.transform.rotation = rightArrowRotation;
		leftArrow.transform.rotation = leftArrowRotation;
		rightArrow.transform.position = transform.position + rightArrowPosition;
		leftArrow.transform.position = transform.position + leftArrowPosition;
	}
}


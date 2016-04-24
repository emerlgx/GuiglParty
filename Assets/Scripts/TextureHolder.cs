using UnityEngine;
using System.Collections;

public class TextureHolder : MonoBehaviour {
	// References to the RenderTextures
	public GameObject quadP1;
	public GameObject quadP2;
	public GameObject quadP3;
	public GameObject quadP4;

	// Indicates if a screen swap is currently happening
	bool isSwapping = false;

	// Values associated with a screen position swap
	float swapDuration;
	GameObject swapA;
	GameObject swapB;
	Vector3 goalPosA;
	Vector3 goalPosB;

	// Use this for initialization
	void Start () {
		SwapCameras (1, 2, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {

		// if a swap is in progress, do the movement here
		if (isSwapping) {
			swapA.transform.position = new Vector3(Mathf.Lerp (swapA.transform.position.x, goalPosA.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapA.transform.position.y, goalPosA.y, Time.deltaTime * swapDuration),
				Mathf.Lerp (swapA.transform.position.z, goalPosA.z, Time.deltaTime * swapDuration));
			
			swapB.transform.position = new Vector3(Mathf.Lerp (swapB.transform.position.x, goalPosB.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapB.transform.position.y, goalPosB.y, Time.deltaTime * swapDuration),
				Mathf.Lerp (swapB.transform.position.z, goalPosB.z, Time.deltaTime * swapDuration));

			swapDuration -= Time.deltaTime;


			if (swapDuration <= 0) {

			}
		}
	}


	// Begin swapping the positions of two of the screens
	public void SwapCameras(int playerA, int playerB, float duration) {

		// ensure we are not moving a screen to its own position
		if (playerA == playerB) {
			Debug.LogError ("Attempted to Swap a Screen with itself!");
			return;
		}

		// set the swapping-related values
		isSwapping = true;
		swapDuration = duration;

		if (playerA == 1) {
			swapA = quadP1;
		} else if (playerA == 2) {
			swapA = quadP2;
		} else if (playerA == 3) {
			swapA = quadP3;
		} else {
			swapA = quadP4;
		}

		if (playerB == 1) {
			swapB = quadP1;
		} else if (playerB == 2) {
			swapB = quadP2;
		} else if (playerB == 3) {
			swapB = quadP3;
		} else {
			swapB = quadP4;
		}
		
		// the positions that our screens will eventually reach
		goalPosA = swapB.transform.position;
		goalPosB = swapA.transform.position;
	}
}

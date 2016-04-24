using UnityEngine;
using System.Collections;

public class TextureHolder : MonoBehaviour {
	// References to the RenderTextures
	public GameObject quadP1;
	public GameObject quadP2;
	public GameObject quadP3;
	public GameObject quadP4;

	// Indicates if a screen swap is currently happening
	bool isSwapping2 = false;
	bool isSwapping3 = false;

	// Values associated with a screen position swap
	float swapDuration;
	GameObject swapA;
	GameObject swapB;
	GameObject swapC;
	GameObject swapD;
	Vector3 goalPosA;
	Vector3 goalPosB;
	Vector3 goalPosC;
	Vector3 goalPosD;

	// Use this for initialization
	void Start () {

		SwapCameras3CounterClockwise (1, 2, 3, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

		// if a swap is in progress, do the movement here
		if (isSwapping2) {
			swapA.transform.position = new Vector3 (Mathf.Lerp (swapA.transform.position.x, goalPosA.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapA.transform.position.y, goalPosA.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapA.transform.position.z, goalPosA.z, Time.deltaTime / swapDuration));
			
			swapB.transform.position = new Vector3 (Mathf.Lerp (swapB.transform.position.x, goalPosB.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapB.transform.position.y, goalPosB.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapB.transform.position.z, goalPosB.z, Time.deltaTime / swapDuration));

			swapDuration -= Time.deltaTime;

			// swap should be almost finished, set everything to be in the correct position
			if (swapDuration <= 0) {
				swapA.transform.position = goalPosA;
				swapB.transform.position = goalPosB;
				isSwapping2 = false;

			}
		} else if (isSwapping3) {
			swapA.transform.position = new Vector3 (Mathf.Lerp (swapA.transform.position.x, goalPosA.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapA.transform.position.y, goalPosA.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapA.transform.position.z, goalPosA.z, Time.deltaTime / swapDuration));

			swapB.transform.position = new Vector3 (Mathf.Lerp (swapB.transform.position.x, goalPosB.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapB.transform.position.y, goalPosB.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapB.transform.position.z, goalPosB.z, Time.deltaTime / swapDuration));

			swapC.transform.position = new Vector3 (Mathf.Lerp (swapC.transform.position.x, goalPosC.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapC.transform.position.y, goalPosC.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapC.transform.position.z, goalPosC.z, Time.deltaTime / swapDuration));

			swapDuration -= Time.deltaTime;

			// swap should be almost finished, set everything to be in the correct position
			if (swapDuration <= 0) {
				swapA.transform.position = goalPosA;
				swapB.transform.position = goalPosB;
				swapC.transform.position = goalPosC;
				isSwapping2 = false;

			}
		}
	}


	// Begin swapping the positions of two of the screens
	public void SwapCameras2(int playerA, int playerB, float duration) {

		// ensure we are not moving a screen to its own position
		if (playerA == playerB) {
			Debug.LogError ("Attempted to Swap a Screen with itself!");
			return;
		}

		// set the swapping-related values
		isSwapping2 = true;
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

	public void SwapCameras3Clockwise(int playerA, int playerB, int playerC, float duration) {

		// ensure we are not moving a screen to its own position
		if (playerA == playerB || playerA == playerC || playerB == playerC) {
			Debug.LogError ("Attempted to Swap a Screen with itself!");
			return;
		}

		// set the swapping-related values
		isSwapping3 = true;
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

		if (playerC == 1) {
			swapC = quadP1;
		} else if (playerC == 2) {
			swapC = quadP2;
		} else if (playerC == 3) {
			swapC = quadP3;
		} else {
			swapC = quadP4;
		}
		// the positions that our screens will eventually reach
		goalPosA = swapB.transform.position;
		goalPosB = swapC.transform.position;
		goalPosC = swapA.transform.position;
	}

	public void SwapCameras3CounterClockwise(int playerA, int playerB, int playerC, float duration) {

		// ensure we are not moving a screen to its own position
		if (playerA == playerB || playerA == playerC || playerB == playerC) {
			Debug.LogError ("Attempted to Swap a Screen with itself!");
			return;
		}

		// set the swapping-related values
		isSwapping3 = true;
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

		if (playerC == 1) {
			swapC = quadP1;
		} else if (playerC == 2) {
			swapC = quadP2;
		} else if (playerC == 3) {
			swapC = quadP3;
		} else {
			swapC = quadP4;
		}
		// the positions that our screens will eventually reach
		goalPosA = swapC.transform.position;
		goalPosB = swapA.transform.position;
		goalPosC = swapB.transform.position;
	}
}

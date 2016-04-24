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
	bool isSwapping4 = false;

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

	// Values associated with a screen flip
	float flipDuration;
	float maxFlipDuration;
	bool isFlipping = false;
	bool isFlipping1P = false;
	bool isFlipping2P = false;
	bool isFlipping3P = false;
	bool isFlipping4P = false;

	// Use this for initialization
	void Start () {

		//SwapCameras4Clockwise (1, 2, 4, 3, 1.0f);
		flipScreens(true,false,true,true,0.5f);
	}
	
	// change position of the screens based on given instructions
	void Update () {

		// if a flip is in progress, do the rotation here
		if (isFlipping) {
			if (isFlipping1P) {
				quadP1.transform.rotation *= Quaternion.AngleAxis(180 * Time.deltaTime / maxFlipDuration, Vector3.forward);
			}
			if (isFlipping2P) {
				quadP2.transform.rotation *= Quaternion.AngleAxis(180 * Time.deltaTime / maxFlipDuration, Vector3.forward);
			}
			if (isFlipping3P) {
				quadP3.transform.rotation *= Quaternion.AngleAxis(180 * Time.deltaTime / maxFlipDuration, Vector3.forward);
			}
			if (isFlipping4P) {
				quadP4.transform.rotation *= Quaternion.AngleAxis(180 * Time.deltaTime / maxFlipDuration, Vector3.forward);
			}

			// flip is almost finished, snap to the correct value
			flipDuration -= Time.deltaTime;
			if (flipDuration <= 0.0f) {
				isFlipping = false;

				if (quadP1.transform.rotation.z >= 270.0f || quadP1.transform.rotation.z <= 90.0f) {
					quadP1.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
				}
				if (quadP2.transform.rotation.z >= 270.0f || quadP2.transform.rotation.z <= 90.0f) {
					quadP2.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
				}
				if (quadP3.transform.rotation.z >= 270.0f || quadP3.transform.rotation.z <= 90.0f) {
					quadP3.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
				}
				if (quadP4.transform.rotation.z >= 270.0f || quadP4.transform.rotation.z <= 90.0f) {
					quadP4.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
				}
					
				if (quadP1.transform.rotation.z < 270.0f && quadP1.transform.rotation.z > 90.0f) {
					quadP1.transform.rotation = Quaternion.AngleAxis (180, Vector3.forward);
				}
				if (quadP2.transform.rotation.z < 270.0f && quadP2.transform.rotation.z > 90.0f) {
					quadP2.transform.rotation = Quaternion.AngleAxis (180, Vector3.forward);
				}
				if (quadP3.transform.rotation.z < 270.0f && quadP3.transform.rotation.z > 90.0f) {
					quadP3.transform.rotation = Quaternion.AngleAxis (180, Vector3.forward);
				}
				if (quadP4.transform.rotation.z < 270.0f && quadP4.transform.rotation.z > 90.0f) {
					quadP4.transform.rotation = Quaternion.AngleAxis (180, Vector3.forward);
				}

				SendMessageUpwards ("DoneFlipping");
			}
			
		}

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
				SendMessageUpwards ("DoneSwapping");
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
				isSwapping3 = false;
				SendMessageUpwards ("DoneSwapping");
			}
		} else if (isSwapping4) {
			swapA.transform.position = new Vector3 (Mathf.Lerp (swapA.transform.position.x, goalPosA.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapA.transform.position.y, goalPosA.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapA.transform.position.z, goalPosA.z, Time.deltaTime / swapDuration));

			swapB.transform.position = new Vector3 (Mathf.Lerp (swapB.transform.position.x, goalPosB.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapB.transform.position.y, goalPosB.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapB.transform.position.z, goalPosB.z, Time.deltaTime / swapDuration));

			swapC.transform.position = new Vector3 (Mathf.Lerp (swapC.transform.position.x, goalPosC.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapC.transform.position.y, goalPosC.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapC.transform.position.z, goalPosC.z, Time.deltaTime / swapDuration));

			swapD.transform.position = new Vector3 (Mathf.Lerp (swapD.transform.position.x, goalPosD.x, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapD.transform.position.y, goalPosD.y, Time.deltaTime / swapDuration),
				Mathf.Lerp (swapD.transform.position.z, goalPosD.z, Time.deltaTime / swapDuration));
			

			swapDuration -= Time.deltaTime;

			// swap should be almost finished, set everything to be in the correct position
			if (swapDuration <= 0) {
				swapA.transform.position = goalPosA;
				swapB.transform.position = goalPosB;
				swapC.transform.position = goalPosC;
				swapD.transform.position = goalPosD;
				isSwapping4 = false;
				SendMessageUpwards ("DoneSwapping");
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

	public void SwapCameras4Clockwise(int playerA, int playerB, int playerC, int playerD, float duration) {

		// ensure we are not moving a screen to its own position
		if (playerA == playerB || playerA == playerC || playerA == playerD || 
			playerB == playerC || playerB == playerD || playerC == playerD) {
			Debug.LogError ("Attempted to Swap a Screen with itself!");
			return;
		}

		// set the swapping-related values
		isSwapping4 = true;
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

		if (playerD == 1) {
			swapD = quadP1;
		} else if (playerD == 2) {
			swapD = quadP2;
		} else if (playerD == 3) {
			swapD = quadP3;
		} else {
			swapD = quadP4;
		}

		// the positions that our screens will eventually reach
		goalPosA = swapB.transform.position;
		goalPosB = swapC.transform.position;
		goalPosC = swapD.transform.position;
		goalPosD = swapA.transform.position;
	}

	public void SwapCameras4CounterClockwise(int playerA, int playerB, int playerC, int playerD, float duration) {

		// ensure we are not moving a screen to its own position
		if (playerA == playerB || playerA == playerC || playerA == playerD || 
			playerB == playerC || playerB == playerD || playerC == playerD) {
			Debug.LogError ("Attempted to Swap a Screen with itself!");
			return;
		}

		// set the swapping-related values
		isSwapping4 = true;
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

		if (playerD == 1) {
			swapD = quadP1;
		} else if (playerD == 2) {
			swapD = quadP2;
		} else if (playerD == 3) {
			swapD = quadP3;
		} else {
			swapD = quadP4;
		}

		// the positions that our screens will eventually reach
		goalPosA = swapD.transform.position;
		goalPosB = swapA.transform.position;
		goalPosC = swapB.transform.position;
		goalPosD = swapC.transform.position;
	}

	// indicates that a player's screen should flip 180 degrees
	public void flipScreens(bool p1, bool p2, bool p3, bool p4, float duration) {
		isFlipping = true;
		isFlipping1P = p1;
		isFlipping2P = p2;
		isFlipping3P = p3;
		isFlipping4P = p4;

		flipDuration = duration;
		maxFlipDuration = duration;
	}
}
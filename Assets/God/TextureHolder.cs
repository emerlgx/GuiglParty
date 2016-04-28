using UnityEngine;
using System.Collections;

public class TextureHolder : MonoBehaviour {
	// References to the RenderTextures
	private GameObject[] quads;

	// Indicates if a screen swap is currently happening
	int howManySwapping = 0;
	bool[] isFlipping;
	bool isAnyFlipping = false;

	// Values associated with a screen position swap
	float swapDuration;
	GameObject[] swappingTextures;

	Vector3[] goalPosns;
	Vector3[] startPosns;

	// Values associated with a screen flip
	float flipDuration;
	float maxFlipDuration;

	void Awake(){
		isFlipping = new bool[4]{ false, false, false, false };
		quads = new GameObject[4];
		swappingTextures = new GameObject[4];
		goalPosns = new Vector3[4];
		startPosns = new Vector3[4];
	}

	// Use this for initialization
	void Start() {
		for (int i = 0; i < 4; i++) {
			quads[i] = transform.FindChild("Q"+(i+1)+"Texture").gameObject;
			startPosns[i] = quads [i].transform.position;
		}
	}


	// change position of the screens based on given instructions
	void Update () {
		// if a flip is in progress, do the rotation here
		if (isAnyFlipping) {
			for (int i = 0; i < 4; i++) {
				if (!isFlipping [i])
					continue;
				quads[i].transform.rotation *= Quaternion.AngleAxis(180 * Time.deltaTime / maxFlipDuration, Vector3.forward);

			}
			// flip is almost finished, snap to the correct value
			flipDuration -= Time.deltaTime;
			if (flipDuration <= 0.0f) {
				isAnyFlipping = false;

				for (int i = 0; i < 4; i++) {
					float angle = quads [i].transform.eulerAngles.z;
					if (angle >= 270.0f || angle <= 90.0f) {
						quads[i].transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
					}
					if (angle < 270.0f && angle > 90.0f) {
						quads[i].transform.rotation = Quaternion.AngleAxis (180, Vector3.forward);
					}
				}
			}
		}

		// if a swap is in progress, do the movement here
		if (howManySwapping > 0) {
			for (int i = 0; i < howManySwapping; i++) {
				Vector3 posn = swappingTextures [i].transform.position;
				swappingTextures[i].transform.position = new Vector3 (
					Mathf.Lerp (posn.x, goalPosns[i].x, Time.deltaTime / swapDuration),
					Mathf.Lerp (posn.y, goalPosns[i].y, Time.deltaTime / swapDuration),
					Mathf.Lerp (posn.z, goalPosns[i].z, Time.deltaTime / swapDuration)
				);
			}
			swapDuration -= Time.deltaTime;

			// swap should be almost finished, set everything to be in the correct position
			if (swapDuration <= 0) {
				for(int i = 0 ; i < howManySwapping; i++) {
					swappingTextures[i].transform.position = goalPosns[i];
				}
				howManySwapping = 0;

				for (int i = 0; i < 4; i++) {
					quads[i].GetComponent<MeshRenderer>().materials [0].SetColor(0, new Color(1f, 1f, 1f, 1f));
				}
			}
		}
	}
		
	// Begin swapping the positions of two of the screens
	public void SwapCameras(int[] swaps, float duration) {
		for (int i = 0; i < 4; i++) {
			quads[i].GetComponent<MeshRenderer>().materials [0].SetColor(0, new Color(1f, 1f, 1f, 0.3f));
		}

		// set the swapping-related values
		howManySwapping = swaps.Length;
		swapDuration = duration;

		int direction = UnityEngine.Random.Range (0, 1);
		int[] nums = { 0, 1, 2, 3};
		for (int i = 0; i < swaps.Length; i++) {
			int fromQuad = swaps[i];
			int toQuad;
			if (direction == 0) {
				toQuad = swaps [(i + 1) % swaps.Length];
			} else {
				toQuad = swaps [(i - 1) % swaps.Length];
			}
			Debug.Log("swap from "+fromQuad+" to "+toQuad);
			swappingTextures[i] = quads[fromQuad];
			goalPosns[i] = quads[toQuad].transform.position;
		}

	}

	// indicates that a player's screen should flip 180 degrees
	public void flipScreens(bool[] screens, float duration) {
		if (screens.Length != 4) {
			Debug.Log("screen flipping args bad");
		}
		isFlipping = screens;
		for(int i = 0; i < 4; i++){
			if (isFlipping [i]) {
				isAnyFlipping = true;
				break;
			}
		}

		flipDuration = duration;
		maxFlipDuration = duration;
	}

	public void revertScreens() {
		// rearrange screens back to the initial position
		isAnyFlipping = false;
		flipDuration = 0.0f;
		howManySwapping = 0;
		swapDuration = 0.0f;
		for (int i = 0; i < 4; i++) {
			isFlipping [i] = false;
			quads [i].transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
			quads [i].transform.position = startPosns [i];
		}
	}
}
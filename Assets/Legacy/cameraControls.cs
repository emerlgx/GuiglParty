using UnityEngine;
using System.Collections;

public class cameraControls : MonoBehaviour {
	public GameObject player1;
	//public GameObject player2;

	public float heightAbove; //hieght above highest player
	public float heightDamping;


	public static cameraControls Instance;
	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	//	float highest = Mathf.Max (player1.transform.position.y, player2.transform.position.y);
		float highest = player1.transform.position.y;
		float wantedHeight = highest + heightAbove;
		float currentHeight = this.transform.position.y;

		wantedHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		Vector2 newPosn = this.transform.position;
			    newPosn.y = wantedHeight;
		this.transform.position = newPosn;
	}
}

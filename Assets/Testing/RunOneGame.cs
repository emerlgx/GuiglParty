using UnityEngine;
using System.Collections;

public class RunOneGame : MonoBehaviour {

	public GameObject camera;

	void Start () {
		camera.GetComponent<MiniGameRef>();
	}
	
	void Update () {
		bool left   = Input.GetKey(KeyCode.A);
		bool middle = Input.GetKey(KeyCode.S);		
		bool right  = Input.GetKey(KeyCode.D);
		InputSet input = new InputSet(left, middle, right);

		MiniGame mg = camera.GetComponent<MiniGameRef>().game;
		mg.tick(input);
	}
}

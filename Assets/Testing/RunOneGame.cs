using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class RunOneGame : MonoBehaviour {

	public GameObject gameCam;

	void Start () {
		//sort of check things are properly assigned
		MiniGameRef mgr = gameCam.GetComponent<MiniGameRef>();
		MiniGame game   = mgr.game;
		//should check that game not null
	}
	
	void Update () {
		bool left   = Input.GetKey(KeyCode.A);
		bool middle = Input.GetKey(KeyCode.S);		
		bool right  = Input.GetKey(KeyCode.D);
		InputSet input = new InputSet(left, middle, right);

		MiniGame mg = gameCam.GetComponent<MiniGameRef>().game;
		mg.tick(input);
	}
}

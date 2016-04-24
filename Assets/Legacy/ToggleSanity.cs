using UnityEngine;
using System.Collections;

public class ToggleSanity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string currentLevel = Application.loadedLevelName;
		if (Input.GetKey ("escape")) {
			if (currentLevel == "Halfpipes") {
				Application.LoadLevel ("Clean");
			}
			else {
				Application.LoadLevel ("Halfpipes");
			}
		}
	}
}

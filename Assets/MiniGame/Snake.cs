using UnityEngine;
using System.Collections;

public class Snake : MiniGame {
	InputSet inputSet;

	public GameObject SnakeHead;

	void Start () {
		
	}
	
	void Update () {
	
	}

	void tick (InputSet input) {
		inputSet = input;
	}
}

using UnityEngine;
using System.Collections;

public class Snake : MiniGame {
	InputSet inputSet;

	public GameObject SnakeHead;

	void Start () {
		
	}
	
	void Update () {
	
	}

	public override void tick(InputSet input) {
		inputSet = input;
	}

	public override void control(ControlCommand cmd) {
	}
}

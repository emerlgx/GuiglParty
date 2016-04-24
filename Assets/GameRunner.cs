using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//from KeyInput import InputSet;

public class God : MonoBehaviour {
	public KeyInput keyInput;
	public InputCoordinator inputCoord;
	public TextureHolder swapper;
	public GameObject camera1;
	public GameObject camera2;
	public GameObject camera3;
	public GameObject camera4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool iwannaswap = false;
		//decide whether to swap
		if (iwannaswap) {
			//if swap: 
			//  send command to swap
			//  while swapping:
			//    roll aesop or something
			int noop = 1;
		} else {
			InputSet[] inputs = keyInput.getInputs();
			//Dictionary<GameObject, InputSet> = inputCoord.organizeInputs(inputs);
			//  coordinate inputs
			//  send inputs to games and run one tick
		}	   
	}
}

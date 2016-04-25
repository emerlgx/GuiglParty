using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//from KeyInput import InputSet;

public class God : MonoBehaviour {
	public KeyInput keyInput;
	public TextureHolder swapper;
	public GameObject[] cameras; //index is partyer. change when player/quadrant swap

	private int[] keyboardPlayerMap; //update when player/keyboard swap 

	// Use this for initialization
	void Start () {
		keyboardPlayerMap = new int[4]{ 
			(int)PartyNames.Guigl,      (int)PartyNames.Ubaldino, 
			(int)PartyNames.Walusneaki, (int)PartyNames.Blooch 
		};
		foreach (GameObject camera in cameras) {
			camera.GetComponent<MiniGameRef>();
		}
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
			Dictionary<MiniGame, InputSet> matchedInputs = organizeInputs(inputs);
			foreach (GameObject camera in cameras) {
				MiniGame mg = camera.GetComponent<MiniGameRef>().game;
				InputSet input = new InputSet(false, false, false);
				matchedInputs.TryGetValue(mg, out input);
				mg.tick(input);
			}
		}	   
	}

	public Dictionary<MiniGame, InputSet> organizeInputs(InputSet[] inputs) {
		Dictionary<MiniGame, InputSet> gameInputs = new Dictionary<MiniGame, InputSet>();
		for (int i = 0; i < 4; i++) {
			MiniGame rightGame = cameras[ keyboardPlayerMap[i] ].GetComponent<MiniGameRef>().game;
			gameInputs.Add(rightGame, inputs[i]);
		}
		return gameInputs;
	}
}
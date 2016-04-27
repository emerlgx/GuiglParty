using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//from KeyInput import InputSet;

public class God : MonoBehaviour {
	public GameObject[] allGames;
	private string[]    names;
	public Sprite[]     sprites;
	private Partyer[]   partyers;

	private GameObject[]  gameCams; //index is partyer. change when player/quadrant swap
	private int[]         keyboardPlayerMap; //update when player/keyboard swap 
	private MiniGame[]    miniGames;
	private TextureHolder swapper;
	private KeyInput      inputManager;

	// the positions of some screens will change depending on these values
	public float screenSwapTimeMax = 60.0f;	// max time between swaps
	public float screenSwapTimeMin = 15.0f;	// minimum time between swaps
	public float screenSwapDuration = 1.0f;	// how long the actual swap operation will take
	private float screenSwapCounter;		// holds the time until the next swap

	// Use this for initialization
	void Awake() {
		keyboardPlayerMap = new int[4]{ 
			(int)PartyNames.Guigl,      (int)PartyNames.Ubaldino, 
			(int)PartyNames.Walusneaki, (int)PartyNames.Blooch 
		};

		swapper = GetComponent<TextureHolder>();
		inputManager = GetComponent<KeyInput>();

		miniGames = new MiniGame[4];

		names = new string[4] { "Guigl", "Ubaldino", "Walusneaki", "Blooch" };
		gameCams = new GameObject[4];
		for (int i = 0; i < 4; i++) {
			gameCams[i] = transform.FindChild("Q"+(i+1)+"Cam").gameObject;
		}

		partyers = new Partyer[4];
	}

	void Start() {
		System.Random rnd = new System.Random();
		for (int i = 0; i < 4; i++) {
			GameObject miniGameInstance = Instantiate(allGames[rnd.Next(allGames.Length)]);
			//GameObject miniGameInstance = Instantiate(allGames[1]);
			Vector3 posn = gameCams	[i].transform.position;
			        posn.z = 0;
			miniGameInstance.transform.position = posn; 
			miniGameInstance.transform.SetParent(gameCams[i].transform);

			miniGames[i] = miniGameInstance.GetComponent<MiniGame>();
			partyers[i]  = new Partyer(names[i], sprites[i], Constants.lights[i], Constants.darks[i]);
			miniGames[i].setPartyer(partyers[i]);

			screenSwapCounter = UnityEngine.Random.Range (screenSwapTimeMin, screenSwapTimeMax);
		}

		//InvokeRepeating("updateScore", 0, 1);
	}

	// Update is called once per frame
	void Update () {
		bool iwannaswap = false;
		//decide whether to swap
		if (screenSwapCounter <= 0.0f) {
			iwannaswap = true;
			screenSwapCounter = UnityEngine.Random.Range (screenSwapTimeMin, screenSwapTimeMax);
			screenSwap ();
		} else {
			screenSwapCounter -= Time.deltaTime;
		}

		if (iwannaswap) {
			//if swap: 
			//  send command to stop games
			//  while swapping:
			//    roll aesop or something
		} else {
			InputSet[] inputs = inputManager.getInputs();
			Dictionary<MiniGame, InputSet> matchedInputs = organizeInputs(inputs);
			foreach (GameObject camera in gameCams) {
				MiniGame mg = camera.GetComponentInChildren<MiniGame>();
				InputSet input = new InputSet(false, false, false);
				matchedInputs.TryGetValue(mg, out input);
				mg.tick(input);
			}
		}	   
	}

	public Dictionary<MiniGame, InputSet> organizeInputs(InputSet[] inputs) {
		Dictionary<MiniGame, InputSet> gameInputs = new Dictionary<MiniGame, InputSet>();
		for (int i = 0; i < 4; i++) {
			MiniGame rightGame = gameCams[ keyboardPlayerMap[i] ].GetComponentInChildren<MiniGame>();
			gameInputs.Add(rightGame, inputs[i]);
		}
		return gameInputs;
	}

	// randomly swap positions of some number of rendertextures
	void screenSwap() {
		// choose to swap 2, 3, or 4 screens
		int rnd = UnityEngine.Random.Range(2,5);
		switch (rnd) {
		case 2:
			int swapA = UnityEngine.Random.Range (1, 4);
			int swapB = UnityEngine.Random.Range (1, 4);
			while (swapA == swapB) {
				swapA = UnityEngine.Random.Range (1, 4);
				swapB = UnityEngine.Random.Range (1, 4);
			}
			swapper.SwapCameras2 (swapA, swapB, screenSwapDuration);
			break;
		case 3:
			// choose the order of the 3 screens to be swapped
			int[] textures3 = { 1, 2, 3, 4 };
			Constants.ShuffleArray (textures3);
			// choose whether to go clockwise or counterclockwise
			int direction = UnityEngine.Random.Range (0, 1);
			if (direction == 0) {
				swapper.SwapCameras3Clockwise (textures3 [0], textures3 [1], textures3 [2], screenSwapDuration);
			} else { 
				swapper.SwapCameras3CounterClockwise (textures3 [0], textures3 [1], textures3 [2], screenSwapDuration);
			}
			break;
		case 4:
			// choose the order of the 3 screens to be swapped
			int[] textures4 = { 1, 2, 3, 4 };
			Constants.ShuffleArray (textures4);
			// choose whether to go clockwise or counterclockwise
			direction = UnityEngine.Random.Range (0, 1);
			if (direction == 0) {
				swapper.SwapCameras4Clockwise (textures4 [0], textures4 [1], textures4 [2], textures4 [3], screenSwapDuration);
			} else { 
				swapper.SwapCameras4CounterClockwise (textures4 [0], textures4 [1], textures4 [2], textures4 [3], screenSwapDuration);
			}
			break;
		}
	}
}
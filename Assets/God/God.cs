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

	private GameObject scoreTemplate;
	private GameObject[] scoreBoards;

	// the positions of some screens will change depending on these values
	public float screenSwapTimeMax = 60.0f;	// max time between swaps
	public float screenSwapTimeMin = 15.0f;	// minimum time between swaps
	public float screenSwapDuration = 1.0f;	// how long the actual swap operation will take
	private float screenSwapCounter;		// holds the time until the next swap

	public float afterSwapPause = 1.0f;

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
		for (int i = 0; i < 4; i++) {
			partyers [i] = new Partyer (names [i], sprites [i], Constants.lights [i], Constants.darks [i]);
		}

		scoreTemplate = transform.FindChild("scoreDisplayTemplate").gameObject;
		scoreBoards   = new GameObject[4];
	}

	void Start() {
		Vector3[] scoreBoardPosns = new Vector3[4] {
			new Vector3(-4.6f,  3.3f, 0f),
			new Vector3( 4.6f,  3.3f, 0f),
			new Vector3(-4.6f, -3.3f, 0f),
			new Vector3( 4.6f, -3.3f, 0f)
		};

		System.Random rnd = new System.Random();
		for (int i = 0; i < 4; i++) {
			GameObject miniGameInstance = Instantiate(allGames[rnd.Next(allGames.Length)]);
			//GameObject miniGameInstance = Instantiate(allGames[1]);
			Vector3 posn = gameCams	[i].transform.position;
			        posn.z = 0;
			miniGameInstance.transform.position = posn; 
			miniGameInstance.transform.SetParent(gameCams[i].transform);

			miniGames[i] = miniGameInstance.GetComponent<MiniGame>();
			miniGames[i].setPartyer(partyers[i]);

			scoreBoards[i] = Instantiate(scoreTemplate) as GameObject;
			scoreBoards[i].transform.localPosition = scoreBoardPosns[i];
			scoreBoards[i].GetComponent<ScoreDisplay>().assignPlayer(partyers[i]);
			Debug.Log("Assigned scorboard "+i);

			screenSwapCounter = UnityEngine.Random.Range(screenSwapTimeMin, screenSwapTimeMax);
		}
	}

	// Update is called once per frame
	void Update () {
		//decide whether to swap
		if (screenSwapCounter <= 0.0f) {
			screenSwapCounter = UnityEngine.Random.Range(screenSwapTimeMin, screenSwapTimeMax);
			swapSomething();
		} else {
			screenSwapCounter -= Time.deltaTime;
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

	void swapSomething(){
		int[] swappers = choosePlayers();

		for (int i = 0; i < 4; i++) {
			if (Array.IndexOf(swappers, i) < 0)
				continue;
			miniGames[i].setInSwap(true); 
		} 

		if (true) {
			swapper.SwapCameras(swappers, screenSwapDuration);
		}
		for (int i = 0; i < 4; i++) {
			if (Array.IndexOf(swappers, i) < 0)
				continue;
			miniGames[i].setInSwap(false); 
		} 
	}

	int[] choosePlayers(){
		int numToSwitch = UnityEngine.Random.Range(2,5);
		int[] order = { 0, 1, 2, 3 };
		Constants.ShuffleArray(order);
		int[] swappers = new int[numToSwitch];
		Array.Copy(order, 0, swappers, 0, numToSwitch);
		return swappers;
	}

	// randomly swap positions of some number of rendertextures
}
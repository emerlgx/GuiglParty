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
		}

		//InvokeRepeating("updateScore", 0, 1);
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
}
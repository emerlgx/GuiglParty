using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//from KeyInput import InputSet;

public class RunOne4PlayerGame : MonoBehaviour {
	public GameObject game4P;
			
	private string[]    names;
	public  Sprite[]    sprites;
	private Partyer[]   partyers;

	private GameObject[]  gameCams; //index is partyer. change when player/quadrant swap
	private int[]         keyboardPlayerMap; //update when player/keyboard swap 
	private MiniGame[]    miniGames;

	private KeyInput      inputManager;

	private GameObject scoreTemplate;
	private GameObject[] scoreBoards;

	private Vector3[] scoreBoardPosns; 

	public GameObject dummyPrefab;		// used for passing input with multiplayer games

	void Awake() {
		keyboardPlayerMap = new int[4]{ 
			(int)PartyNames.Guigl,      (int)PartyNames.Ubaldino, 
			(int)PartyNames.Walusneaki, (int)PartyNames.Blooch 
		};

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
		scoreBoardPosns = new Vector3[4] {
			new Vector3(-4.7f,  3f, 0f),
			new Vector3( 4.7f,  3f, 0f),
			new Vector3(-4.7f, -3f, 0f),
			new Vector3( 4.7f, -3f, 0f)
		};			
	}

	void Start() {
		createNew4PGame();

		for (int i = 0; i < 4; i++) {
			scoreBoards[i] = Instantiate(scoreTemplate) as GameObject;
			scoreBoards[i].transform.localPosition = scoreBoardPosns[i];
			scoreBoards[i].GetComponent<ScoreDisplay>().assignPlayer(partyers[i]);
		}
	}

	void Update () {
		InputSet[] inputs = inputManager.getInputs();
		Dictionary<MiniGame, InputSet> matchedInputs = organizeInputs(inputs);
		foreach (GameObject camera in gameCams) {
			MiniGame mg = camera.GetComponentInChildren<MiniGame>();
			InputSet input = new InputSet(false, false, false);
			matchedInputs.TryGetValue(mg, out input);
			mg.tick(input);
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
		
	void createNew4PGame() {
		GameObject[] gamePieces = new GameObject[4];
		gamePieces[0] = Instantiate(game4P) as GameObject;
		//give dummy games to the other 3 players
		for (int i = 1; i < 4; i++) {
			gamePieces[i] = Instantiate (dummyPrefab) as GameObject;
		}

		for (int i = 0; i < 4; i++) {
			Vector3 posn = gameCams[i].transform.position;
			posn.z = 0;
			gamePieces [i].transform.position = posn; 
			gamePieces [i].transform.SetParent (gameCams [i].transform);

			miniGames [i] = gamePieces [i].GetComponent<MiniGame> ();
			miniGames[i].setPartyer(partyers[i]);
		}
		for (int i = 1; i < 4; i++) {
			miniGames[i].GetComponent<DummyGame>().assignGame(miniGames[0] as MiniGameMulti, i);
		}
		gamePieces [0].transform.position += new Vector3(2f, -2f, 0);
	}
}
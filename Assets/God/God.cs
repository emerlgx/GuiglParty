using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//from KeyInput import InputSet;

public class God : MonoBehaviour {
	public GameObject[][] allGames;
	public GameObject[] games1P;
	public GameObject[] games2P;
	public GameObject[] games3P;
	public GameObject[] games4P;

	// camera positions for when games with varying numbers of players are made
	public Vector3[][] allPosns;
	public Vector3[] posns1P;
	public Vector3[] posns2P;
	public Vector3[] posns3P;
	public Vector3[] posns4P;
		

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
	// screens will flip upside-down depending on these values
	public float screenFlipTimeMax = 60.0f;	// max time between swaps
	public float screenFlipTimeMin = 15.0f;	// minimum time between swaps
	public float screenFlipDuration = 1.0f;	// how long the actual swap operation will take
	private float screenFlipCounter;		// holds the time until the next swap

	private JukeBox jukeBox;
	public float musicSwitchTimeMax = 60.0f;	// max time between swaps
	public float musicSwitchTimeMin = 15.0f;	// minimum time between swaps
	private float musicSwitchCounter;

	private Vector3[] scoreBoardPosns; 

	public float replaceGameChance;
	public float[] playersInGameChance;	// percent chance of a game with # players being chosen. must sum to 1
	public float replaceGameTimeMin;
	public float replaceGameTimeMax;
	private float replaceGameCounter;
	public float gameAnimationDelay4P = 1.0f;	// delay the creation of a 4 player game in order to allow an animation to cover the change
	public GameObject dummyPrefab;		// used for passing input with multiplayer games
	public GameObject explosionGif;		// displayed when switching singleplayer games

	void Awake() {
		keyboardPlayerMap = new int[4]{ 
			(int)PartyNames.Guigl,      (int)PartyNames.Ubaldino, 
			(int)PartyNames.Walusneaki, (int)PartyNames.Blooch 
		};

		swapper = GetComponent<TextureHolder>();
		inputManager = GetComponent<KeyInput>();
		allGames = new GameObject[4][];
		allGames [0] = games1P;
		allGames [1] = games2P;
		allGames [2] = games3P;
		allGames [3] = games4P;

		allPosns = new Vector3[4][];
		allPosns [0] = posns1P;
		allPosns [1] = posns2P;
		allPosns [2] = posns3P;
		allPosns [3] = posns4P;

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
			
		jukeBox = transform.FindChild("JukeBox").GetComponent<JukeBox>();
	}

	void Start() {
		for (int i = 0; i < 4; i++) {
			createNewGame(i, partyers[i]);

			scoreBoards[i] = Instantiate(scoreTemplate) as GameObject;
			scoreBoards[i].transform.localPosition = scoreBoardPosns[i];
			scoreBoards[i].GetComponent<ScoreDisplay>().assignPlayer(partyers[i]);
			Debug.Log("Assigned scorboard "+i);

			screenSwapCounter = UnityEngine.Random.Range (screenSwapTimeMin, screenSwapTimeMax);
			screenFlipCounter = UnityEngine.Random.Range (screenFlipTimeMin, screenFlipTimeMax);
			musicSwitchCounter = UnityEngine.Random.Range(musicSwitchTimeMin, musicSwitchTimeMax);
			replaceGameCounter = UnityEngine.Random.Range(replaceGameTimeMin, replaceGameTimeMax);

			jukeBox.pickSong(musicSwitchCounter);
		}
	}

	void Update () {
		//decide whether to swap
		if (screenSwapCounter <= 0.0f) {
			swapSomething();
			screenSwapCounter = UnityEngine.Random.Range(screenSwapTimeMin, screenSwapTimeMax);
		} else {
			screenSwapCounter -= Time.deltaTime;
		}

		// decide whether to flip
		if (screenFlipCounter <= 0.0f) {
			quadFlip();
			screenFlipCounter = UnityEngine.Random.Range (screenFlipTimeMin, screenFlipTimeMax);
		} else {
			screenFlipCounter -= Time.deltaTime;
		}

		if (musicSwitchCounter <= 0.0f) {
			musicSwitchCounter = UnityEngine.Random.Range(musicSwitchTimeMin, musicSwitchTimeMax);
			jukeBox.pickSong(musicSwitchCounter);
		} else {
			musicSwitchCounter -= Time.deltaTime;
		}

		if (replaceGameCounter <= 0f) {
			replaceGames();
			replaceGameCounter = UnityEngine.Random.Range(replaceGameTimeMin, replaceGameTimeMax);
		} else {
			replaceGameCounter -= Time.deltaTime;
		}

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


	void createNewGame(int camNum, Partyer partyer){
		GameObject miniGameInstance = Instantiate(allGames[0][Random.Range(0, allGames.Length)]) as GameObject;
		Vector3 posn = gameCams[camNum].transform.position;
		posn.z = 0;
		miniGameInstance.transform.position = posn; 
		miniGameInstance.transform.SetParent(gameCams[camNum].transform);

		miniGames[camNum] = miniGameInstance.GetComponent<MiniGame>();
		miniGames[camNum].setPartyer(partyer);
	}

	// replaces all 4 games with a new 4P game
	// built as a coroutine in order to allow for an animation to pass before creating the new games
	IEnumerator createNew4PGame() {
		// display some animation so that the transition looks smooth

		yield return new WaitForSeconds(2);

		// move the textures back to their initial position so that the display will make sense
		swapper.revertScreens();

		// select a 4P game and give it to player 1
		GameObject miniGameInstance = Instantiate(allGames[3][Random.Range(0, allGames.Length)]) as GameObject;
		Vector3 posn = gameCams[0].transform.position;
		posn.z = 0;
		miniGameInstance.transform.position = posn; 
		miniGameInstance.transform.SetParent(gameCams[0].transform);

		miniGames[0] = miniGameInstance.GetComponent<MiniGame>();
		miniGames[0].setPartyer(partyers[0]);

		//give dummy games to the other 3 players
		for (int i = 1; i < 4; i++) {
			GameObject dummyGameInstance = Instantiate (dummyPrefab) as GameObject;
			posn = gameCams[i].transform.position;
			posn.z = 0;
			miniGameInstance.transform.position = posn; 
			miniGameInstance.transform.SetParent(gameCams[i].transform);

			miniGames [i] = miniGameInstance.GetComponent<MiniGame>();
			miniGames [i].setPartyer(partyers[i]);
			miniGames [i].GetComponent<DummyGame> ().assignGame (miniGames [0] as MiniGameMulti, i);
		}


		yield break;
	}

	// replaces an individual singleplayer game
	void replaceGame(int camNum){
		Partyer p = miniGames[camNum].partyer;
		miniGames[camNum].blowUp(explosionGif);
		Destroy(miniGames[camNum].gameObject);
		createNewGame(camNum, p);
	}

	void replaceGames(){
		// select the number of players in the game
		float sum = Random.value;

		if (sum < playersInGameChance [0]) {
			// place the cameras in the correct positions for 1P games
			for (int j = 0; j < 4; j++) {
				gameCams [j].transform.position = allPosns [0] [j];
			}

			// make 4 new 1P games
			for (int j = 0; j < 4; j++) {
				if (Random.value < replaceGameChance) {
					replaceGame (j);
				}
			}
		} else if (sum < playersInGameChance [0] + playersInGameChance [1]) {
			// make a 2P game
			Debug.LogError("2P games can't be made yet!");
		} else if (sum < playersInGameChance [0] + playersInGameChance [1] + playersInGameChance [2]) {
			// make a 3P game
			Debug.LogError("3P games can't be made yet!");
		} else {
			// place the cameras in the correct positions for 4P games
			for (int j = 0; j < 4; j++) {
				gameCams [j].transform.position = allPosns [3] [j];
			}

			// make a 4P game
			StartCoroutine(createNew4PGame());
		}

	}

	void swapSomething(){
		int[] swappers = choosePlayers();

		for (int i = 0; i < 4; i++) {
			if (System.Array.IndexOf(swappers, i) < 0)
				continue;
			miniGames[i].setInSwap(true); 
		} 

		if (true) {
			swapper.SwapCameras(swappers, screenSwapDuration);
		}

		for (int i = 0; i < 4; i++) {
			if (System.Array.IndexOf(swappers, i) < 0)
				continue;
			miniGames[i].setInSwap(false); 
		}
	}

	int[] choosePlayers(){
		int numToSwitch = UnityEngine.Random.Range(2,5);
		int[] order = { 0, 1, 2, 3 };
		Constants.ShuffleArray(order);
		int[] swappers = new int[numToSwitch];
		System.Array.Copy(order, 0, swappers, 0, numToSwitch);
		return swappers;
	}

	void quadFlip() {
		bool[] flipper = { UnityEngine.Random.value > 0.5f, 
			UnityEngine.Random.value > 0.5f, 
			UnityEngine.Random.value > 0.5f, 
			UnityEngine.Random.value > 0.5f };
		
		swapper.flipScreens(flipper, screenFlipDuration);
	}
}
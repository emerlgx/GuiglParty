using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class RunOneGame : MonoBehaviour {
	public GameObject gameCamera;
	public GameObject miniGameObjectTemplate;
	private MiniGame game;

	void Start () {
		GameObject miniGameObject = Instantiate(miniGameObjectTemplate);
		Vector3 posn = gameCamera.transform.position;
		        posn.z = 0;
		miniGameObject.transform.position = posn; 
		miniGameObject.transform.SetParent(gameCamera.transform);

		game = miniGameObject.GetComponent<MiniGame>();
		game.partyer = Instantiate(Resources.Load("Assets/Testing/testPlayer") as GameObject).GetComponent<Partyer>();
	}
	
	void Update () {
		bool left   = Input.GetKey(KeyCode.A);
		bool middle = Input.GetKey(KeyCode.S);		
		bool right  = Input.GetKey(KeyCode.D);
		InputSet input = new InputSet(left, middle, right);

		game.tick(input);
	}
}

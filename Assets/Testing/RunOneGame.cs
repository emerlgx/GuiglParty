using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class RunOneGame : MonoBehaviour {
	public GameObject gameCamera;
	public Sprite playerSprite;
	public GameObject miniGameObjectTemplate;
	private MiniGame game;
	private Partyer partyer;

	void Start () {
		GameObject miniGameObject = Instantiate(miniGameObjectTemplate);
		Vector3 posn = gameCamera.transform.position;
		        posn.z = 0;
		miniGameObject.transform.position = posn; 
		miniGameObject.transform.SetParent(gameCamera.transform);

		game = miniGameObject.GetComponent<MiniGame>();
		partyer = new Partyer();
		partyer.setPartyer("walusneaki", playerSprite);
		game.partyer = partyer;

		InvokeRepeating("updateScore", 0, 1);
	}
	
	void Update () {
		bool left   = Input.GetKey(KeyCode.A);
		bool middle = Input.GetKey(KeyCode.S);
		bool right  = Input.GetKey(KeyCode.D);
		InputSet input = new InputSet(left, middle, right);
		game.tick(input);
	}

	void updateScore() {
		GameObject scoreText = transform.FindChild("ScoreText").gameObject;
		scoreText.GetComponent<TextMesh>().text = "Score: " + partyer.getScore();
	}
}

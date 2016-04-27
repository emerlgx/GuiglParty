using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class RunOneGame : MonoBehaviour {
	public GameObject gameCamera;
	public Sprite playerSprite;
	public GameObject miniGameObjectTemplate;
	private MiniGame game;

	void Start () {
		GameObject miniGameObject = Instantiate(miniGameObjectTemplate);
		Vector3 posn = gameCamera.transform.position;
		        posn.z = 0;
		miniGameObject.transform.position = posn; 
		miniGameObject.transform.SetParent(gameCamera.transform);

		game = miniGameObject.GetComponent<MiniGame>();
		Partyer partyer = new Partyer();
		partyer.setPartyer("test Blooch", playerSprite);
		game.partyer = partyer;
	}
	
	void Update () {
		bool left   = Input.GetKey(KeyCode.A);
		bool middle = Input.GetKey(KeyCode.S);		
		bool right  = Input.GetKey(KeyCode.D);
		InputSet input = new InputSet(left, middle, right);
		game.tick(input);
	}
}

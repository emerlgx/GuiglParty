using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	public float killDistance = 10f;
	public Transform player1;
	public Transform player2;
	public int player1Score;
	public int player2Score;
	public GameObject player1ScoreText;
	public GameObject player2ScoreText;
	public GameObject explosionPrefab;
	//public GameObject tonguePrefab;
	public float explosionOffset;

	// Use this for initialization
	void Start () {
		player1Score = 0;
		player2Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (player1.position.y < (player2.position.y - killDistance)) {
			// TODO: kill player 1
			Explode(player1.position, player2.position);
			player1.position = new Vector3(player2.position.x + .001f,
			                      		   player2.position.y + 3f,
			                      		   player2.position.z + .001f);
			player1.GetComponent<Rigidbody2D>().velocity = player2.GetComponent<Rigidbody2D>().velocity;
			player2Score++;
		}
		if (player2.position.y < (player1.position.y - killDistance)) {
			// TODO: kill player 2
			Explode(player2.position, player1.position);
			player2.position = new Vector3(player1.position.x + .001f,
			                      		   player1.position.y + 3f,
			                               player1.position.z + .001f);
			player2.GetComponent<Rigidbody2D>().velocity = player1.GetComponent<Rigidbody2D>().velocity;

			player1Score++;
		}

		player1ScoreText.GetComponent<Text> ().text = player1Score.ToString();
		player2ScoreText.GetComponent<Text> ().text = player2Score.ToString();
	}

	void Explode (Vector3 dead, Vector3 alive) {
		Instantiate (explosionPrefab, 
		             new Vector3 (dead.x, alive.y - killDistance + explosionOffset, 0), 
		             Quaternion.identity);
		//Instantiate (tonguePrefab, transform.localPosition, Quaternion.identity);	
	}


}

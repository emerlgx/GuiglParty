using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
	private  Partyer partyer;
	private TextMesh textmesh;
	private TextMesh name;
	private SpriteRenderer spriteRender;
	private int playerHighscoreEver;
	//static private int currentAnyHighScore;
	private bool playerAssigned = false;

	void Awake(){
		textmesh = GetComponent<TextMesh>();
		spriteRender = transform.FindChild("face").GetComponent<SpriteRenderer>();
		name = transform.FindChild("name").GetComponent<TextMesh>();
	}

	public void assignPlayer(Partyer p){
		partyer = p;
		playerHighscoreEver = partyer.score;
		spriteRender.sprite = partyer.face;
		textmesh.color      = partyer.darkCol;
		name.text           = partyer.name;
		name.color          = partyer.darkCol;
		playerAssigned = true;
	}

	// Update is called once per frame
	void Update () {
		if (!playerAssigned) {
			textmesh.text = "Score: NOPLAYER";
		} else {
			textmesh.text = "Score: " + partyer.score;
			if (partyer.score > playerHighscoreEver) {
				playerHighscoreEver = partyer.score;
				//maybe some kind of animation
			}
		}
	}
}

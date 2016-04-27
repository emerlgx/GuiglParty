using UnityEngine;
using System.Collections;

public class Partyer {
	public int score;
	public Sprite face;
	public string name;
	public Color lightCol;
	public Color darkCol;

	public Partyer(string name_, Sprite face_, Color lc, Color dc) {
		name     = name_;
		face     = face_;
		lightCol = lc;
		darkCol  = dc;
		score    = 0;
	}

	public void givePoints(int points) {
		score += points;
	}

	public int getScore() {
		return score;
	}

	void Update(){
		Debug.Log(name + "'s score: "+score);
	}
}

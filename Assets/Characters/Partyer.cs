using UnityEngine;
using System.Collections;

public class Partyer : MonoBehaviour {
	public int score;
	public Sprite face;
	public string name;

	void Start() {
		score = 0;
	}

	public void setPartyer(string name_, Sprite face_) {
		name = name_;
		face = face_;
	}

	public void givePoints(int points) {
		score += points;
	}

	public int getScore() {
		return score;
	}
}

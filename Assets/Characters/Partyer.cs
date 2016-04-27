using UnityEngine;
using System.Collections;

public class Partyer {
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
}

using UnityEngine;
using System.Collections;

public class DummyGame : MiniGame {
	private MiniGameMulti minigame;
	private int playerNum;

	public void assignGame(MiniGameMulti mg, int pn){
		minigame  = mg;
		playerNum = pn;
		mg.addPartyer(pn, partyer);
	}

	public override void setPartyer(Partyer p){
		partyer = p;
	}

	public override void tick(InputSet input){
		Debug.Log("passing p"+playerNum+" null: "+(input == null));
		minigame.takeInput(playerNum, input);
	}

	public override void control(ControlCommand cmd) {
		minigame.takeCommand(playerNum, cmd);
	}
}

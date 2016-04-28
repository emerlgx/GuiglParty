using UnityEngine;
using System.Collections;

public abstract class MiniGameMulti : MiniGame {
	protected Partyer[]  partyers;
	protected InputSet[] inputs;

	public virtual void addPartyer(int index, Partyer p){
		partyers[index] = p;
	}

	public void takeInput(int index, InputSet input){
		inputs[index] = input;
	}

	public abstract void takeCommand(int index, ControlCommand cmd);
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;

public class InputRange {
	public int left   = 0;
	public int middle = 0;
	public int right  = 0;
	public InputRange(int start) {
		this.left   = start;
		this.middle = start + 2;
		this.right  = start + 4;
	}
}

public class InputSet {
	readonly public bool left;
	readonly public bool middle;
	readonly public bool right;

	public InputSet(bool l, bool m, bool r) {
		left   = l;
		middle = m;
		right  = r;
	}

	public void debug(){
		List<string> ins = new List<string>();
		if (left)   ins.Add("l"); 
		if (middle) ins.Add("m"); 
		if (right)  ins.Add("r");
		//Debug.Log("INPUT: ("+ String.Join(", ", ins.ToArray()) +")");
	}
}

public class KeyInput : MonoBehaviour {
	//public GameObject      audioObject;
	//private UnitySynthTest audioScript;

	static private InputRange[] ranges;

	void Awake(){
		int keyBottom = 36;
		int keyTop    = 96; 

		ranges = new InputRange[4];
		for (int i = 0; i < 4; i++) {
			ranges[i] = new InputRange(keyBottom + 6 + 12 * i);
		}
	//	audioScript = audioObject.GetComponent<UnitySynthTest>();
	}
		
	public InputSet[] getInputs() {
		InputSet[] sectionInputs = new InputSet[4];

		for (int i = 0; i < 4; i++) {
			InputSet sectionInput = new InputSet(
			  MidiMaster.GetKey(ranges[i].left)   > 0,
			  MidiMaster.GetKey(ranges[i].middle) > 0,
			  MidiMaster.GetKey(ranges[i].right)  > 0
			);

			sectionInputs[i] = sectionInput;
		}
		return sectionInputs;
	}
}
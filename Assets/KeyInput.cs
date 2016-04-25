﻿using UnityEngine;
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
}

public class KeyInput : MonoBehaviour {
	public GameObject audioObject;
	private UnitySynthTest audioScript;

	static private InputRange[] ranges = new InputRange[4] {
		new InputRange(6), new InputRange(12), new InputRange(32), new InputRange(54)
	};

	// Use this for initialization
	void Start () {
		audioScript = audioObject.GetComponent<UnitySynthTest>();
	}
	
	// Update is called once per frame
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
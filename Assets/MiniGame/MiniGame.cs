using UnityEngine;
using System.Collections;

public class MiniGame : MonoBehaviour {
	public Partyer partyer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	abstract void tick (InputSet input);
	abstract void control (ControlCommand command);
}

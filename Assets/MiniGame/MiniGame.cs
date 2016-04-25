using UnityEngine;
using System.Collections;

abstract public class MiniGame : MonoBehaviour {
	public Partyer partyer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract void tick (InputSet input);
	public abstract void control (ControlCommand command);
}

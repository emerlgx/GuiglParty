using UnityEngine;
using System.Collections;

abstract public class MiniGame : MonoBehaviour {
	public Partyer partyer;

	public abstract void tick (InputSet input); 			// gives input from the player after being processed
	public abstract void control (ControlCommand command);	// gives instructions from god

	public virtual void setPartyer (Partyer p) {
		// updates the partyer
		partyer = p;
	}
}

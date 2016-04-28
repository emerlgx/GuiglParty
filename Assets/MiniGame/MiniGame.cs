using UnityEngine;
using System.Collections;

abstract public class MiniGame : MonoBehaviour {
	public Partyer partyer;
	protected bool inSwap;

	public abstract void tick (InputSet input); 			// gives input from the player after being processed
	public abstract void control (ControlCommand command);	// gives instructions from god

	public virtual void setPartyer (Partyer p) {
		// updates the partyer
		partyer = p;
	}

	public virtual void setInSwap(bool b){
		inSwap = b;
	}

	public GameObject blowUp(GameObject kablew){
		Quaternion direction; 
		switch (Random.Range (0, 4)) {
		case 0:
			direction = Quaternion.Euler (new Vector3 (0, 0, 0));
			break;
		case 1:
			direction = Quaternion.Euler (new Vector3 (0, 0, 90));
			break;
		case 2:
			direction = Quaternion.Euler (new Vector3 (0, 0, 180));
			break;
		case 3:
			direction = Quaternion.Euler (new Vector3 (0, 0, 270));
			break;
		default:
			direction = Quaternion.identity;
			break;
		}
		return Instantiate(kablew, transform.position, direction) as GameObject;
	}
}

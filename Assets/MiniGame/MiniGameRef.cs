using UnityEngine;
using System.Collections;

public class MiniGameRef : MonoBehaviour {
	private string gameName;
	private MiniGame getGame() {
		return GetComponentInParent<MiniGame>();
	}
}

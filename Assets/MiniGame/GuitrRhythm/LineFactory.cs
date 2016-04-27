using UnityEngine;
using System.Collections;

public class LineFactory : MonoBehaviour {
	public GameObject note;
	public GameObject chord;
	public float chordChance;

	void Start() {
		NoteFaller.fallspeed = 2f / GetComponent<GuitrRhythm>().beatLength;
	}

	public GameObject makeNote(Vector3 startPoint) {
		GameObject newNote;
		if (Random.Range(0f, 1f) >	 chordChance) {
			newNote = Instantiate(note);
			newNote.GetComponent<NoteFaller>().noteLength = 0;
		} else {
			newNote = Instantiate(chord);
			int chordlen = (int)Mathf.Floor(Random.Range(1f, 5f));
			newNote.GetComponent<NoteFaller>().noteLength = chordlen;
			newNote.transform.localScale = new Vector3(0.1f, 0.1f * chordlen, 1f);
		}
		newNote.transform.position = startPoint;
		return newNote;
	}
}

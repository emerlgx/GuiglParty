using UnityEngine;
using System.Collections;

public class GuitrRhythm : MiniGame {
	private Button[] buttons;
	private readonly float noteLineOffsetWidth = 1.1f;
	private readonly float playLineY           = -1.45f;
	private readonly float noteLinesStartX     = -1.05f;
	private readonly float genLineY            = 3f;
	public GameObject buttonPrefab;	
	public float beatLength;
	public float noteCreationChance;

	private Queue[] noteQueues;
	private int[] beatsTillFree;

	private LineFactory lineFactory;
	private Vector3[] lineStarts; 

	void Awake() {
		buttons = new Button[3];
		for (int i = 0; i < 3; i++) {
			//Debug.Log("making button "+i);
			GameObject buttonCopy = Instantiate<GameObject> (buttonPrefab);
			buttonCopy.transform.SetParent (this.transform);
			buttons [i] = buttonCopy.GetComponent<Button> ();
		}

		noteQueues = new Queue[3];
		for (int i = 0; i < 3; i++) {
			noteQueues[i] = new Queue();
		}
		beatsTillFree = new int[]{ 0, 0, 0 };

		lineFactory = GetComponent<LineFactory> ();
		lineStarts = new Vector3[3];
	}

	void Start() {
		for (int i=0; i < 3; i++) {
			Vector3 posn = new Vector3 (noteLinesStartX + noteLineOffsetWidth * i, playLineY, 0);
			posn += transform.position;
			buttons[i].setPosn(posn);
			buttons[i].newPartyer(partyer);


			lineStarts[i] = new Vector3 (noteLinesStartX + noteLineOffsetWidth * i, genLineY, 0);
			lineStarts[i] += transform.position;
		}
		GameObject playbar = transform.Find("playline").gameObject;
		Vector3 pbPonsn = playbar.transform.position;
		playbar.transform.position = new Vector3(0, playLineY - 0.4f, 0) + transform.position;
		//put partyer in default position

		GameObject topText = transform.FindChild("hotline name").gameObject;	
		topText.GetComponent<TextMesh>().text = "HOTLINE\n" + partyer.name;

		InvokeRepeating("updateNoteTracks", 0, beatLength);
		InvokeRepeating("updateScore",      0, 0.5f);
	}
	
	void Update () {
		foreach (Queue notes in noteQueues) {
			if (notes.Count == 0) continue;
			GameObject note = notes.Peek() as GameObject;
			if (note.transform.position.y < transform.position.y - 4f) {
				Destroy(notes.Dequeue() as GameObject);
			}
		}
	}

	void updateNoteTracks() {
		for (int i = 0; i < 3; i++) {
			if (beatsTillFree[i]-- > 0)
				continue;

			if (Random.Range(0f, 1f) > noteCreationChance)
				continue;

			GameObject newNote = lineFactory.makeNote(lineStarts[i]);
			newNote.transform.SetParent(transform);
			noteQueues[i].Enqueue(newNote);
			beatsTillFree[i] = newNote.GetComponent<NoteFaller>().noteLength;
		}
	}

	void updateScore() {
		if (inSwap) return;
		for (int i = 0; i < 3; i++) {
			buttons[i].updatePlayerScore();
		}
	}

	public override void tick(InputSet input) {
		input.debug();
		buttons[0].setActive(input.left);
		buttons[1].setActive(input.middle);
		buttons[2].setActive(input.right);
	}

	public override void control(ControlCommand cmd) {
	}
}


/*
 * Static background
 * asset package for things that move around
 */

/* Need:
 * Button object with:
     collider
     slot for character sprite
	 script to toggle collider & image

   
 */
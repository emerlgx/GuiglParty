using UnityEngine;
using System.Collections;

public class CatchDerburg : MiniGameMulti {
	private GameObject derberg;
	private Vector3[] startPosns;
	private ChaserMover[] avatars;
	public GameObject avatarTemplate;
	private float bounds  = 4;

	void Awake(){
		partyers = new Partyer[4];
		derberg = transform.FindChild("derburg").gameObject;
		startPosns = new Vector3[4]{
			new Vector3(-bounds,  bounds, 0),
			new Vector3( bounds,  bounds, 0),
			new Vector3(-bounds, -bounds, 0),
			new Vector3( bounds, -bounds, 0)
		};
		avatars = new ChaserMover[4];
	}

	void Start () {
		GameObject[] chasers = new GameObject[4];
		for (int i = 0; i < 4; i++) {
			chasers[i] = Instantiate(avatarTemplate, startPosns[i], Quaternion.identity) as GameObject;
			avatars[i] = chasers[i].GetComponent<ChaserMover>();
			//maybe get ahold of their control scripts
		}

		//place derberg
		derberg.GetComponent<DerburgMover>().setChasers(chasers);
		placeDerberg();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 4; i++) {
			avatars[i].move(inputs[i]);
		}
	}

	public override void tick(InputSet input){
		inputs[0] = input;
	}

	public override void setPartyer(Partyer p){
		addPartyer (0, p);
	}

	public override void addPartyer(int index, Partyer p){
		partyers[index] = p;
		avatars[index].GetComponent<SpriteRenderer>().sprite = p.face;
	}

	void placeDerberg(){
		derberg.transform.position = new Vector3(
			Random.Range(-bounds, bounds),
			Random.Range(-bounds, bounds),
			0
		);
	}

	public override void control(ControlCommand cmd){}
	public override void takeCommand(int i, ControlCommand cmd){}
}

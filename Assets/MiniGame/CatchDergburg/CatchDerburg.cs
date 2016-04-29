using UnityEngine;
using System.Collections;

public class CatchDerburg : MiniGameMulti {
	private GameObject derberg;
	private Vector3[] startPosns;
	private ChaserMover[] chasers;
	public GameObject avatarTemplate;
	private float bounds  = 3.5f;

	void Awake(){
		inputs = new InputSet[4];
		partyers = new Partyer[4];
		derberg = transform.FindChild("derburg").gameObject;
		startPosns = new Vector3[4]{
			new Vector3(-bounds,  bounds, 0),
			new Vector3( bounds,  bounds, 0),
			new Vector3(-bounds, -bounds, 0),
			new Vector3( bounds, -bounds, 0)
		};
		chasers = new ChaserMover[4];

		for (int i = 0; i < 4; i++) {
			GameObject chaserObj = Instantiate(avatarTemplate, startPosns[i] + transform.position, Quaternion.identity) as GameObject;
			chaserObj.transform.SetParent(transform);
			chasers[i] = chaserObj.GetComponent<ChaserMover>();
		}
	}

	void Start () {
		Transform[] chaserObjs = new Transform[4];
		for (int i = 0; i < 4; i++) {
			chaserObjs[i] = chasers[i].transform;
		}

		derberg.GetComponent<DerburgMover>().setChasers(chaserObjs);
		derberg.GetComponent<DerburgMover>().placeDerberg();

	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 4; i++) {
			chasers[i].move(inputs[i]);
		}
	}

	public override void tick(InputSet input){
		Debug.Log("passing p0 null: "+(input == null));
		inputs[0] = input;
	}

	public override void setPartyer(Partyer p){
		partyer = p;
		addPartyer (0, p);
	}

	public override void addPartyer(int index, Partyer p){
		partyers[index] = p;
		//maybe get ahold of their control scripts
		chasers[index].GetComponent<SpriteRenderer>().sprite = p.face;
		chasers [index].partyer = partyers[index];
	}
		
	public override void control(ControlCommand cmd){}
	public override void takeCommand(int i, ControlCommand cmd){}
}

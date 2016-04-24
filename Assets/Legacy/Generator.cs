using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Transform player1;
	public Transform player2;
	public Transform topPosition;
	public Vector3 topOfWorld;
	public Vector3 bottomOfWorld;
	float rowHeight = 50f;
	public int rowBufferCount = 5;
	public bool ObstacleNeeded;
	public GameObject obstacle;
	public GameObject wall;
	public bool isBigWall;

	public float leftBoundary;
	float rightBoundary;
	public float leftWallx; //x value for left wall
	float rightWallx;
	public float screenWidth;

	public float numBoxes = 6;

	// Use this for initialization
	void Start () {
		topPosition = player1;
		bottomOfWorld = topPosition.position;
		topOfWorld = topPosition.position;

		Vector2 wallSize = wall.GetComponent<BoxCollider2D> ().size;  
		rowHeight = wallSize.y * wall.transform.localScale.y - 1;

		leftBoundary = -5f;
		rightBoundary = 35f;
		if (isBigWall) {
			leftBoundary = -40f;//-(screenWidth / 2);
			rightBoundary = 50f;//leftBoundary + screenWidth;
		}
		leftWallx  = leftBoundary + wallSize.x;
		rightWallx = rightBoundary - wallSize.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (player1 && player2) {
			if (player1.position.y >= player2.position.y) {
				topPosition = player1; 
			} else {
				topPosition = player2;
			}
		}
		//
		while (topPosition.position.y > (topOfWorld.y - rowBufferCount * rowHeight)) {
			// TODO: spawn a new row above
			makeRow (topOfWorld.y, topOfWorld.y + rowHeight);
			topOfWorld = new Vector3(topOfWorld.x,
			                         topOfWorld.y + rowHeight, 
			                         topOfWorld.z);
		}
		// check below
		while (topPosition.position.y < (bottomOfWorld.y + rowBufferCount * rowHeight)) {
			// TODO: spawn a new row below
			makeRow (bottomOfWorld.y - rowHeight, bottomOfWorld.y);
			bottomOfWorld = new Vector3(bottomOfWorld.x,
			                            bottomOfWorld.y - rowHeight, 
			                            bottomOfWorld.z);
			}
	}

	void makeRow(float rowTop, float rowBottom) {
		if (wall) {
			GameObject leftWall, rightWall;
			leftWall = Instantiate (wall, new Vector3 (leftBoundary, rowBottom, 0), Quaternion.identity) as GameObject;
			rightWall = Instantiate (wall, new Vector3 (rightWallx, rowBottom, 0), Quaternion.identity) as GameObject;
		}
		if (obstacle) {
			float boxWidth = 40f/numBoxes;
			for (int j = 0; j < numBoxes; j++) {
				Vector3 position = new Vector3 (Random.Range(0, boxWidth-4), Random.Range (rowTop, rowBottom), 0);
				Vector3 scaleFactor = new Vector3(0, Random.Range(0,6), 0);
				Vector3 rotation = new Vector3 (0, 0, Random.Range (10,170));
				position.x += -20f + j * boxWidth;
				GameObject newObstacle = Instantiate (obstacle, position, Quaternion.identity) as GameObject;
				newObstacle.transform.localScale += scaleFactor;
				newObstacle.transform.eulerAngles = rotation;
			}
		}
	}
}

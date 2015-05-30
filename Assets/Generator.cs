using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Transform player1;
	public Transform player2;
	public Transform topPosition;
	public Vector3 topOfWorld;
	public Vector3 bottomOfWorld;
	public float rowHeight = 50f;
	public int rowBufferCount = 5;
	public bool ObstacleNeeded;
	public GameObject obstacle;
	public GameObject wall;

	public float leftWallx; //x value for left wall
	public float rightWallx; //"        " right wall

	// Use this for initialization
	void Start () {
		topPosition = player1;
		bottomOfWorld = topPosition.position;
		topOfWorld = topPosition.position;
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
			// TODO: spawn a new row above
			makeRow (bottomOfWorld.y - rowHeight, bottomOfWorld.y);
			bottomOfWorld = new Vector3(bottomOfWorld.x,
			                                     bottomOfWorld.y - rowHeight, 
			                                     bottomOfWorld.z);
			}
	}

	void makeRow(float rowTop, float rowBottom) {
		if (wall) {
			int numObstacles = (int) Mathf.Round (Random.Range(0, 5));
			GameObject leftWall, rightWall;
			leftWall  = Instantiate (wall, new Vector3 (0,          rowBottom, 0), Quaternion.identity) as GameObject;
			rightWall = Instantiate (wall, new Vector3 (rightWallx, rowBottom, 0), Quaternion.identity) as GameObject;

			for (int i=0; i<numObstacles; i++) {
				makeObstacle(rowTop, rowBottom);
			}
		}
	}

	void makeObstacle (float rowTop, float rowBottom) {
		if (obstacle) {
			Quaternion rotation = new Quaternion ();
			rotation.eulerAngles = new Vector3 (0, 0, Random.Range (-180, 180)); 

			Vector3 position = new Vector3 (Random.Range (leftWallx, rightWallx), Random.Range (rowTop, rowBottom), 0);
			GameObject newObstacle = Instantiate (obstacle, position, rotation) as GameObject;

			Vector3 scale = new Vector3 (Random.Range (0.1f, 4.0f), Random.Range (0.1f, 4.0f), 0f);
			newObstacle.transform.localScale = scale;
		}
	}
}

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
	
			topOfWorld = new Vector3(topOfWorld.x,
			                                  topOfWorld.y + rowHeight, 
			                                  topOfWorld.z);
		}
		// check below
		while (topPosition.position.y < (bottomOfWorld.y + rowBufferCount * rowHeight)) {
			// TODO: spawn a new row above
			
			bottomOfWorld = new Vector3(bottomOfWorld.x,
			                                     bottomOfWorld.y - rowHeight, 
			                                     bottomOfWorld.z);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public Transform player1;
	public Transform player2;
	public Transform topPosition;
	public Transform topOfWorld;
	public Transform bottomOfWorld;
	public float rowHeight = 50f;
	public int rowBufferCount = 5;

	// Use this for initialization
	void Start () {
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
		while (topPosition.position.y > (topOfWorld.position.y - rowBufferCount * rowHeight)) {
			// TODO: spawn a new row above
	
			topOfWorld.position = new Vector3(topOfWorld.position.x,
			                         topOfWorld.position.y + rowHeight, 
			                         topOfWorld.position.z);
		}
		// check below
		while (topPosition.position.y < (bottomOfWorld.position.y + rowBufferCount * rowHeight)) {
			// TODO: spawn a new row above
			
			bottomOfWorld.position = new Vector3(topOfWorld.position.x,
			                         topOfWorld.position.y - rowHeight, 
			                         topOfWorld.position.z);
		}
	}
}

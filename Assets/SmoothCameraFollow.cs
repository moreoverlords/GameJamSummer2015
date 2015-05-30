using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform player1;
	public Transform player2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player1 && player2)
		{
			Transform target;
			if (player1.position.y >= player2.position.y){
				target = player1; 
			} else {
				target = player2;
			}
			Vector3 targetRelPosn = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(targetRelPosn.x, 0.5f, targetRelPosn.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}

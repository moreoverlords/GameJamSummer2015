using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {

	public float dampTime = 0.15f;
	public float verticalOffset;
	private Vector3 velocity = Vector3.zero;
	public Transform target;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target)
		{
			Vector3 targetRelPosn = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(
				new Vector3(targetRelPosn.x, verticalOffset, targetRelPosn.z)
			);
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}

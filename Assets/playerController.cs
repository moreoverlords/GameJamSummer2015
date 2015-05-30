using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	
	public float releaseForce;
	public float maxReleaseForce;
	private Rigidbody2D rigidbody2d;
	public float releaseForceIncreaseRate;
	public KeyCode left;
	public KeyCode right;

	enum State{Idle, ChargeRight, ChargeLeft, LaunchRight, LaunchLeft} 
	private State currentState;

	// Use this for initialization
	void Start () {
		currentState = State.Idle;
		rigidbody2d = GetComponent<Rigidbody2D>(); 
	
	}
	
	void FixedUpdate () {
		if (Input.GetKey (right)) {
			currentState = State.ChargeRight;
			releaseForce += releaseForceIncreaseRate;
		} else if (currentState == State.ChargeRight && Input.GetKeyUp (right)) {
			currentState = State.LaunchRight;
			float currentRelease = Mathf.Min(releaseForce, maxReleaseForce);
			rigidbody2d.AddForce (Vector2.right * currentRelease);
			releaseForce = 0;
		} 
		
	}
}

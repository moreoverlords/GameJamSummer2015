using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float verticalReleaseForce;
	public float releaseForce;
	public float verticalinitialReleaseForce;
	public float initialReleaseForce;
	public float maxReleaseForce;
	public float maxVerticalReleaseForce;

	public float rotationScalar = .5f;

	public float releaseForceIncreaseRate;
	public float verticalReleaseForceIncreaseRate;
	public KeyCode left;
	public KeyCode right;

	private Rigidbody2D rigidbody2d;
	private Transform transform2d;

	enum State{Idle, ChargeRight, ChargeLeft, LaunchRight, LaunchLeft} 
	private State currentState;

	// Use this for initialization
	void Start () {
		currentState = State.Idle;
		rigidbody2d = GetComponent<Rigidbody2D>();
		transform2d = GetComponent<Transform>();	
	}
	
	void Update () {
		if (rigidbody2d.velocity.y > 0) {
			gameObject.layer = LayerMask.NameToLayer("UpBall");
		}
		else if (rigidbody2d.velocity.y <= 0) {
			gameObject.layer = LayerMask.NameToLayer("DownBall");
		}

		if (Input.GetKeyDown (right)) {
			currentState = State.ChargeRight;
			releaseForce = initialReleaseForce;
		} else if (Input.GetKeyDown (left)) {
			currentState = State.ChargeLeft;
			releaseForce = initialReleaseForce;
		} 

		else if (currentState == State.ChargeRight && Input.GetKey (right)) {
			releaseForce += releaseForceIncreaseRate * Time.deltaTime;
			verticalReleaseForce += verticalReleaseForceIncreaseRate * Time.deltaTime;
			rigidbody2d.angularVelocity = -releaseForce*rotationScalar;
		} else if (currentState == State.ChargeLeft && Input.GetKey (left)) {
			releaseForce += releaseForceIncreaseRate * Time.deltaTime;
			verticalReleaseForce += verticalReleaseForceIncreaseRate * Time.deltaTime;
			rigidbody2d.angularVelocity = releaseForce*rotationScalar;
		}

		else if (currentState == State.ChargeRight && Input.GetKeyUp (right)) {
			currentState = State.LaunchRight;
			float currentRelease = Mathf.Min(releaseForce, maxReleaseForce);
			float currentVerticalRelease = Mathf.Min (verticalReleaseForce, maxVerticalReleaseForce);
			rigidbody2d.AddForce (Vector2.right * currentRelease);
			rigidbody2d.AddForce (Vector2.up * currentVerticalRelease);
			releaseForce = 0;
		} else if (currentState == State.ChargeLeft && Input.GetKeyUp (left)) {
			currentState = State.LaunchLeft;
			float currentRelease = Mathf.Min(releaseForce, maxReleaseForce);
			float currentVerticalRelease = Mathf.Min (verticalReleaseForce, maxVerticalReleaseForce);
			rigidbody2d.AddForce (Vector2.right * currentRelease * -1);
			rigidbody2d.AddForce (Vector2.up * currentVerticalRelease);
			releaseForce = 0;
		}		
	}
}


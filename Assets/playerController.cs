using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	
	public float releaseForce;
	public float initialReleaseForce;
	public float maxReleaseForce;
	public float rotationScalar = .5f;

	public float releaseForceIncreaseRate;
	public KeyCode left;
	public KeyCode right;

	public float momentumRange;
	public float hardMomentum;
	public float softMomentum;
	public float momentumScale;
	public float minBounce;

	public float verticalDrag;

	private Rigidbody2D rigidbody2d;
	private Transform transform2d;
	private CircleCollider2D circleCollider2d;

	enum State{Idle, ChargeRight, ChargeLeft, LaunchRight, LaunchLeft} 
	private State currentState;

	// Use this for initialization
	void Start () {
		currentState = State.Idle;
		circleCollider2d = GetComponent<CircleCollider2D> ();
		rigidbody2d = GetComponent<Rigidbody2D>();
		transform2d = GetComponent<Transform>();	
	}
	
	void Update () {
		if (Input.GetKeyDown (right)) {
			currentState = State.ChargeRight;
			releaseForce = initialReleaseForce;
		} else if (Input.GetKeyDown (left)) {
			currentState = State.ChargeLeft;
			releaseForce = initialReleaseForce;
		} 

		else if (currentState == State.ChargeRight && Input.GetKey (right)) {
			releaseForce += releaseForceIncreaseRate * Time.deltaTime;
			rigidbody2d.angularVelocity = -releaseForce*rotationScalar;
		} else if (currentState == State.ChargeLeft && Input.GetKey (left)) {
			releaseForce += releaseForceIncreaseRate * Time.deltaTime;
			rigidbody2d.angularVelocity = releaseForce*rotationScalar;
		}

		else if (currentState == State.ChargeRight && Input.GetKeyUp (right)) {
			currentState = State.LaunchRight;
			float currentRelease = Mathf.Min(releaseForce, maxReleaseForce);
			rigidbody2d.AddForce (Vector2.right * currentRelease);
			releaseForce = 0;
		} else if (currentState == State.ChargeLeft && Input.GetKeyUp (left)) {
			currentState = State.LaunchLeft;
			float currentRelease = Mathf.Min(releaseForce, maxReleaseForce);
			rigidbody2d.AddForce (Vector2.right * currentRelease * -1);
			releaseForce = 0;
		}		
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			// the other player has more momentum, so this player bounces off harder
			float otherVelocityMag = coll.rigidbody.velocity.magnitude;
			float velocityMag = rigidbody2d.velocity.magnitude;
			if (otherVelocityMag/velocityMag>=1+momentumRange) {
				//rigidbody2d.velocity = Vector2.zero;
				Vector2 relativePosition = transform.position - coll.rigidbody.transform.position;
				relativePosition = relativePosition/relativePosition.magnitude;
				rigidbody2d.AddForce(relativePosition*(otherVelocityMag*hardMomentum*momentumScale*(1+velocityMag/otherVelocityMag) + minBounce));
			} else {
				//rigidbody2d.velocity = Vector2.zero;
				Vector2 relativePosition = transform.position - coll.rigidbody.transform.position;
				relativePosition = relativePosition/relativePosition.magnitude;
				rigidbody2d.AddForce(relativePosition*(otherVelocityMag*softMomentum*momentumScale*(1+velocityMag/otherVelocityMag) + minBounce));
			}
		}
	}


}

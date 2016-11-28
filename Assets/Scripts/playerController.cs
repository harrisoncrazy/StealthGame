using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float moveSpeed;
	private float defaultMoveSpeed = 20; //Default move speed for slowing down/speeding up with shift if implimented, leaving it for now

	private float maxSpeed = 2.5f;

	public float rotationSpeed;

	private float jumpSpeed;
	public Rigidbody playerRB;

	private bool isOnGround = false;

	// Use this for initialization
	void Start () {
		moveSpeed = defaultMoveSpeed;
		jumpSpeed = 200;
		playerRB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

		if (playerRB.velocity.magnitude < maxSpeed) { //restricting player speed
			if (Input.GetKey ("w")) { //Forward
				playerRB.AddForce (transform.forward * moveSpeed);
			}
			if (Input.GetKey ("s")) { //Back
				playerRB.AddForce (-transform.forward * moveSpeed);
			}
			/* OLD A AND D MOVEMENT STRAFING
			if (Input.GetKey ("a")) { //left
				playerRB.AddForce (-transform.right * moveSpeed);
			}
			if (Input.GetKey ("d")) { //right
				playerRB.AddForce (transform.right * moveSpeed);
			}*/
		}
		if (Input.GetKey ("a")) { //rotating left
			transform.Rotate(0, -rotationSpeed, 0);
		}
		if (Input.GetKey ("d")) {//rotating right
			transform.Rotate (0, rotationSpeed, 0);
		}

		/*
		//slowing down if shift is pressed
		if (Input.GetKey (KeyCode.LeftShift)) {
			moveSpeed = defaultMoveSpeed*3;
		}
		//setting speed back to normal
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			moveSpeed = defaultMoveSpeed;
		}*/


		if (Input.GetKeyDown (KeyCode.Space) && isOnGround == true) { //jumping
			playerRB.AddForce(transform.up * jumpSpeed);
			isOnGround = false;
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "ground") {//hitting the ground again and reseting jump bool
			isOnGround = true;
			moveSpeed = defaultMoveSpeed;
		}
	}
	/*/ TESTING MAGNITUDE/VELOCITY VALUES
	void OnGUI() {
		GUI.Label (new Rect (20, 20, 200, 200), "rigidbody velocity: " + playerRB.velocity.magnitude);
	}*/
}

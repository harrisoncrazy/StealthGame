using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float moveSpeed;
	private float defaultMoveSpeed = 15;

	private float maxSpeed = 2.5f;

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
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

		if (playerRB.velocity.magnitude < maxSpeed) { //restricting player speed
			if (Input.GetKey ("w")) { //Forward
				playerRB.AddForce (transform.forward * moveSpeed);
			}
			if (Input.GetKey ("s")) { //Back
				playerRB.AddForce (-transform.forward * moveSpeed);
			}
			if (Input.GetKey ("a")) { //left
				playerRB.AddForce (-transform.right * moveSpeed);
			}
			if (Input.GetKey ("d")) { //right
				playerRB.AddForce (transform.right * moveSpeed);
			}
		}

		//slowing down if shift is pressed
		if (Input.GetKey (KeyCode.LeftShift)) {
			moveSpeed = defaultMoveSpeed - 5;
		}
		//setting speed back to normal
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			moveSpeed = defaultMoveSpeed;
		}


		if (Input.GetKeyDown (KeyCode.Space) && isOnGround == true) {
			playerRB.AddForce(transform.up * jumpSpeed);
			isOnGround = false;
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "ground") {//killing an enemy minion
			isOnGround = true;
			moveSpeed = defaultMoveSpeed;
		}
	}
	/*/ TESTING MAGNITUDE/VELOCITY VALUES
	void OnGUI() {
		GUI.Label (new Rect (20, 20, 200, 200), "rigidbody velocity: " + playerRB.velocity.magnitude);
	}*/
}

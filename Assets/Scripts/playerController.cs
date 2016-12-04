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

	//Gadget Bools
	public bool boostEnabled = false;
	public bool hookEnabled = false;
	public bool smokeEnabled = false;
	public bool tazerEnabled = false;

	//Hookshot variables
	private bool isHooked = false;
	public Vector3 hookLocation;
	LineRenderer line;
	private bool isAiming = false;
	private bool hasFired = false;

	//Smoke Bomb variables
	public bool isInSmoke = false;
	public GameObject smoke;

	//Tazer variables
	public bool firedTazer = false;
	public GameObject tazerBolt;

	// Use this for initialization
	void Start () {
		moveSpeed = defaultMoveSpeed;
		jumpSpeed = 200;
		playerRB = GetComponent<Rigidbody> ();

		line = gameObject.GetComponent<LineRenderer> ();
		line.SetVertexCount (2);
		line.SetWidth (0.25f, 0.25f);
		line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) {
			if (isHooked != true) {
				line.enabled = false;//disabling linerender when player dies
			}
		}
		if (Time.timeScale != 0) {//disabling input if time is frozen
			line.SetPosition (0, transform.position);
			if (isAiming == true) {
				line.SetPosition (1, transform.forward * 10 + transform.position);
			}
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
				transform.Rotate (0, -rotationSpeed, 0);
			}
			if (Input.GetKey ("d")) {//rotating right
				transform.Rotate (0, rotationSpeed, 0);
			}

			if (boostEnabled == true) {
				if (Input.GetKeyDown (KeyCode.LeftShift)) {//small speed boost when shift pressed
					moveSpeed = defaultMoveSpeed * 20;
					StartCoroutine ("backToNormal");
				}
			}

			if (hookEnabled == true) {
				if (Input.GetKeyDown ("v")) {//Grappling hook
					if (isAiming == false) {//toggling aiming on
						line.SetWidth (0.1f, 0.1f);
						line.SetColors (Color.red, Color.red);
						line.enabled = true;
						isAiming = true;
					} else if (isAiming == true) {//firing the hook
						if (hasFired == false) {
							RaycastHit hit;
							int layer_mask = LayerMask.GetMask ("Avoid");
							if (Physics.Raycast (transform.position, transform.forward, out hit, 10f, layer_mask)) {
								hookLocation = hit.point;
								isHooked = true;//begging reel
								hasFired = true;
							}
						}
					}
				}
			}

			if (isHooked == true) { //reeling in to hook location
				line.SetPosition (1, hookLocation);//enabling and setting color of the linerender hook
				line.SetWidth (0.25f, 0.25f);
				line.SetColors (Color.white, Color.white);
				line.enabled = true;
				isAiming = false;
				hasFired = false;

				transform.LookAt (hookLocation);
				transform.position += transform.forward * Time.deltaTime * 10.0f;
				if (Mathf.Round (transform.localPosition.x) == Mathf.Round (hookLocation.x) && Mathf.Round (transform.localPosition.z) == Mathf.Round (hookLocation.z)) {//stopping when reaching hooked location
					isHooked = false;
					line.enabled = false;
					transform.rotation = Quaternion.Euler (0, transform.rotation.y, 0);
				}
			}

			if (smokeEnabled == true) {
				if (Input.GetKeyDown ("c")) {//throwing a smoke bomb
					GameObject smoker = GameObject.Instantiate (smoke, transform.position, Quaternion.Euler (90, 0, 0)) as GameObject;
				}
			}

			if (tazerEnabled == true) {
				if (Input.GetMouseButtonDown (0)) {//firing a tazer bolt
					if (firedTazer == false) {
						GameObject shot = GameObject.Instantiate (tazerBolt, transform.position, transform.rotation) as GameObject;
						firedTazer = true;
					}
				}
			}
				
			if (Input.GetKeyDown (KeyCode.Space) && isOnGround == true) { //jumping
				playerRB.AddForce (transform.up * jumpSpeed);
				isOnGround = false;
			}
		}
	}

	IEnumerator backToNormal(){//setting move speed back to normal
		yield return new WaitForSeconds (.4f);
		moveSpeed = defaultMoveSpeed;
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "ground") {//hitting the ground again and reseting jump bool
			isOnGround = true;
			moveSpeed = defaultMoveSpeed;
		}
		if (col.gameObject.tag == "Enemy") {//hitting an enemy
			if (col.gameObject.GetComponent<guardController> ().isDead != true) {
				Time.timeScale = 0;
			}
		}
		if (col.gameObject.tag == "Smoke") {//standing in smoke
			isInSmoke = true;
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Smoke") {//standing in smoke
			isInSmoke = true;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Smoke") {//standing in smoke
			isInSmoke = false;
		}
	}
	/*/ TESTING MAGNITUDE/VELOCITY VALUES
	void OnGUI() {
		GUI.Label (new Rect (20, 20, 200, 200), "rigidbody velocity: " + playerRB.velocity.magnitude);
	}*/
}


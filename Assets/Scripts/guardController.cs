using UnityEngine;
using System.Collections;

public class guardController : MonoBehaviour {

	public float velocityMax;

	//Wandering Values
	public float xMax;
	public float zMax;
	public float xMin;
	public float zMin;
	private float x;
	private float z;
	private float timer;
	private Vector3 lookLoc;

	//point to point Values
	private bool pointGenerated = false;
	private bool isAtEndPoint = false;

	//Scanning Values
	public float Angle;
	public float originalAngle; //A varable between 10 and -5 which relates to the orign point of rotation
	public float Period;
	private float Timer;


	//Different Mode Bools
	public bool isWandering = false;
	public bool isPointToPoint = false;
	public bool isScanning = false;


	public GameObject player;
	private bool foundPlayer = false;

	// Use this for initialization
	void Start () {
		velocityMax = velocityMax / 100;

		if (isWandering == true) {
			x = Random.Range (xMin, xMax);
			z = Random.Range (zMin, zMax);
			lookLoc = new Vector3 (x, -0.563f, z);
			transform.LookAt (lookLoc);
		}
		if (isPointToPoint == true) {
			generatePoint ();
		}
		if (isScanning == true) {
			Angle = Angle + transform.rotation.y;
		}

		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (isWandering == true) { //Wanders based on random point generated from zmax/min and xmax/min
			if (Mathf.Round (transform.localPosition.x) == Mathf.Round (x) && Mathf.Round (transform.localPosition.z) == Mathf.Round (z) && foundPlayer != true) {
				x = Random.Range (xMin, xMax);
				z = Random.Range (zMin, zMax);
				lookLoc = new Vector3 (x, -0.563f, z);
				transform.LookAt (lookLoc);
			}
			transform.LookAt (lookLoc);
			transform.position += transform.forward * Time.deltaTime * velocityMax;
		}

		if (isPointToPoint == true) { //wanders from point a to b and back generated from zmax/min and xmax/min using the min value for the start and max for the end
			if (isAtEndPoint == false) {
				if (Mathf.Round (transform.localPosition.x) == Mathf.Round (lookLoc.x) && Mathf.Round (transform.localPosition.z) == Mathf.Round (lookLoc.z) && foundPlayer != true) {
					isAtEndPoint = true;
					generatePoint ();
				}
			}
			if (isAtEndPoint == true) {
				if (Mathf.Round (transform.localPosition.x) == Mathf.Round (lookLoc.x) && Mathf.Round (transform.localPosition.z) == Mathf.Round (lookLoc.z) && foundPlayer != true) {
					isAtEndPoint = false;
					generatePoint ();
				}
			}
			transform.LookAt (lookLoc);
			transform.position += transform.forward * Time.deltaTime * velocityMax;
		}

		if (isScanning == true) {
			Timer = Timer + Time.deltaTime;
			float phase = Mathf.Sin (Timer / Period) + originalAngle;
			transform.rotation = Quaternion.Euler (new Vector3 (0, phase * Angle, 0));

			if (foundPlayer == true) {
				transform.LookAt (lookLoc);
				transform.position += transform.forward * Time.deltaTime * velocityMax;
			}
		}

		RaycastHit hit;//Trying to detect player
		if (Physics.Raycast (transform.position, transform.forward, out hit, 4.0f)) {
			if (hit.collider.tag == "Player") {
				lookLoc = player.transform.position;
				velocityMax = 2;
				foundPlayer = true;
			}
		}
	}

	void generatePoint() {
		if (isAtEndPoint == false) {
			lookLoc = new Vector3 (xMin, -0.563f, zMin);
		} else if (isAtEndPoint == true) {
			lookLoc = new Vector3 (xMax, -0.563f, zMax);
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {//killing an enemy minion
			Time.timeScale = 0;
		}
	}
}

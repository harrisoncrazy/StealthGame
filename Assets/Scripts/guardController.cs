using UnityEngine;
using System.Collections;

public class guardController : MonoBehaviour {

	public bool isDead = false;

	public float velocityMax;
	private float velocityInit;

	//Wandering Values
	public float xMax;
	public float zMax;
	public float xMin;
	public float zMin;
	private float x;
	private float z;
	private float timer;
	public Vector3 lookLoc;

	//point to point Values
	private bool pointGenerated = false;
	private bool isAtEndPoint = false;
	public bool rotatingTowards = false;

	//Scanning Values
	public float Angle;
	public float originalAngle; //A varable between 10 and -5 which relates to the orign point of rotation
	public float Period;
	private float Timer;


	//Different Mode Bools
	public bool isWandering = false;
	public bool isPointToPoint = false;
	public bool isScanning = false;

	//Light values
	public Light mainLight;
	public Light frontRed;
	public Light backRed;
	public Light leftRed;
	public Light rightRed;
	private float timerPhaseOne = 1.0f;
	private bool timerPhaseOneDone = false;
	private float timerPhaseTwo = 0.5f;
	private bool timerPhaseTwoDone = false;
	private int lightFlashNum;

	private float minIntensity = 0.01f;
	private float maxIntensity = 1.0f;

	float random;

	public GameObject player;
	public bool foundPlayer = false;

	// Use this for initialization
	void Start () {
		random = Random.Range (0.0f, 65545.0f);

		velocityMax = velocityMax / 100;
		velocityInit = velocityMax;

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
		if (isDead == false) {//disabling if dead
			mainLight.enabled = true;
			frontRed.enabled = true;
			backRed.enabled = true;
			leftRed.enabled = true;
			rightRed.enabled = true;
			frontRed.intensity = 1.0f;
			backRed.intensity = 1.0f;
			leftRed.intensity = 1.0f;
			rightRed.intensity = 1.0f;

			if (isWandering == true) { //Wanders based on random point generated from zmax/min and xmax/min
				if (foundPlayer == false) {
					if (Mathf.Round (transform.localPosition.x) == Mathf.Round (x) && Mathf.Round (transform.localPosition.z) == Mathf.Round (z) && foundPlayer != true) {
						x = Random.Range (xMin, xMax);
						z = Random.Range (zMin, zMax);
						lookLoc = new Vector3 (x, -0.563f, z);
					}
					Vector3 _direction = (lookLoc - transform.position).normalized;
					Quaternion _lookRotation = Quaternion.LookRotation (_direction);
					transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * 1.0f);
					transform.position += transform.forward * Time.deltaTime * velocityMax;
				}
			}

			if (isPointToPoint == true) { //wanders from point a to b and back generated from zmax/min and xmax/min using the min value for the start and max for the end
				if (foundPlayer == false) {
					if (isAtEndPoint == false) {
						if (Mathf.Round (transform.localPosition.x) == Mathf.Round (lookLoc.x) && Mathf.Round (transform.localPosition.z) == Mathf.Round (lookLoc.z)) {
							isAtEndPoint = true;
							rotatingTowards = true;
							generatePoint ();
						}
					}
					if (isAtEndPoint == true) {
						if (Mathf.Round (transform.localPosition.x) == Mathf.Round (lookLoc.x) && Mathf.Round (transform.localPosition.z) == Mathf.Round (lookLoc.z)) {
							isAtEndPoint = false;
							rotatingTowards = true;
							generatePoint ();
						}
					}
				
					if (rotatingTowards == false) {
						transform.LookAt (lookLoc);
						transform.position += transform.forward * Time.deltaTime * velocityMax;
					} else {
						Vector3 _direction = (lookLoc - transform.position).normalized;
						Quaternion _lookRotation = Quaternion.LookRotation (_direction);
						transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * 2.0f);

						float threshold = 0.99f;
						Vector3 dir = (lookLoc - transform.position).normalized;
						float direction = Vector3.Dot (dir, transform.forward);

						if (direction >= threshold) {
							rotatingTowards = false;
						}
					}
				}
			}

			if (isScanning == true) {
				if (foundPlayer == false) {
					Timer = Timer + Time.deltaTime;
					float phase = Mathf.Sin (Timer / Period) + originalAngle;
					transform.rotation = Quaternion.Euler (new Vector3 (0, phase * Angle, 0));
				}
			}

			if (foundPlayer == true) {
				lookLoc = player.transform.position;
				velocityMax = 5;
				transform.LookAt (lookLoc);
				transform.position += transform.forward * Time.deltaTime * velocityMax;
			}
		} 

		else if (isDead == true) {
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			mainLight.enabled = false;
			frontRed.enabled = false;
			backRed.enabled = false;
			leftRed.enabled = false;
			rightRed.enabled = false;

			timerPhaseOne -= Time.deltaTime;
			if (timerPhaseOne <= 0) {
				frontRed.enabled = true;
				backRed.enabled = true;
				leftRed.enabled = true;
				rightRed.enabled = true;
				timerPhaseTwo -= Time.deltaTime;
				if (timerPhaseTwo <= 0) {
					frontRed.enabled = false;
					backRed.enabled = false;
					leftRed.enabled = false;
					rightRed.enabled = false;
					timerPhaseTwo = Random.Range (0.1f, 2.0f);
					timerPhaseOne = Random.Range (0.1f, 1.0f);
				}
			}

			float noise = Mathf.PerlinNoise (random, Time.time);
			frontRed.intensity = Mathf.Lerp (minIntensity, maxIntensity, noise);
			backRed.intensity = Mathf.Lerp (minIntensity, maxIntensity, noise);
			leftRed.intensity = Mathf.Lerp (minIntensity, maxIntensity, noise);
			rightRed.intensity = Mathf.Lerp (minIntensity, maxIntensity, noise);
		}
	}

	void generatePoint() {
		if (isAtEndPoint == false) {
			lookLoc = new Vector3 (xMin, -0.563f, zMin);
		} else if (isAtEndPoint == true) {
			lookLoc = new Vector3 (xMax, -0.563f, zMax);
		}
	}
	/*
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			Time.timeScale = 0;
		}
	}*/
}

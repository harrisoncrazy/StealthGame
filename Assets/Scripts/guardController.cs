using UnityEngine;
using System.Collections;

public class guardController : MonoBehaviour {

	public float velocityMax;

	public float xMax;
	public float zMax;
	public float xMin;
	public float zMin;

	public float x;
	public float z;
	private float timer;
	private Vector3 lookLoc;
	private Vector3 tempStore;

	public GameObject player;
	private bool foundPlayer = false;

	// Use this for initialization
	void Start () {
		x = Random.Range(xMin, xMax);
		z = Random.Range(zMin, zMax);
		lookLoc = new Vector3 (x, -0.563f, z);
		transform.LookAt (lookLoc);

		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (Mathf.Round(transform.localPosition.x) == Mathf.Round(x) && Mathf.Round(transform.localPosition.z) == Mathf.Round(z) && foundPlayer != true) {
			x = Random.Range(xMin, xMax);
			z = Random.Range(zMin, zMax);
			lookLoc = new Vector3 (x, -0.563f, z);
			transform.LookAt (lookLoc);
		}
		transform.LookAt (lookLoc);
		transform.position += transform.forward * Time.deltaTime * velocityMax;

		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, 4.0f)) {
			if (hit.collider.tag == "Player") {
				tempStore = lookLoc;
				lookLoc = player.transform.position;
				velocityMax = 2;
				foundPlayer = true;
			}
		}
	}
}

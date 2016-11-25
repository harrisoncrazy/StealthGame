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

	// Use this for initialization
	void Start () {
		x = Random.Range(xMin, xMax);
		z = Random.Range(zMin, zMax);
		lookLoc = new Vector3 (x, -0.487f, z);
		transform.LookAt (lookLoc);
	}

	// Update is called once per frame
	void Update () {
		/*
		if (transform.localPosition.x > xMax) {
			x = Random.Range(-velocityMax, 0.0f);
			angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
			transform.localRotation = Quaternion.Euler(0, angle, 0);
			timer = 0.0f; 
		}
		if (transform.localPosition.x < xMin) {
			x = Random.Range(0.0f, velocityMax);
			angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
			transform.localRotation = Quaternion.Euler(0, angle, 0); 
			timer = 0.0f; 
		}
		if (transform.localPosition.z > zMax) {
			z = Random.Range(-velocityMax, 0.0f);
			angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
			transform.localRotation = Quaternion.Euler(0, angle, 0); 
			timer = 0.0f; 
		}
		if (transform.localPosition.z < zMin) {
			z = Random.Range(0.0f, velocityMax);
			angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
			transform.localRotation = Quaternion.Euler(0, angle, 0);
			timer = 0.0f; 
		}

*/
		if (Mathf.Round(transform.localPosition.x) == Mathf.Round(x) && Mathf.Round(transform.localPosition.z) == Mathf.Round(z)) {
			x = Random.Range(xMin, xMax);
			z = Random.Range(zMin, zMax);
			lookLoc = new Vector3 (x, -0.487f, z);
			transform.LookAt (lookLoc);
		}
		transform.LookAt (lookLoc);
		transform.position += transform.forward * Time.deltaTime * velocityMax;
	}
}

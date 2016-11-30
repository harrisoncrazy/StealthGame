using UnityEngine;
using System.Collections;

public class guardCollision : MonoBehaviour {

	private float velocityInit;

	// Use this for initialization
	void Start () {
		velocityInit = transform.parent.GetComponent<guardController> ().velocityMax;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.tag == "Player") {//killing an enemy minion
			transform.parent.GetComponent<guardController>().foundPlayer = true;
			transform.parent.GetComponent<guardController>().velocityMax = 5;
			Time.timeScale = .5f;
		}
	}
	void OnTriggerExit (Collider coll) {
		if (coll.gameObject.tag == "Player") {//killing an enemy minion
			transform.parent.GetComponent<guardController>().foundPlayer = false;
			transform.parent.GetComponent<guardController>().velocityMax = velocityInit;
		}
	}
}

using UnityEngine;
using System.Collections;

public class tazerBolt : MonoBehaviour {

	private float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine ("DestroySelf");
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * moveSpeed; //moving forward
	}

	IEnumerator DestroySelf() {
		yield return new WaitForSeconds (1.0f);
		GameObject.Find ("Player").GetComponent<playerController> ().firedTazer = false;
		Destroy (this.gameObject);
	}

	void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.tag != "Player") {
			if (coll.gameObject.tag != "Cone") {
				if (coll.gameObject.tag == "Enemy") {
					coll.gameObject.GetComponent<guardController> ().isDead = true;
				}
				GameObject.Find ("Player").GetComponent<playerController> ().firedTazer = false;
				Destroy (this.gameObject);
			}
		}
	}

	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.tag != "Player") {
			if (coll.gameObject.tag != "Cone") {
				GameObject.Find ("Player").GetComponent<playerController> ().firedTazer = false;
				Destroy (this.gameObject);
			}
		}
	}
}

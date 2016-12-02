using UnityEngine;
using System.Collections;

public class smokeHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("DestroySelf");
	}

	IEnumerator DestroySelf() {
		yield return new WaitForSeconds (5.0f);
		GameObject.Find ("Player").GetComponent<playerController> ().isInSmoke = false;
		Destroy (this.gameObject);
	}
}

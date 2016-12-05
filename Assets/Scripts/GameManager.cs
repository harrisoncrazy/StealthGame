using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public string sceneToLoad;

	public Text briefcaseText;
	public int briefcasesMax;
	public int briefcasesCollected;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		briefcaseText.text = "Briefcases Collected: " + briefcasesCollected;

		if (briefcasesCollected == briefcasesMax) {
			QuitGame ();
		}
	}

	public void QuitGame() {
		Application.Quit ();
	}

	public void LoadLevel() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene (sceneToLoad);
	}

	public void Upgrade() {
		playerController.Instance.boostEnabled = true;
		playerController.Instance.hookEnabled = true;
		playerController.Instance.smokeEnabled = true;
		playerController.Instance.tazerEnabled = true;
	}
}

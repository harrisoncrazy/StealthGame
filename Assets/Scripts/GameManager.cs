using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

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
	}
}

using UnityEngine;
using System.Collections;

public class TutorialInventorBootsTextBehaviour : MonoBehaviour {
	bool playerEntered = false;
	bool pressedThree = false;
	bool pressedE = false;
	GameObject textManager;

	void OnTriggerEnter(Collider collider) {
		playerEntered = true;
	}

	// Use this for initialization
	void Start () {
		textManager = GameObject.FindGameObjectWithTag("TextManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (playerEntered) {
			if (!pressedThree && Input.GetKey ("3")) {
				textManager.guiText.text = "Super shoes! Grab them by pressing E...";
				pressedThree = true;
			} else if (!pressedE && Input.GetKey ("e")) {
				textManager.guiText.text = "Now try jumping up on that ledge...";
				pressedE = true;
			}
		}
	}
}

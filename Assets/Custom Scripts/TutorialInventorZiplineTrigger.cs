using UnityEngine;
using System.Collections;

public class TutorialInventorZiplineTrigger : MonoBehaviour {
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
				textManager.guiText.text = "A zipline! You can use that to zip across that wire.";
				pressedThree = true;
			} else if (!pressedE && Input.GetKey ("e")) {
				textManager.guiText.text = "Now try going up to the wire and pressing E...";
				pressedE = true;
			}
		}
	}
}

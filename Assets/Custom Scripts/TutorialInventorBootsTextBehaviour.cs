using UnityEngine;
using System.Collections;

public class TutorialInventorBootsTextBehaviour : MonoBehaviour {
	bool investigated = false;
	GameObject textManager;

	// Use this for initialization
	void Start () {
		textManager = GameObject.FindGameObjectWithTag("TextManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("3")) {
			textManager.guiText.text = "Super shoes! Grab them by pressing E...";
		} else if (Input.GetKey ("e")) {
			textManager.guiText.text = "Now try jumping up on that ledge...";
		}
	}
}

using UnityEngine;
using System.Collections;

public class LevelCube : MonoBehaviour {

	bool inCube = false;
	bool enterable = false;
	public string level;
	GameObject textManager;

	void Start () {
		textManager = GameObject.FindGameObjectWithTag("TextManager");
	}
	
	void Update () {
		
		if (inCube && enterable && Input.GetKey("e")) {
			Application.LoadLevel(level);
		}
		
	}
	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			inCube = true;
			if (PersistentLevelManager.IsLevelCompleted(level)) {
				textManager.guiText.text = "Level already completed. Press E to enter the level...";
			}
			else {
				textManager.guiText.text = "Press E to enter the level...";
			}
			if (PersistentLevelManager.IsNextLevel(level)) {
				enterable = true;
			}
			else {
				textManager.guiText.text = "You need to complete all previous levels.";
				enterable = false;
			}
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			inCube = false; 
			textManager.guiText.text = "";
		}
	}
}

using UnityEngine;
using System.Collections;

public class TutorialCube : MonoBehaviour {

	bool inCube = false;
	bool enterable = false;
	GameObject textManager;

	void Start () {
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("PersistentLevelManager"));
		textManager = GameObject.FindGameObjectWithTag("TextManager");
		textManager.guiText.text = "Welcome to Alterego!";
	}
	
	void Update () {
		
		if (inCube && enterable && Input.GetKey("e")) {
			Application.LoadLevel("map_tutorial");
		}
		
	}
	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Alterego Player") {
			inCube = true;
			Debug.Log (PersitentLevelManager.IsLevelCompleted("map_tutorial") + "");
			if (PersitentLevelManager.IsLevelCompleted("map_tutorial")) {
				textManager.guiText.text = "Level already completed. Press E to enter the level...";
			}
			else {
				textManager.guiText.text = "Press E to enter the level...";
				if (PersitentLevelManager.IsNextLevel("map_tutorial") ) {
					enterable = true;
				}
				else {
					textManager.guiText.text = "You need to complete all previous levels.";
					enterable = false;
				}
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
